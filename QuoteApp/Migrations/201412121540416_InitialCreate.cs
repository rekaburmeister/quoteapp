namespace QuoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Address3 = c.String(),
                        Town = c.String(),
                        Country = c.String(),
                        PostCode = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        MiddleName = c.String(),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        MobileNumber = c.String(),
                        PhoneNumber = c.String(),
                        Company_CompanyId = c.Int(),
                    })
                .PrimaryKey(t => t.ContactId)
                .ForeignKey("dbo.Companies", t => t.Company_CompanyId)
                .Index(t => t.Company_CompanyId);
            
            CreateTable(
                "dbo.WorkLocations",
                c => new
                    {
                        WorkLocationId = c.Int(nullable: false, identity: true),
                        WorkLocationName = c.String(nullable: false),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Address3 = c.String(),
                        Town = c.String(),
                        Country = c.String(),
                        PostCode = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        Company_CompanyId = c.Int(),
                    })
                .PrimaryKey(t => t.WorkLocationId)
                .ForeignKey("dbo.Companies", t => t.Company_CompanyId)
                .Index(t => t.Company_CompanyId);
            
            CreateTable(
                "dbo.InvoicedWorks",
                c => new
                    {
                        InvoicedWorkId = c.Int(nullable: false, identity: true),
                        InvoicedWorkDescription = c.String(nullable: false),
                        InvoicedWorkPrice = c.Int(nullable: false),
                        Invoice_InvoiceId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.InvoicedWorkId)
                .ForeignKey("dbo.Invoices", t => t.Invoice_InvoiceId, cascadeDelete: true)
                .Index(t => t.Invoice_InvoiceId);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceId = c.String(nullable: false, maxLength: 128),
                        InvoiceNumber = c.String(nullable: false),
                        InvoiceDate = c.DateTime(nullable: false),
                        Contact_ContactId = c.Int(nullable: false),
                        Location_WorkLocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InvoiceId)
                .ForeignKey("dbo.Contacts", t => t.Contact_ContactId, cascadeDelete: true)
                .ForeignKey("dbo.WorkLocations", t => t.Location_WorkLocationId, cascadeDelete: true)
                .Index(t => t.Contact_ContactId)
                .Index(t => t.Location_WorkLocationId);
            
            CreateTable(
                "dbo.QuotedWorks",
                c => new
                    {
                        QuotedWorkId = c.Int(nullable: false, identity: true),
                        QuotedWorkMainAreaName = c.String(nullable: false),
                        QuotedWorkSubAreaName = c.String(nullable: false),
                        QuotedWorkDescription = c.String(nullable: false),
                        QuotedWorkPrice = c.Int(nullable: false),
                        Quote_QuoteId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.QuotedWorkId)
                .ForeignKey("dbo.Quotes", t => t.Quote_QuoteId, cascadeDelete: true)
                .Index(t => t.Quote_QuoteId);
            
            CreateTable(
                "dbo.Quotes",
                c => new
                    {
                        QuoteId = c.String(nullable: false, maxLength: 128),
                        QuoteDate = c.DateTime(nullable: false),
                        Contact_ContactId = c.Int(nullable: false),
                        Location_WorkLocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuoteId)
                .ForeignKey("dbo.Contacts", t => t.Contact_ContactId, cascadeDelete: true)
                .ForeignKey("dbo.WorkLocations", t => t.Location_WorkLocationId, cascadeDelete: true)
                .Index(t => t.Contact_ContactId)
                .Index(t => t.Location_WorkLocationId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.IdentityUser_Id)
                .Index(t => t.RoleId)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.WorkAreas",
                c => new
                    {
                        WorkAreaId = c.Int(nullable: false, identity: true),
                        WorkAreaName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.WorkAreaId);
            
            CreateTable(
                "dbo.Works",
                c => new
                    {
                        WorkId = c.Int(nullable: false, identity: true),
                        WorkName = c.String(nullable: false),
                        WorkDescription = c.String(nullable: false),
                        WorkPrice = c.Int(nullable: false),
                        WorkArea_WorkAreaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WorkId)
                .ForeignKey("dbo.WorkAreas", t => t.WorkArea_WorkAreaId, cascadeDelete: true)
                .Index(t => t.WorkArea_WorkAreaId);
            
            CreateTable(
                "dbo.WorkLocationContacts",
                c => new
                    {
                        WorkLocation_WorkLocationId = c.Int(nullable: false),
                        Contact_ContactId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkLocation_WorkLocationId, t.Contact_ContactId })
                .ForeignKey("dbo.WorkLocations", t => t.WorkLocation_WorkLocationId, cascadeDelete: true)
                .ForeignKey("dbo.Contacts", t => t.Contact_ContactId, cascadeDelete: true)
                .Index(t => t.WorkLocation_WorkLocationId)
                .Index(t => t.Contact_ContactId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.AspNetUserLogins", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.AspNetUserClaims", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.Works", "WorkArea_WorkAreaId", "dbo.WorkAreas");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.QuotedWorks", "Quote_QuoteId", "dbo.Quotes");
            DropForeignKey("dbo.Quotes", "Location_WorkLocationId", "dbo.WorkLocations");
            DropForeignKey("dbo.Quotes", "Contact_ContactId", "dbo.Contacts");
            DropForeignKey("dbo.InvoicedWorks", "Invoice_InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "Location_WorkLocationId", "dbo.WorkLocations");
            DropForeignKey("dbo.Invoices", "Contact_ContactId", "dbo.Contacts");
            DropForeignKey("dbo.WorkLocationContacts", "Contact_ContactId", "dbo.Contacts");
            DropForeignKey("dbo.WorkLocationContacts", "WorkLocation_WorkLocationId", "dbo.WorkLocations");
            DropForeignKey("dbo.WorkLocations", "Company_CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Contacts", "Company_CompanyId", "dbo.Companies");
            DropIndex("dbo.WorkLocationContacts", new[] { "Contact_ContactId" });
            DropIndex("dbo.WorkLocationContacts", new[] { "WorkLocation_WorkLocationId" });
            DropIndex("dbo.Works", new[] { "WorkArea_WorkAreaId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Quotes", new[] { "Location_WorkLocationId" });
            DropIndex("dbo.Quotes", new[] { "Contact_ContactId" });
            DropIndex("dbo.QuotedWorks", new[] { "Quote_QuoteId" });
            DropIndex("dbo.Invoices", new[] { "Location_WorkLocationId" });
            DropIndex("dbo.Invoices", new[] { "Contact_ContactId" });
            DropIndex("dbo.InvoicedWorks", new[] { "Invoice_InvoiceId" });
            DropIndex("dbo.WorkLocations", new[] { "Company_CompanyId" });
            DropIndex("dbo.Contacts", new[] { "Company_CompanyId" });
            DropTable("dbo.WorkLocationContacts");
            DropTable("dbo.Works");
            DropTable("dbo.WorkAreas");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Quotes");
            DropTable("dbo.QuotedWorks");
            DropTable("dbo.Invoices");
            DropTable("dbo.InvoicedWorks");
            DropTable("dbo.WorkLocations");
            DropTable("dbo.Contacts");
            DropTable("dbo.Companies");
        }
    }
}
