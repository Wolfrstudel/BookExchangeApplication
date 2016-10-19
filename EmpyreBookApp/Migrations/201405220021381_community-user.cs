namespace EmpyreBookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class communityuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "ComunityID", c => c.Int(nullable: false));
            AddColumn("dbo.User", "community_CommunityID", c => c.Int());
            CreateIndex("dbo.User", "community_CommunityID");
            AddForeignKey("dbo.User", "community_CommunityID", "dbo.Community", "CommunityID");
            DropColumn("dbo.User", "School");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "School", c => c.String(nullable: false));
            DropForeignKey("dbo.User", "community_CommunityID", "dbo.Community");
            DropIndex("dbo.User", new[] { "community_CommunityID" });
            DropColumn("dbo.User", "community_CommunityID");
            DropColumn("dbo.User", "ComunityID");
        }
    }
}
