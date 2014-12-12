using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    public class Quote
    {
        [Key]
        [Required]
        public string QuoteId { get; set; }

        [Required]
        public virtual WorkLocation Location { get; set; }

        [Required]
        public virtual Contact Contact { get; set; }

        [Required]
        public DateTime QuoteDate { get; set; }

        public virtual ICollection<QuotedWork> QuotedWorks { get; set; }
    }
}