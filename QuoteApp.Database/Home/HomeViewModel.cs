using System.Collections.Generic;
using System.Linq;
using QuoteApp.Database.Invoice;
using QuoteApp.Database.Quote;
using QuoteApp.Database.Time;
using QuoteApp.Database.Work;

namespace QuoteApp.Database.Home
{
    public class HomeViewModel
    {
        public int JobsCompleted { get; set; }
        public double MoneyMade { get; set; }
        public double Vat { get; set; }
        public double IncomeTax { get; set; }
        public List<InvoiceViewModel> Invoices { get; set; }
        public List<QuoteSummary> Quotes { get; set; }
        public List<ScheduledWork> ScheduledWorks { get; set; }

        public HomeViewModel()
        {
            InvoiceViewModel invoiceViewModel = new InvoiceViewModel();
            Invoices = invoiceViewModel.GetUnPaidInvoices();
            Quarter quarter = new Quarter();
            var invoicesForPeriod = Invoice.Invoice.GetInvoicesForPeriod(quarter.Start, quarter.End);
            JobsCompleted = invoicesForPeriod.Count;
            MoneyMade = invoicesForPeriod.Sum(m=>m.Price);
            IncomeTax = MoneyMade*0.3;
            Vat = MoneyMade * 0.2;
            Quotes = Quote.Quote.GetQuoteSummaries();
            ScheduledWorks = ScheduledWork.GetScheduledWorks();
        }
    }
    
}