namespace QuoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class archiveQuote : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotes", "Archived", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quotes", "Archived");
        }
    }
}
