using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace QuoteApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DatabaseContext")
        {
        }

        public ApplicationDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<WorkLocation> WorkLocations { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<WorkArea> WorkAreas { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<QuotedWork> QuotedWorks { get; set; }
        public DbSet<AcceptedWork> InvoicedWorks { get; set; }
        public DbSet<GeneratedPdf> GeneratedPdfs { get; set; }

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