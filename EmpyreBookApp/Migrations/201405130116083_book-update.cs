namespace EmpyreBookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookupdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Book", "BI_BIID", "dbo.BI");
            DropIndex("dbo.Book", new[] { "BI_BIID" });
            RenameColumn(table: "dbo.Book", name: "BI_BIID", newName: "BIID");
            AlterColumn("dbo.Book", "BIID", c => c.Int(nullable: false));
            CreateIndex("dbo.Book", "BIID");
            AddForeignKey("dbo.Book", "BIID", "dbo.BI", "BIID", cascadeDelete: true);
            DropColumn("dbo.Book", "Title");
            DropColumn("dbo.Book", "ContactInfo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Book", "ContactInfo", c => c.String());
            AddColumn("dbo.Book", "Title", c => c.String());
            DropForeignKey("dbo.Book", "BIID", "dbo.BI");
            DropIndex("dbo.Book", new[] { "BIID" });
            AlterColumn("dbo.Book", "BIID", c => c.Int());
            RenameColumn(table: "dbo.Book", name: "BIID", newName: "BI_BIID");
            CreateIndex("dbo.Book", "BI_BIID");
            AddForeignKey("dbo.Book", "BI_BIID", "dbo.BI", "BIID");
        }
    }
}
