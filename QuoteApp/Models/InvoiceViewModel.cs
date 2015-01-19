using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    public class InvoiceViewModel
    {
        [Required]
        public string InvoiceId { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public int WorkLocationId { get; set; }

        [Required]
        public string InvoiceTo { get; set; }

        [Required]
        public string InvoiceToAddress { get; set; }
        
        public string Reference { get; set; }

        [Required]
        public int ContactId { get; set; }

        [Required]
        public string CareOf { get; set; }

        [Required]
        [EmailAddress]
        public string CareOfEmail { get; set; }

        [Required]
        public string CareOfNumber { get; set; }

        [Required]
        public string Details { get; set; }

        [Required]
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
            ContactId = quote.ContactId;
            WorkLocationId = quote.WorkLocationId;
            CareOf = quote.Contact.GetName();
            CareOfEmail = quote.Contact.Email;
            CareOfNumber = quote.Contact.MobileNumber ?? quote.Contact.PhoneNumber;
            Price = acceptedWorks.Sum(work => work.QuotedWorkPrice*work.Accepted);

        }
    }

}