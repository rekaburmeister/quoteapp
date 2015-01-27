using System.Data.Entity;
using QuoteApp.Database.Company;
using QuoteApp.Database.Contact;
using QuoteApp.Database.Invoice;
using QuoteApp.Database.Quote;
using QuoteApp.Database.Work;
using QuoteApp.Models;

namespace QuoteApp.Services
{
    public interface IApplicationService
    {
        DbSet<Contact> Contacts { get; set; }
        DbSet<Company> Companies { get; set; }
        DbSet<WorkLocation> WorkLocations { get; set; }
        DbSet<Work> Works { get; set; }
        DbSet<WorkArea> WorkAreas { get; set; }
        DbSet<Quote> Quotes { get; set; }
        DbSet<Invoice> Invoices { get; set; }
        DbSet<QuotedWork> QuotedWorks { get; set; }
        DbSet<AcceptedWork> AcceptedWork { get; set; }
        DbSet<GeneratedPdf> GeneratedPdfs { get; set; }
        DbSet<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
