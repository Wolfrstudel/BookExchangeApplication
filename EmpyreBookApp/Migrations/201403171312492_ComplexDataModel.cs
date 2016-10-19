namespace EmpyreBookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComplexDataModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        BookID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        SoldTo = c.String(),
                        ISBN = c.String(),
                        Condition = c.String(),
                        Photo = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ContactInfo = c.String(),
                    })
                .PrimaryKey(t => t.BookID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Trade",
                c => new
                    {
                        TradeID = c.Int(nullable: false, identity: true),
                        SellerID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Review = c.String(),
                        Credit = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TradeID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trade", "UserID", "dbo.User");
            DropForeignKey("dbo.Book", "UserID", "dbo.User");
            DropIndex("dbo.Trade", new[] { "UserID" });
            DropIndex("dbo.Book", new[] { "UserID" });
            DropTable("dbo.Trade");
            DropTable("dbo.Book");
        }
    }
}
