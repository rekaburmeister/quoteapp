using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    public class InvoiceViewModel
    {
        public string InvoiceId { get; set; }
        public string Date { get; set; }
        public string InvoiceTo { get; set; }
        public string InvoiceToAddress { get; set; }
        public string Reference { get; set; }
        public string CareOf { get; set; }
        public string CareOfEmail { get; set; }
        public string CareOfNumber { get; set; }
        public string Details { get; set; }
        public int Price { get; set; }

        public InvoiceViewModel()
        {

        }

        public InvoiceViewModel(Quote quote, string nextInvoice)
        {
            string[] split = quote.QuoteId.Split('-');
            var acceptedWorks = quote.QuotedWorks.Where(work => work.Accepted > 0);
            InvoiceId = string.Join("-", split.Where(s => !s.Equals(split.Last()))) + "-" + nextInvoice;
            Date = DateTime.Today.ToString("dd-MM-yyyy");
            InvoiceTo = quote.WorkLocation.WorkLocationName;
            InvoiceToAddress = quote.WorkLocation.GetAddress();
            Details = "Refurbishment to squash courts as agrred";
            CareOf = quote.Contact.GetName();
            CareOfEmail = quote.Contact.Email;
            CareOfNumber = quote.Contact.MobileNumber ?? quote.Contact.PhoneNumber;
            Price = acceptedWorks.Sum(work => work.QuotedWorkPrice*work.Accepted);

        }
    }

}