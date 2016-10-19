namespace EmpyreBookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zach_ : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BI", "Title", c => c.String());
            AddColumn("dbo.BI", "Authors", c => c.String());
            AddColumn("dbo.BI", "CoverLink", c => c.String());
            AddColumn("dbo.BI", "Version", c => c.String());
            AddColumn("dbo.Book", "genre", c => c.String());
            AddColumn("dbo.Book", "Bi_BIID", c => c.Int());
            CreateIndex("dbo.Book", "Bi_BIID");
            AddForeignKey("dbo.Book", "Bi_BIID", "dbo.BI", "BIID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Book", "Bi_BIID", "dbo.BI");
            DropIndex("dbo.Book", new[] { "Bi_BIID" });
            DropColumn("dbo.Book", "Bi_BIID");
            DropColumn("dbo.Book", "genre");
            DropColumn("dbo.BI", "Version");
            DropColumn("dbo.BI", "CoverLink");
            DropColumn("dbo.BI", "Authors");
            DropColumn("dbo.BI", "Title");
        }
    }
}
