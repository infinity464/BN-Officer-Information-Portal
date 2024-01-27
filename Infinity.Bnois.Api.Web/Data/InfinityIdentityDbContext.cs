using System.Data.Entity;

using Microsoft.AspNet.Identity.EntityFramework;
using Infinity.Bnois.Api.Web.Models;
using System.Configuration;

namespace Infinity.Bnois.Api.Web.Data
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'InfinityIdentityDbContext'
    public class InfinityIdentityDbContext : IdentityDbContext<User, Role, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'InfinityIdentityDbContext'
    {
        private readonly string schemaName;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'InfinityIdentityDbContext.InfinityIdentityDbContext()'
        public InfinityIdentityDbContext()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'InfinityIdentityDbContext.InfinityIdentityDbContext()'
          : base(nameOrConnectionString: ConfigurationManager.ConnectionStrings["InfinityIdentityEntities"].ConnectionString)
        {
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'InfinityIdentityDbContext.InfinityIdentityDbContext(string)'
        public InfinityIdentityDbContext(string schemaName)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'InfinityIdentityDbContext.InfinityIdentityDbContext(string)'
          : base(nameOrConnectionString: ConfigurationManager.ConnectionStrings["InfinityIdentityEntities"].ConnectionString)
        {
            this.schemaName = schemaName;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'InfinityIdentityDbContext.Languages'
        public virtual DbSet<Language> Languages { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'InfinityIdentityDbContext.Languages'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'InfinityIdentityDbContext.OnModelCreating(DbModelBuilder)'
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'InfinityIdentityDbContext.OnModelCreating(DbModelBuilder)'
        {
            base.OnModelCreating(modelBuilder);
            // You can globally assign schema here
            if (!string.IsNullOrWhiteSpace("Administration"))
            {
                modelBuilder.HasDefaultSchema("Administration");
            }
        }
    }
}