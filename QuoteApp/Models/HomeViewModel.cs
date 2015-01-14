using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    public class HomeViewModel
    {
        public int JobsCompleted { get; set; }
        public int MoneyMade { get; set; }
        public int Vat { get; set; }
        public double IncomeTax { get; set; }
        public List<Invoice> Invoices { get; set; }
        public List<QuoteSummary> Quotes { get; set; }
        public List<ScheduledWork> ScheduledWorks { get; set; }

        public HomeViewModel()
        {
            var invoices = Invoice.GetInvoicesForPeriod(DateTime.Now.AddMonths(3), DateTime.Now);
            JobsCompleted = invoices.Count;
            MoneyMade = 0;
            IncomeTax = MoneyMade*0.2;
            Quotes = Quote.GetQuoteSummaries();
        }

    }
    
}