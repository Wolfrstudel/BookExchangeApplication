namespace EmpyreBookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigrationtitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "Title", c => c.String());
            AddColumn("dbo.Book", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Book", "Description");
            DropColumn("dbo.Book", "Title");
        }
    }
}
