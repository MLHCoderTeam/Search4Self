namespace Search4Self.DAL
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Searches",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Query = c.String(),
                        Count = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Searches", "UserId", "dbo.Users");
            DropIndex("dbo.Searches", new[] { "UserId" });
            DropTable("dbo.Searches");
        }
    }
}
