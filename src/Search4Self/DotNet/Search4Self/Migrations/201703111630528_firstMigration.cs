namespace Search4Self.DAL
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MusicGenre",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Genre = c.String(),
                        Hits = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Session = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Session);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MusicGenre", "UserId", "dbo.Users");
            DropIndex("dbo.Users", new[] { "Session" });
            DropIndex("dbo.MusicGenre", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.MusicGenre");
        }
    }
}
