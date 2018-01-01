namespace Skoodle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DrawingVotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserDrawings", "Votes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserDrawings", "Votes");
        }
    }
}
