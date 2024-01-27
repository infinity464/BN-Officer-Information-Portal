namespace Infinity.Bnois.Api.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserInitialCreate'
    public partial class UserInitialCreate : DbMigration
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserInitialCreate'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserInitialCreate.Up()'
        public override void Up()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserInitialCreate.Up()'
        {
            AddColumn("Administration.AspNetUsers", "CreatedBy", c => c.String());
            AddColumn("Administration.AspNetUsers", "CreatedDate", c => c.DateTime());
            AddColumn("Administration.AspNetUsers", "InActiveBy", c => c.String());
            AddColumn("Administration.AspNetUsers", "InActiveDate", c => c.DateTime());
        }
        
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserInitialCreate.Down()'
        public override void Down()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserInitialCreate.Down()'
        {
            DropColumn("Administration.AspNetUsers", "InActiveDate");
            DropColumn("Administration.AspNetUsers", "InActiveBy");
            DropColumn("Administration.AspNetUsers", "CreatedDate");
            DropColumn("Administration.AspNetUsers", "CreatedBy");
        }
    }
}
