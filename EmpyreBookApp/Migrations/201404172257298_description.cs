namespace EmpyreBookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class description : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Book", "Price", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Book", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
