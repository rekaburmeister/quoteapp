namespace QuoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scheduleWork : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotes", "ScheduledFor", c => c.DateTime(nullable: false));
            AddColumn("dbo.Quotes", "JobLength", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quotes", "JobLength");
            DropColumn("dbo.Quotes", "ScheduledFor");
        }
    }
}
