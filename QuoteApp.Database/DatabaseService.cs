using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using QuoteApp.Database.Invoice;
using QuoteApp.Database.Quote;
using QuoteApp.Database.Work;
using QuoteApp.DatabaseService;
using QuoteApp.Models;

namespace QuoteApp.Database
{
    class DatabaseService :IdentityDbContext<ApplicationUser>, IApplicationService
    {
        public DatabaseService()
            : base("DatabaseContext")
        {
        }

        public DatabaseService(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<Contact.Contact> Contacts { get; set; }
        public DbSet<Company.Company> Companies { get; set; }
        public DbSet<WorkLocation> WorkLocations { get; set; }
        public DbSet<Work.Work> Works { get; set; }
        public DbSet<WorkArea> WorkAreas { get; set; }
        public DbSet<Quote.Quote> Quotes { get; set; }
        public DbSet<Invoice.Invoice> Invoices { get; set; }
        public DbSet<QuotedWork> QuotedWorks { get; set; }
        public DbSet<AcceptedWork> AcceptedWork { get; set; }
        public DbSet<GeneratedPdf> GeneratedPdfs { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Change the name of the table to be Users instead of AspNetUsers
            modelBuilder.Entity<IdentityUser>()
                .ToTable("Users");
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("Users");
        }
    }
}
