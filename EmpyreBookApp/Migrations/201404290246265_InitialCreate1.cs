namespace EmpyreBookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Community",
                c => new
                    {
                        CommunityID = c.Int(nullable: false, identity: true),
                        CName = c.String(),
                        CShortName = c.String(),
                        CUrl = c.String(),
                        BackgroundColor = c.String(),
                        SecondaryBackgroundColor = c.String(),
                        Picture = c.Binary(),
                    })
                .PrimaryKey(t => t.CommunityID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Community");
        }
    }
}
