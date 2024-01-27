namespace Infinity.Bnois.Configuration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoleFeatureAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("Company.RoleFeature", "Add", c => c.Boolean(nullable: false));
            AddColumn("Company.RoleFeature", "Update", c => c.Boolean(nullable: false));
            AddColumn("Company.RoleFeature", "Delete", c => c.Boolean(nullable: false));
            AddColumn("Company.RoleFeature", "Report", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Company.RoleFeature", "Report");
            DropColumn("Company.RoleFeature", "Delete");
            DropColumn("Company.RoleFeature", "Update");
            DropColumn("Company.RoleFeature", "Add");
        }
    }
}
