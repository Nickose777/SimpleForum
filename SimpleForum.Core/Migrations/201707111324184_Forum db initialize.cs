namespace SimpleForum.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Forumdbinitialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 200),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        SenderId = c.String(nullable: false, maxLength: 128),
                        TopicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserEntities", t => t.SenderId, cascadeDelete: true)
                .ForeignKey("dbo.TopicEntities", t => t.TopicId, cascadeDelete: true)
                .Index(t => t.SenderId)
                .Index(t => t.TopicId);
            
            CreateTable(
                "dbo.TopicEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 200),
                        DateCreated = c.DateTime(nullable: false),
                        DateOfLastMessage = c.DateTime(nullable: false),
                        CreatorId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserEntities", t => t.CreatorId)
                .Index(t => t.CreatorId);
            
            AddColumn("dbo.UserEntities", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MessageEntities", "TopicId", "dbo.TopicEntities");
            DropForeignKey("dbo.TopicEntities", "CreatorId", "dbo.UserEntities");
            DropForeignKey("dbo.MessageEntities", "SenderId", "dbo.UserEntities");
            DropIndex("dbo.TopicEntities", new[] { "CreatorId" });
            DropIndex("dbo.MessageEntities", new[] { "TopicId" });
            DropIndex("dbo.MessageEntities", new[] { "SenderId" });
            DropColumn("dbo.UserEntities", "IsDeleted");
            DropTable("dbo.TopicEntities");
            DropTable("dbo.MessageEntities");
        }
    }
}
