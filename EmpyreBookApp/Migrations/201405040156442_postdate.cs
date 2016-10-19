namespace EmpyreBookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "PostDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Book", "PostDate");
        }
    }
}
