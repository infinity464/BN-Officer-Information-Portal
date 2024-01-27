namespace Infinity.Bnois.Api.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'InitialCreate'
    public partial class InitialCreate : DbMigration
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'InitialCreate'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'InitialCreate.Up()'
        public override void Up()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'InitialCreate.Up()'
        {
            CreateTable(
                "Administration.Languages",
                c => new
                    {
                        CultureCode = c.String(nullable: false, maxLength: 5),
                        DisplayName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.CultureCode);
            
            CreateTable(
                "Administration.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CompanyId = c.String(maxLength: 36),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "Administration.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("Administration.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("Administration.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "Administration.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        CultureCode = c.String(nullable: false, maxLength: 10),
                        CompanyId = c.String(maxLength: 36),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "Administration.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Administration.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "Administration.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("Administration.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'InitialCreate.Down()'
        public override void Down()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'InitialCreate.Down()'
        {
            DropForeignKey("Administration.AspNetUserRoles", "UserId", "Administration.AspNetUsers");
            DropForeignKey("Administration.AspNetUserLogins", "UserId", "Administration.AspNetUsers");
            DropForeignKey("Administration.AspNetUserClaims", "UserId", "Administration.AspNetUsers");
            DropForeignKey("Administration.AspNetUserRoles", "RoleId", "Administration.AspNetRoles");
            DropIndex("Administration.AspNetUserLogins", new[] { "UserId" });
            DropIndex("Administration.AspNetUserClaims", new[] { "UserId" });
            DropIndex("Administration.AspNetUsers", "UserNameIndex");
            DropIndex("Administration.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("Administration.AspNetUserRoles", new[] { "UserId" });
            DropIndex("Administration.AspNetRoles", "RoleNameIndex");
            DropTable("Administration.AspNetUserLogins");
            DropTable("Administration.AspNetUserClaims");
            DropTable("Administration.AspNetUsers");
            DropTable("Administration.AspNetUserRoles");
            DropTable("Administration.AspNetRoles");
            DropTable("Administration.Languages");
        }
    }
}
