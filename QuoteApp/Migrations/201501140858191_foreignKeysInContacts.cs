namespace QuoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignKeysInContacts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contacts", "Company_CompanyId", "dbo.Companies");
            DropIndex("dbo.Contacts", new[] { "Company_CompanyId" });
            RenameColumn(table: "dbo.Contacts", name: "Company_CompanyId", newName: "CompanyId");
            AlterColumn("dbo.Contacts", "CompanyId", c => c.Int(nullable: true));
            CreateIndex("dbo.Contacts", "CompanyId");
            AddForeignKey("dbo.Contacts", "CompanyId", "dbo.Companies", "CompanyId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contacts", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Contacts", new[] { "CompanyId" });
            AlterColumn("dbo.Contacts", "CompanyId", c => c.Int());
            RenameColumn(table: "dbo.Contacts", name: "CompanyId", newName: "Company_CompanyId");
            CreateIndex("dbo.Contacts", "Company_CompanyId");
            AddForeignKey("dbo.Contacts", "Company_CompanyId", "dbo.Companies", "CompanyId");
        }
    }
}
