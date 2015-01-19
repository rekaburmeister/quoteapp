namespace QuoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvoicePaidDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "PaidDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "PaidDate");
        }
    }
}
