namespace EmpyreBookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BI",
                c => new
                    {
                        BIID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        ISBN = c.String(),
                    })
                .PrimaryKey(t => t.BIID);
            
            CreateTable(
                "dbo.Request",
                c => new
                    {
                        RequestID = c.Int(nullable: false, identity: true),
                        BIID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Preferance = c.String(),
                        ISBN = c.String(),
                        RequestDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RequestID)
                .ForeignKey("dbo.BI", t => t.BIID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.BIID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 60),
                        Email = c.String(nullable: false),
                        School = c.String(nullable: false),
                        Contact = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Request", "UserID", "dbo.User");
            DropForeignKey("dbo.Request", "BIID", "dbo.BI");
            DropIndex("dbo.Request", new[] { "UserID" });
            DropIndex("dbo.Request", new[] { "BIID" });
            DropTable("dbo.User");
            DropTable("dbo.Request");
            DropTable("dbo.BI");
        }
    }
}
