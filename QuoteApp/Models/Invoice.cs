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
    }
}