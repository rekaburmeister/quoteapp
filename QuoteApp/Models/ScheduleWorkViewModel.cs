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

        public int TotalPrice { get; set; }

        public List<QuotedWork> QuotedWorks { get; set; }

        public ScheduleWorkViewModel()
        {
            if (QuotedWorks != null)
            {
                TotalPrice = QuotedWorks.Sum(w => w.QuotedWorkPrice*w.NumberOfCourts);
            }
        }
    }
}