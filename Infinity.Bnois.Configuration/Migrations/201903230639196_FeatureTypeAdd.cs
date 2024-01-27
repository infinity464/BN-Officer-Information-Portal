namespace Infinity.Bnois.Configuration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeatureTypeAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("Company.Feature", "FeatureTypeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Company.Feature", "FeatureTypeId");
        }
    }
}
