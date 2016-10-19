namespace EmpyreBookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zach_2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Book", "Bi_BIID", "dbo.BI");
            DropIndex("dbo.Book", new[] { "Bi_BIID" });
            RenameColumn(table: "dbo.Book", name: "Bi_BIID", newName: "BIID");
            AlterColumn("dbo.Book", "BIID", c => c.Int(nullable: false));
            CreateIndex("dbo.Book", "BIID");
            AddForeignKey("dbo.Book", "BIID", "dbo.BI", "BIID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Book", "BIID", "dbo.BI");
            DropIndex("dbo.Book", new[] { "BIID" });
            AlterColumn("dbo.Book", "BIID", c => c.Int());
            RenameColumn(table: "dbo.Book", name: "BIID", newName: "Bi_BIID");
            CreateIndex("dbo.Book", "Bi_BIID");
            AddForeignKey("dbo.Book", "Bi_BIID", "dbo.BI", "BIID");
        }
    }
}
