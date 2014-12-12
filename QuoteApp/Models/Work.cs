using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    public class Work
    {
        [Key]
        [Required]
        public int WorkId { get; set; }

        [Required]
        public string WorkName { get; set; }

        [Required]
        public string WorkDescription { get; set; }

        [Required]
        public int WorkPrice { get; set; }

        [Required]
        public virtual WorkArea WorkArea { get; set; }
    }
}