namespace QuoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AcceptedNumberOfCourtsNotNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.QuotedWorks", "Accepted", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.QuotedWorks", "Accepted", c => c.Int());
        }
    }
}
