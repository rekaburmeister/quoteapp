namespace QuoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvoiceRedo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Price", c => c.Int(nullable: false));
            AddColumn("dbo.Invoices", "Details", c => c.String(nullable: false));
            DropColumn("dbo.Invoices", "InvoiceNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "InvoiceNumber", c => c.String(nullable: false));
            DropColumn("dbo.Invoices", "Details");
            DropColumn("dbo.Invoices", "Price");
        }
    }
}
