namespace Skoodle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GameStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "IsGameAlive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Games", "WinnerUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Games", "WinnerUser_Id");
            AddForeignKey("dbo.Games", "WinnerUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "WinnerUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Games", new[] { "WinnerUser_Id" });
            DropColumn("dbo.Games", "WinnerUser_Id");
            DropColumn("dbo.Games", "IsGameAlive");
        }
    }
}
