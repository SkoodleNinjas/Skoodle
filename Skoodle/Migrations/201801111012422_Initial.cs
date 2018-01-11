namespace Skoodle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cathegories",
                c => new
                    {
                        CathergoryId = c.Int(nullable: false, identity: true),
                        CathegoryName = c.String(),
                    })
                .PrimaryKey(t => t.CathergoryId);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.Binary(),
                        Category_CathergoryId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cathegories", t => t.Category_CathergoryId)
                .Index(t => t.Category_CathergoryId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        IsGameAlive = c.Boolean(nullable: false),
                        PlayedTime = c.DateTime(nullable: false),
                        Room_RoomId = c.Int(),
                        WinnerUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.GameId)
                .ForeignKey("dbo.Rooms", t => t.Room_RoomId)
                .ForeignKey("dbo.AspNetUsers", t => t.WinnerUser_Id)
                .Index(t => t.Room_RoomId)
                .Index(t => t.WinnerUser_Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        RoomName = c.String(),
                        MaxPlayers = c.Int(nullable: false),
                        MaxRounds = c.Int(nullable: false),
                        Cathegory_CathergoryId = c.Int(),
                    })
                .PrimaryKey(t => t.RoomId)
                .ForeignKey("dbo.Cathegories", t => t.Cathegory_CathergoryId)
                .Index(t => t.Cathegory_CathergoryId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Room_RoomId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.Room_RoomId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Room_RoomId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                        Votes = c.Int(nullable: false),
                        FileName = c.String(),
                        User_Id = c.String(maxLength: 128),
                        Round_RoundId = c.Int(),
                    })
                .PrimaryKey(t => t.UserDrawingId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.Rounds", t => t.Round_RoundId)
                .Index(t => t.User_Id)
                .Index(t => t.Round_RoundId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Games", "WinnerUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Rounds", "Game_GameId", "dbo.Games");
            DropForeignKey("dbo.UserDrawings", "Round_RoundId", "dbo.Rounds");
            DropForeignKey("dbo.UserDrawings", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Games", "Room_RoomId", "dbo.Rooms");
            DropForeignKey("dbo.AspNetUsers", "Room_RoomId", "dbo.Rooms");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Rooms", "Cathegory_CathergoryId", "dbo.Cathegories");
            DropForeignKey("dbo.Topics", "Category_CathergoryId", "dbo.Cathegories");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.UserDrawings", new[] { "Round_RoundId" });
            DropIndex("dbo.UserDrawings", new[] { "User_Id" });
            DropIndex("dbo.Rounds", new[] { "Game_GameId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Room_RoomId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Rooms", new[] { "Cathegory_CathergoryId" });
            DropIndex("dbo.Games", new[] { "WinnerUser_Id" });
            DropIndex("dbo.Games", new[] { "Room_RoomId" });
            DropIndex("dbo.Topics", new[] { "Category_CathergoryId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.UserDrawings");
            DropTable("dbo.Rounds");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Rooms");
            DropTable("dbo.Games");
            DropTable("dbo.Topics");
            DropTable("dbo.Cathegories");
        }
    }
}
