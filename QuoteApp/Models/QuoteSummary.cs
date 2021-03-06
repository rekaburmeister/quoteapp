﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    public class QuoteSummary
    {
        public string QuoteId { get; set; }
        public string QuoteDate { get; set; }
        public string LocationName { get; set; }
        public string LocationAddress { get; set; }
        public string ContactName { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public string Job { get; set; }
        public int Sum { get; set; }
    }
}