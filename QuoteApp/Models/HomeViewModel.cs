using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuoteApp.Helpers;

namespace QuoteApp.Models
{
    public class HomeViewModel
    {
        public int JobsCompleted { get; set; }
        public double MoneyMade { get; set; }
        public double Vat { get; set; }
        public double IncomeTax { get; set; }
        public List<Invoice> Invoices { get; set; }
        public List<QuoteSummary> Quotes { get; set; }
        public List<ScheduledWork> ScheduledWorks { get; set; }

        public HomeViewModel()
        {
            Invoices = Invoice.GetUnpaidInvoices();
            Quarter quarter = new Quarter();
            var invoicesForPeriod = Invoice.GetInvoicesForPeriod(quarter.Start, quarter.End);
            JobsCompleted = invoicesForPeriod.Count;
            MoneyMade = invoicesForPeriod.Sum(m=>m.Price);
            IncomeTax = MoneyMade*0.3;
            Vat = MoneyMade * 0.2;
            Quotes = Quote.GetQuoteSummaries();
            ScheduledWorks = ScheduledWork.GetScheduledWorks();
        }
    }
    
}