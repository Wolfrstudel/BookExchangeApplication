namespace EmpyreBookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class exit : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BI", "UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BI", "UserID", c => c.Int(nullable: false));
        }
    }
}
