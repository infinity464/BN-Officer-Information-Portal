namespace Infinity.Bnois.Configuration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsReportAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("Company.Feature", "IsReport", c => c.Boolean(nullable: false));
            AddColumn("Company.Module", "IsReport", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Company.Module", "IsReport");
            DropColumn("Company.Feature", "IsReport");
        }
    }
}
