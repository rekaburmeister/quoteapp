namespace QuoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeInvoicedWork : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InvoicedWorks", "Invoice_InvoiceId", "dbo.Invoices");
            DropIndex("dbo.InvoicedWorks", new[] { "Invoice_InvoiceId" });
            CreateTable(
                "dbo.AcceptedWorks",
                c => new
                    {
                        AcceptedWorkkId = c.Int(nullable: false, identity: true),
                        QuoteId = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Price = c.Int(nullable: false),
                        InvoiceId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AcceptedWorkkId)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId)
                .Index(t => t.InvoiceId);
            
            DropTable("dbo.InvoicedWorks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.InvoicedWorks",
                c => new
                    {
                        InvoicedWorkId = c.Int(nullable: false, identity: true),
                        InvoicedWorkDescription = c.String(nullable: false),
                        InvoicedWorkPrice = c.Int(nullable: false),
                        Invoice_InvoiceId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.InvoicedWorkId);
            
            DropForeignKey("dbo.AcceptedWorks", "InvoiceId", "dbo.Invoices");
            DropIndex("dbo.AcceptedWorks", new[] { "InvoiceId" });
            DropTable("dbo.AcceptedWorks");
            CreateIndex("dbo.InvoicedWorks", "Invoice_InvoiceId");
            AddForeignKey("dbo.InvoicedWorks", "Invoice_InvoiceId", "dbo.Invoices", "InvoiceId", cascadeDelete: true);
        }
    }
}
