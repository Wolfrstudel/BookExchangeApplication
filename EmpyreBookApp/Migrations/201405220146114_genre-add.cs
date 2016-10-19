namespace EmpyreBookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class genreadd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        GenreID = c.Int(nullable: false, identity: true),
                        CommunityID = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.GenreID);
            
            AddColumn("dbo.Book", "GenreID", c => c.Int(nullable: false));
            CreateIndex("dbo.Book", "GenreID");
            AddForeignKey("dbo.Book", "GenreID", "dbo.Genre", "GenreID", cascadeDelete: true);
            DropColumn("dbo.Book", "Genre");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Book", "Genre", c => c.String());
            DropForeignKey("dbo.Book", "GenreID", "dbo.Genre");
            DropIndex("dbo.Book", new[] { "GenreID" });
            DropColumn("dbo.Book", "GenreID");
            DropTable("dbo.Genre");
        }
    }
}
