using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    public class QuotedWork
    {
        [Key]
        [Required]
        public int QuotedWorkId { get; set; }

        [Required]
        public string QuotedWorkMainAreaName { get; set; }

        public string WorkTitle { get; set; }

        public string QuotedWorkSubAreaName { get; set; }

        [Required]
        public string QuotedWorkDescription { get; set; }

        [Required]
        public int NumberOfCourts { get; set; }

        [Required]
        public int QuotedWorkPrice { get; set; }

        [Required]
        public virtual Quote Quote { get; set; }
    }
}