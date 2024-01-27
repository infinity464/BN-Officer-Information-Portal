namespace Infinity.Bnois.Api.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserEntityUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("Administration.AspNetUsers", "PNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Administration.AspNetUsers", "PNo");
        }
    }
}
