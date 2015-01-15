using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    public class WorkItem
    {
        [Required]
        public string WorkArea { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string WorkTitle { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int NumberOfCourts { get; set; }
    }
}