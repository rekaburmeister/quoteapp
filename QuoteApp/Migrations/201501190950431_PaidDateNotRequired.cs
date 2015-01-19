namespace QuoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaidDateNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoices", "PaidDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoices", "PaidDate", c => c.DateTime(nullable: false));
        }
    }
}
