namespace Infinity.Bnois.Configuration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFeatureCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("Company.Feature", "FeatureCode", c => c.Int(nullable: false));
            AddColumn("Company.Feature", "OrderNo", c => c.Int(nullable: false));
            AddColumn("Company.Module", "OrderNo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Company.Module", "OrderNo");
            DropColumn("Company.Feature", "OrderNo");
            DropColumn("Company.Feature", "FeatureCode");
        }
    }
}
