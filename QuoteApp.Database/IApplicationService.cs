﻿using System.Data.Entity;
using QuoteApp.Database.Invoice;
using QuoteApp.Database.Quote;
using QuoteApp.Database.Work;
using QuoteApp.Models;

namespace QuoteApp.Database
{
    public interface IApplicationService
    {
        DbSet<Contact.Contact> Contacts { get; set; }
        DbSet<Company.Company> Companies { get; set; }
        DbSet<WorkLocation> WorkLocations { get; set; }
        DbSet<Work.Work> Works { get; set; }
        DbSet<WorkArea> WorkAreas { get; set; }
        DbSet<Quote.Quote> Quotes { get; set; }
        DbSet<Invoice.Invoice> Invoices { get; set; }
        DbSet<QuotedWork> QuotedWorks { get; set; }
        DbSet<AcceptedWork> AcceptedWork { get; set; }
        DbSet<GeneratedPdf> GeneratedPdfs { get; set; }
        DbSet<InvoiceDetail> InvoiceDetails { get; set; }
    }
}