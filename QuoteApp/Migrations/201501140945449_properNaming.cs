namespace QuoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class properNaming : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Quotes", "Location_WorkLocationId", "dbo.WorkLocations");
            DropIndex("dbo.Quotes", new[] { "Location_WorkLocationId" });
            RenameColumn(table: "dbo.Quotes", name: "Location_WorkLocationId", newName: "WorkLocationId");
            AlterColumn("dbo.Quotes", "WorkLocationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Quotes", "WorkLocationId");
            AddForeignKey("dbo.Quotes", "WorkLocationId", "dbo.WorkLocations", "WorkLocationId", cascadeDelete: true);
            DropColumn("dbo.Quotes", "LocationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Quotes", "LocationId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Quotes", "WorkLocationId", "dbo.WorkLocations");
            DropIndex("dbo.Quotes", new[] { "WorkLocationId" });
            AlterColumn("dbo.Quotes", "WorkLocationId", c => c.Int());
            RenameColumn(table: "dbo.Quotes", name: "WorkLocationId", newName: "Location_WorkLocationId");
            CreateIndex("dbo.Quotes", "Location_WorkLocationId");
            AddForeignKey("dbo.Quotes", "Location_WorkLocationId", "dbo.WorkLocations", "WorkLocationId");
        }
    }
}
