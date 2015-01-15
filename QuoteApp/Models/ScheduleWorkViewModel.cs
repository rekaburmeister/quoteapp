using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    public class ScheduleWorkViewModel
    {
        [Required]
        public int NumberOfDays { get; set; }

        [Required]
        public string WorkStarts { get; set; }

        [Required]
        public string QuoteId { get; set; }
    }
}