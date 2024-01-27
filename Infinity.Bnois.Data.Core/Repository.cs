using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infinity.Bnois.Data.Core
{
    public abstract class Repository<TDbContext, TEntity> : IRepository<TEntity>
        where TEntity : class
         where TDbContext : DbContext
    {
        public readonly TDbContext context;
        private readonly DbSet<TEntity> dbSet;

        protected Repository(TDbContext context)
        {
            this.context = context;
            dbSet = this.context.Set<TEntity>();
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// Find entity based on lamda-expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity FindOne(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.FirstOrDefault(predicate);
        }

        public virtual TEntity FindOne(Expression<Func<TEntity, bool>> predicate, List<string> includeProperties)
        {
            IQueryable<TEntity> query = dbSet.Where(predicate);

            if (includeProperties != null)
            {
                foreach (var name in includeProperties)
                {
                    query = query.Include(name);
                }
            }

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Check entity exists or not based on lamda-expression
        /// </summary>
        /// <param name="predicate">Expression to match</param>
        /// <returns></returns>
        public virtual bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Any(predicate);
        }

        public virtual IQueryable<TEntity> AsQueryable()
        {
            return dbSet.AsQueryable();
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An System.Linq.IQueryable`1 that contains elements from the input sequence that 
        /// satisfy the condition specified by predicate.
        /// </returns>
        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public virtual int Count()
        {
            return dbSet.Count();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Count(predicate);
        }

        public virtual int Save(TEntity entity)
        {
            DbEntityEntry<TEntity> entry = context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                dbSet.Add(entity);
                entry.State = EntityState.Added;
            }
            else
            {
                dbSet.Attach(entity);
                entry.State = EntityState.Modified;
            }

            try
            {
                return context.SaveChanges();
            }
            catch
            {
                ClearEntityState();
                throw;
            }
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
            SaveChanges();
        }

        public virtual int SaveAll(List<TEntity> entity)
        {
            dbSet.AddRange(entity);
            return SaveChanges();
        }

        public virtual int UpdateAll(List<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                DbEntityEntry<TEntity> entry = context.Entry(entity);

                dbSet.Attach(entity);
                entry.State = EntityState.Modified;
            }
            return SaveChanges();
        }

        public virtual int Recreate(List<TEntity> entity)
        {
            DbSet<TEntity> objects = dbSet;
            dbSet.RemoveRange(objects);
            dbSet.AddRange(entity);
            return SaveChanges();
        }

        public virtual int Recreate(List<TEntity> oldEntities, List<TEntity> newEntities)
        {
            dbSet.RemoveRange(oldEntities);
            dbSet.AddRange(newEntities);
            return SaveChanges();
        }

        public virtual bool Delete(TEntity entity)
        {
            dbSet.Remove(entity);
            return SaveChanges() > 0;
        }

        /// <summary>
        /// Will be used for DeleteALL
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> objects = Where(predicate);
            dbSet.RemoveRange(objects);
            return SaveChanges();
        }

        /// <summary>
        /// Save all changes in DbContext
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            try
            {
                return context.SaveChanges();
            }
            catch
            {
                ClearEntityState();
                throw;
            }
        }

        private void ClearEntityState()
        {
            List<DbEntityEntry> changedEntries = context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Modified))
            {
                entry.CurrentValues.SetValues(entry.OriginalValues);
                entry.State = EntityState.Unchanged;
            }

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Added))
            {
                entry.State = EntityState.Detached;
            }

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Deleted))
            {
                entry.State = EntityState.Unchanged;
            }
        }

        /// <summary>
        /// Add entity to DbSet. SaveChanges requires to update database.
        /// </summary>
        /// <param name="entity">Entity to add in DbSet</param>
        public virtual void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// Add entity collection to DbSet. SaveChanges requires to update database.
        /// </summary>
        /// <param name="entities"></param>
        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }

        /// <summary>
        /// Remove entity from DbSet. SaveChanges requires to update database.
        /// </summary>
        /// <param name="entity">Entity to remove from DbSet</param>
        public virtual void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }


        public virtual void BlukInsert(IEnumerable<TEntity> entities)
        {
            bool autoDetectChangesEnabled = context.Configuration.AutoDetectChangesEnabled;
            bool validateOnSaveEnabled = context.Configuration.ValidateOnSaveEnabled;

            context.Configuration.AutoDetectChangesEnabled = false;
            context.Configuration.ValidateOnSaveEnabled = false;

            dbSet.AddRange(entities);
            SaveChanges();

            context.Configuration.AutoDetectChangesEnabled = autoDetectChangesEnabled;
            context.Configuration.ValidateOnSaveEnabled = validateOnSaveEnabled;
        }

        public virtual IQueryable<TEntity> GetAllInclude(params string[] include)
        {
            IQueryable<TEntity> query = this.dbSet.AsNoTracking();
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.AsQueryable();
        }

        public virtual TEntity FinedOneInclude(Expression<Func<TEntity, bool>> predicate, params string[] include)
        {
            IQueryable<TEntity> query = this.dbSet.AsNoTracking();
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.FirstOrDefault(predicate);
        }

        public virtual IQueryable<TEntity> FilterWithInclude(Expression<Func<TEntity, bool>> predicate, params string[] include)
        {
            IQueryable<TEntity> query = this.dbSet.AsNoTracking();
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(predicate).AsQueryable();
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.CountAsync(predicate);
        }

        public virtual Task<int> CountAsync()
        {
            return dbSet.CountAsync();
        }


        public async virtual Task<int> SaveChangeAsync()
        {
            try
            {
                return await context.SaveChangesAsync();
            }
            catch(Exception e)
            {
               
                ClearEntityState();
                throw;
            }
        }

        public virtual Task<int> SaveAsync(TEntity entity)
        {
            DbEntityEntry<TEntity> entry = context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                dbSet.Add(entity);
                entry.State = EntityState.Added;
            }
            else
            {
                dbSet.Attach(entity);
                entry.State = EntityState.Modified;
            }

            try
            {
                return SaveChangeAsync();
            }
            catch
            {
                ClearEntityState();
                throw;
            }
        }

        public virtual Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.FirstOrDefaultAsync(predicate);
        }

        public Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate, List<string> includeProperties)
        {
            IQueryable<TEntity> query = this.dbSet.AsNoTracking();
            query = includeProperties.Aggregate(query, (current, inc) => current.Include(inc));
            return query.FirstOrDefaultAsync(predicate);
        }


        public virtual Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.AnyAsync(predicate);
        }

        public async virtual Task<bool> DeleteAsync(TEntity entity)
        {
            dbSet.Remove(entity);
            try
            {
                var effectedRows = await SaveChangeAsync();
                return effectedRows > 0;
            }
            catch (Exception e)
            {

                throw new Exception("Someting went wrong. Please first delete family information");
              
            }
            
        }

        public async virtual Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> objects = Where(predicate);
            dbSet.RemoveRange(objects);
            return await SaveChangeAsync() > 0;
        }

        public async virtual Task<ICollection<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> predicate)
        {
          return  await dbSet.Where(predicate).ToListAsync();
        }

        public Task<int> SaveAllAsync(List<TEntity> entity)
        {
            dbSet.AddRange(entity);
            return SaveChangeAsync();
        }

        public int RemoveRange(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
            return SaveChanges();
        }
    }


}
