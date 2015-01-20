namespace QuoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingExtraFieldsToAcceptedWork : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AcceptedWorks", "WorkType", c => c.String());
            AddColumn("dbo.AcceptedWorks", "CourtName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AcceptedWorks", "CourtName");
            DropColumn("dbo.AcceptedWorks", "WorkType");
        }
    }
}
