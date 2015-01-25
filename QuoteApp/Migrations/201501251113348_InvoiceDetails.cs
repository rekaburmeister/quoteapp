namespace QuoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvoiceDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvoiceDetails",
                c => new
                    {
                        DetailId = c.String(nullable: false, maxLength: 128),
                        InvoiceId = c.String(nullable: false, maxLength: 128),
                        Description = c.String(nullable: false),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DetailId)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .Index(t => t.InvoiceId);
            
            DropColumn("dbo.Invoices", "Details");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "Details", c => c.String(nullable: false));
            DropForeignKey("dbo.InvoiceDetails", "InvoiceId", "dbo.Invoices");
            DropIndex("dbo.InvoiceDetails", new[] { "InvoiceId" });
            DropTable("dbo.InvoiceDetails");
        }
    }
}
