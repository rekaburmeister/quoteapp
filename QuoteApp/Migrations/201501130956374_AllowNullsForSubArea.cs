namespace QuoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllowNullsForSubArea : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.QuotedWorks", "QuotedWorkSubAreaName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.QuotedWorks", "QuotedWorkSubAreaName", c => c.String(nullable: false));
        }
    }
}
