namespace Skoodle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoomsUsersRelationAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Room_RoomId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Room_RoomId");
            AddForeignKey("dbo.AspNetUsers", "Room_RoomId", "dbo.Rooms", "RoomId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Room_RoomId", "dbo.Rooms");
            DropIndex("dbo.AspNetUsers", new[] { "Room_RoomId" });
            DropColumn("dbo.AspNetUsers", "Room_RoomId");
        }
    }
}
