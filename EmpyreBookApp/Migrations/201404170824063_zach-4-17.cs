namespace EmpyreBookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zach417 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Book", "BIID", "dbo.BI");
            DropIndex("dbo.Book", new[] { "BIID" });
            DropColumn("dbo.Book", "BIID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Book", "BIID", c => c.Int(nullable: false));
            CreateIndex("dbo.Book", "BIID");
            AddForeignKey("dbo.Book", "BIID", "dbo.BI", "BIID", cascadeDelete: true);
        }
    }
}
