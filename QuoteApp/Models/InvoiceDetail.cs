using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    public class InvoiceDetail
    {
        [Key]
        [Required]
        public string DetailId { get; set; }

        [Required]
        public string InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Price { get; set; }
    }
}