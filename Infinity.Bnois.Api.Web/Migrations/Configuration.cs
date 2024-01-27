namespace Infinity.Bnois.Api.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Infinity.Bnois.Api.Web.Data.InfinityIdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Infinity.Bnois.Api.Web.Data.InfinityIdentityDbContext";
        }

        protected override void Seed(Infinity.Bnois.Api.Web.Data.InfinityIdentityDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
