namespace EmpyreBookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _51214 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Password", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.User", "Email", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "Email", c => c.String(nullable: false));
            DropColumn("dbo.User", "Password");
        }
    }
}
