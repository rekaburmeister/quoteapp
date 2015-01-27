﻿using System.ComponentModel.DataAnnotations;

namespace QuoteApp.Database.Quote
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

        public int Accepted { get; set; }

        [Required]
        public int QuotedWorkPrice { get; set; }

        [Required]
        public virtual Quote Quote { get; set; }
    }
}