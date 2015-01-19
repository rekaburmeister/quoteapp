namespace QuoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeForeignKeys : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Invoices", name: "Contact_ContactId", newName: "ContactId");
            RenameColumn(table: "dbo.Invoices", name: "Location_WorkLocationId", newName: "WorkLocationId");
            RenameIndex(table: "dbo.Invoices", name: "IX_Location_WorkLocationId", newName: "IX_WorkLocationId");
            RenameIndex(table: "dbo.Invoices", name: "IX_Contact_ContactId", newName: "IX_ContactId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Invoices", name: "IX_ContactId", newName: "IX_Contact_ContactId");
            RenameIndex(table: "dbo.Invoices", name: "IX_WorkLocationId", newName: "IX_Location_WorkLocationId");
            RenameColumn(table: "dbo.Invoices", name: "WorkLocationId", newName: "Location_WorkLocationId");
            RenameColumn(table: "dbo.Invoices", name: "ContactId", newName: "Contact_ContactId");
        }
    }
}
