namespace EmpyreBookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zach417_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "BI_BIID", c => c.Int());
            CreateIndex("dbo.Book", "BI_BIID");
            AddForeignKey("dbo.Book", "BI_BIID", "dbo.BI", "BIID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Book", "BI_BIID", "dbo.BI");
            DropIndex("dbo.Book", new[] { "BI_BIID" });
            DropColumn("dbo.Book", "BI_BIID");
        }
    }
}
