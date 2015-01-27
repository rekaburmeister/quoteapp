using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using QuoteApp.Database.Work;

namespace QuoteApp.Database.Invoice
{
    public class CreateInvoiceViewModel
    {
        [Required]
        public string InvoiceId { get; set; }

        [Required]
        public string QuoteId { get; set; }

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

        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }

        public CreateInvoiceViewModel()
        {

        }

        public CreateInvoiceViewModel(string quoteId, string nextInvoice)
        {
            QuoteId = quoteId;
            Quote.Quote quote = Quote.Quote.GetQuote(quoteId);
            WorkLocation location = WorkLocation.GetLocation(quote.WorkLocationId);
            Contact.Contact contact = Contact.Contact.GetContact(quote.ContactId);
            var acceptedWorks = AcceptedWork.GetWorksForQuote(quoteId);
            InvoiceId = quote.GetCustomerIdentifier() + "-" + nextInvoice;
            Date = DateTime.Today.ToString("dd-MM-yyyy");
            InvoiceTo = location.WorkLocationName;
            InvoiceToAddress = location.GetAddress();
            ContactId = quote.ContactId;
            WorkLocationId = quote.WorkLocationId;
            CareOf = contact.GetName();
            CareOfEmail = contact.Email;
            CareOfNumber = contact.MobileNumber ?? contact.PhoneNumber;
            InvoiceDetails = new List<InvoiceDetail>() 
                { new InvoiceDetail() 
                    { Description = "Refurbishment to squash courts as agrred", 
                      Price = acceptedWorks.Sum(work => work.Price) } };
        }
    }

}