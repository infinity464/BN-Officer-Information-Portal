namespace Infinity.Bnois.Configuration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfigurationDbContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Company.Feature",
                c => new
                    {
                        FeatureId = c.Int(nullable: false, identity: true),
                        FeatureName = c.String(),
                        ActionNgHref = c.String(),
                        ModuleId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Guid(),
                        EditedDate = c.DateTime(),
                        EditedBy = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FeatureId)
                .ForeignKey("Company.Module", t => t.ModuleId, cascadeDelete: true)
                .Index(t => t.ModuleId);
            
            CreateTable(
                "Company.Module",
                c => new
                    {
                        ModuleId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Guid(),
                        EditedDate = c.DateTime(),
                        EditedBy = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ModuleId);
            
            CreateTable(
                "Company.RoleFeature",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 36),
                        FeatureKey = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.FeatureKey });
            
        }
        
        public override void Down()
        {
            DropForeignKey("Company.Feature", "ModuleId", "Company.Module");
            DropIndex("Company.Feature", new[] { "ModuleId" });
            DropTable("Company.RoleFeature");
            DropTable("Company.Module");
            DropTable("Company.Feature");
        }
    }
}
