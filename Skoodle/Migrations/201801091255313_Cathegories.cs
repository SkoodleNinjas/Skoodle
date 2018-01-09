namespace Skoodle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cathegories : DbMigration
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
            
            AddColumn("dbo.Topics", "Category_CathergoryId", c => c.Int());
            CreateIndex("dbo.Topics", "Category_CathergoryId");
            AddForeignKey("dbo.Topics", "Category_CathergoryId", "dbo.Cathegories", "CathergoryId");
            DropColumn("dbo.Topics", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Topics", "Category", c => c.String());
            DropForeignKey("dbo.Topics", "Category_CathergoryId", "dbo.Cathegories");
            DropIndex("dbo.Topics", new[] { "Category_CathergoryId" });
            DropColumn("dbo.Topics", "Category_CathergoryId");
            DropTable("dbo.Cathegories");
        }
    }
}
