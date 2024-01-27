
using Infinity.Bnois.Configuration.Models;
using System;
using System.Configuration;
using System.Data.Entity;


namespace Infinity.Bnois.Configuration.Data
{
    public class ConfigurationDbContext : DbContext, IDisposable
    {
        public ConfigurationDbContext()
        : base(nameOrConnectionString:ConfigurationManager.ConnectionStrings["InfinityIConfigurationEntities"].ConnectionString)

        {
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ValidateOnSaveEnabled = true;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

     
        public DbSet<Module> Modules { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<RoleFeature> RoleFeatures { get; set; }




        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
      
           
         
        }
    }
}