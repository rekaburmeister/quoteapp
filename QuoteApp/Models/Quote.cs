using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    public class QuoteViewModel
    {
        public string WorkLocationName { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime QuoteDate { get; set; }
        [Required]
        public string ContactName { get; set; }
        [EmailAddress]
        [Required]
        [DisplayName("Contact e-mail")]
        public string Email { get; set; }
        [Required]
        public string QuoteId { get; set; }

        public List<WorkViewModel> Works { get; set; }
        public List<string> WorkTypes { get; set; }
    }
}