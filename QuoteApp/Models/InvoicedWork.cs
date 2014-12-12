using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    public class InvoicedWork
    {
        [Key]
        [Required]
        public int InvoicedWorkId { get; set; }

        [Required]
        public string InvoicedWorkDescription { get; set; }

        [Required]
        public int InvoicedWorkPrice { get; set; }

        [Required]
        public virtual Invoice Invoice { get; set; }
    }
}