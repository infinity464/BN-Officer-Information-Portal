using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Data.Core
{
    public interface IRepository<T> where T : class
    {
        T FindOne(Expression<Func<T, bool>> predicate);

        T FindOne(Expression<Func<T, bool>> predicate, List<string> includeProperties);

        bool Exists(Expression<Func<T, bool>> predicate);

        IQueryable<T> AsQueryable();

        IQueryable<T> Where(Expression<Func<T, bool>> predicate);

        int Count();

        int Count(Expression<Func<T, bool>> predicate);

        int Save(T entity);

        void Insert(T entity);

        int SaveAll(List<T> entity);
        int UpdateAll(List<T> entities);
        int Recreate(List<T> entity);

        int Recreate(List<T> oldEntities, List<T> newEntities);

        int SaveChanges();

        bool Delete(T entity);

        int Delete(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Add entity to DbSet
        /// </summary>
        /// <param name="entity">Entity to add in DbSet</param>
        void Add(T entity);

        /// <summary>
        /// Add entity collection to DbSet.
        /// </summary>
        /// <param name="entities"></param>
        void AddRange(IEnumerable<T> entities);

        void Remove(T entity);
        int RemoveRange(IEnumerable<T> entities);
        void BlukInsert(IEnumerable<T> entities);
        T FinedOneInclude(Expression<Func<T, bool>> predicate, params string[] include);
        IQueryable<T> FilterWithInclude(Expression<Func<T, bool>> predicate, params string[] include);
        Task<int> SaveAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync();
        Task<int> SaveAllAsync(List<T> entity);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<T> FindOneAsync(Expression<Func<T, bool>> predicate);
        Task<T> FindOneAsync(Expression<Func<T, bool>> predicate, List<string> includeProperties);
        Task<ICollection<T>> FilterAsync(Expression<Func<T, bool>> predicate);
        Task<int> SaveChangeAsync();

    }
}
