using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    public class Invoice
    {
        [Key]
        [Required]
        public string InvoiceId { get; set; }

        [Required]
        public string InvoiceNumber { get; set; }

        [Required]
        public virtual WorkLocation Location { get; set; }

        [Required]
        public virtual Contact Contact { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        public virtual ICollection<InvoicedWork> InvoicedWorks { get; set; }

        public static List<Invoice> GetInvoicesForPeriod(DateTime from, DateTime to)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Invoices.Where(i => i.InvoiceDate >= from && i.InvoiceDate <= to).ToList();
            }
        }

        public static string GetNextInvoiceId()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                if (context.Invoices.Any())
                {
                    var id = context.Invoices.ToList().Last();
                    string idString = id.InvoiceId;
                    string idPart = idString.Split('-').Last();
                    idPart = idPart.Remove(0, 1);
                    return "I" + Convert.ToInt16(idPart) + 1;
                }
                return "I1658";
            }
        }
    }
}