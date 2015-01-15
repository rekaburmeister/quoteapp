namespace QuoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scheduleWorkAllowNulls : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Quotes", "ScheduledFor", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Quotes", "ScheduledFor", c => c.DateTime(nullable: false));
        }
    }
}
