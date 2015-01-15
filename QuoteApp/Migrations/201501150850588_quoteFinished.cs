namespace QuoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quoteFinished : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotes", "Finished", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quotes", "Finished");
        }
    }
}
