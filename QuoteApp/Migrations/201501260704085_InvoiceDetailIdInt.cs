namespace QuoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvoiceDetailIdInt : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.InvoiceDetails");
            AlterColumn("dbo.InvoiceDetails", "DetailId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.InvoiceDetails", "DetailId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.InvoiceDetails");
            AlterColumn("dbo.InvoiceDetails", "DetailId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.InvoiceDetails", "DetailId");
        }
    }
}
