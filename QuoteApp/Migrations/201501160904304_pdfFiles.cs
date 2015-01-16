namespace QuoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pdfFiles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GeneratedPdfs",
                c => new
                    {
                        Reference = c.String(nullable: false, maxLength: 128),
                        Content = c.Binary(),
                    })
                .PrimaryKey(t => t.Reference);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GeneratedPdfs");
        }
    }
}
