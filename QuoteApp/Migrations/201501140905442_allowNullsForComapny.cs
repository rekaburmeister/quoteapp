namespace QuoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allowNullsForComapny : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contacts", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Contacts", new[] { "CompanyId" });
            AlterColumn("dbo.Contacts", "CompanyId", c => c.Int());
            CreateIndex("dbo.Contacts", "CompanyId");
            AddForeignKey("dbo.Contacts", "CompanyId", "dbo.Companies", "CompanyId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contacts", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Contacts", new[] { "CompanyId" });
            AlterColumn("dbo.Contacts", "CompanyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Contacts", "CompanyId");
            AddForeignKey("dbo.Contacts", "CompanyId", "dbo.Companies", "CompanyId", cascadeDelete: true);
        }
    }
}
