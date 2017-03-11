namespace Search4Self.DAL
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_youtube_search_history_model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.YoutubeSearchHistory",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Word = c.String(),
                        Count = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.YoutubeSearchHistory", "UserId", "dbo.Users");
            DropIndex("dbo.YoutubeSearchHistory", new[] { "UserId" });
            DropTable("dbo.YoutubeSearchHistory");
        }
    }
}
