namespace QuoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignKeysInQuotes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Quotes", "Location_WorkLocationId", "dbo.WorkLocations");
            DropIndex("dbo.Quotes", new[] { "Location_WorkLocationId" });
            RenameColumn(table: "dbo.Quotes", name: "Contact_ContactId", newName: "ContactId");
            RenameIndex(table: "dbo.Quotes", name: "IX_Contact_ContactId", newName: "IX_ContactId");
            AddColumn("dbo.Quotes", "LocationId", c => c.Int(nullable: false));
            AlterColumn("dbo.Quotes", "Location_WorkLocationId", c => c.Int());
            CreateIndex("dbo.Quotes", "Location_WorkLocationId");
            AddForeignKey("dbo.Quotes", "Location_WorkLocationId", "dbo.WorkLocations", "WorkLocationId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quotes", "Location_WorkLocationId", "dbo.WorkLocations");
            DropIndex("dbo.Quotes", new[] { "Location_WorkLocationId" });
            AlterColumn("dbo.Quotes", "Location_WorkLocationId", c => c.Int(nullable: false));
            DropColumn("dbo.Quotes", "LocationId");
            RenameIndex(table: "dbo.Quotes", name: "IX_ContactId", newName: "IX_Contact_ContactId");
            RenameColumn(table: "dbo.Quotes", name: "ContactId", newName: "Contact_ContactId");
            CreateIndex("dbo.Quotes", "Location_WorkLocationId");
            AddForeignKey("dbo.Quotes", "Location_WorkLocationId", "dbo.WorkLocations", "WorkLocationId", cascadeDelete: true);
        }
    }
}
