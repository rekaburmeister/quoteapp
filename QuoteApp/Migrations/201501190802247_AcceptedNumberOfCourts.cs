namespace QuoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AcceptedNumberOfCourts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuotedWorks", "Accepted", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuotedWorks", "Accepted");
        }
    }
}
