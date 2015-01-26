namespace QuoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReferenceInInvoice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Reference", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "Reference");
        }
    }
}
