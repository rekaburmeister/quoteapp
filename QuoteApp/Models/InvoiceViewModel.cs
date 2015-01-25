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
        public string InvoiceTo { get; set; }

        [Required]
        public string InvoiceToAddress { get; set; }

        [Required]
        public string ContactName { get; set; }

        [Required]
        [EmailAddress]
        public string ContactEmail { get; set; }

        [Required]
        public string ContactNumber { get; set; }

        [Required]
        public int Price { get; set; }

        public string Details { get; set; }
        public string Reference { get; set; }

        public InvoiceViewModel()
        {
            
        }

        public InvoiceViewModel(string invoiceId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Invoice invoice = context.Invoices.Find(invoiceId);
                if (invoice != null)
                {
                    InvoiceId = invoice.InvoiceId;
                    Date = invoice.InvoiceDate.ToString("dd-MM-yyyy");
                    InvoiceTo = invoice.WorkLocation.WorkLocationName;
                    InvoiceToAddress = invoice.WorkLocation.GetAddress();
                    ContactName = invoice.Contact.GetName();
                    ContactNumber = invoice.Contact.MobileNumber ?? invoice.Contact.PhoneNumber;
                    ContactEmail = invoice.Contact.Email;
                    Price = invoice.Price;
                    //Details = invoice.Details;
                }
            }
        }

        public List<InvoiceViewModel> GetUnPaidInvoices()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Invoices.Where(i => i.PaidDate == null).ToList().Select(invoice => new InvoiceViewModel()
                {
                    InvoiceId = invoice.InvoiceId,
                    Date = invoice.InvoiceDate.ToString("dd-MM-yyyy"),
                    InvoiceTo = invoice.WorkLocation.WorkLocationName,
                    ContactName = invoice.Contact.GetName(),
                    ContactNumber = invoice.Contact.MobileNumber ?? invoice.Contact.PhoneNumber,
                    ContactEmail = invoice.Contact.Email,
                    Price = invoice.Price
                }).ToList();
            }
        }
    }
}