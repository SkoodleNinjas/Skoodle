namespace Skoodle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRoundsGamesUserDrawings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        PlayedTime = c.DateTime(nullable: false),
                        Room_RoomId = c.Int(),
                    })
                .PrimaryKey(t => t.GameId)
                .ForeignKey("dbo.Rooms", t => t.Room_RoomId)
                .Index(t => t.Room_RoomId);
            
            CreateTable(
                "dbo.Rounds",
                c => new
                    {
                        RoundId = c.Int(nullable: false, identity: true),
                        RoundNum = c.Int(nullable: false),
                        Game_GameId = c.Int(),
                    })
                .PrimaryKey(t => t.RoundId)
                .ForeignKey("dbo.Games", t => t.Game_GameId)
                .Index(t => t.Game_GameId);
            
            CreateTable(
                "dbo.UserDrawings",
                c => new
                    {
                        UserDrawingId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        User_Id = c.String(maxLength: 128),
                        Round_RoundId = c.Int(),
                    })
                .PrimaryKey(t => t.UserDrawingId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.Rounds", t => t.Round_RoundId)
                .Index(t => t.User_Id)
                .Index(t => t.Round_RoundId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rounds", "Game_GameId", "dbo.Games");
            DropForeignKey("dbo.UserDrawings", "Round_RoundId", "dbo.Rounds");
            DropForeignKey("dbo.UserDrawings", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Games", "Room_RoomId", "dbo.Rooms");
            DropIndex("dbo.UserDrawings", new[] { "Round_RoundId" });
            DropIndex("dbo.UserDrawings", new[] { "User_Id" });
            DropIndex("dbo.Rounds", new[] { "Game_GameId" });
            DropIndex("dbo.Games", new[] { "Room_RoomId" });
            DropTable("dbo.UserDrawings");
            DropTable("dbo.Rounds");
            DropTable("dbo.Games");
        }
    }
}
