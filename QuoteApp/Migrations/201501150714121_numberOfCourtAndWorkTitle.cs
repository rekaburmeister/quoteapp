namespace QuoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class numberOfCourtAndWorkTitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuotedWorks", "WorkTitle", c => c.String());
            AddColumn("dbo.QuotedWorks", "NumberOfCourts", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuotedWorks", "NumberOfCourts");
            DropColumn("dbo.QuotedWorks", "WorkTitle");
        }
    }
}
