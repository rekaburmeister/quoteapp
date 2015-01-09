using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    public class ScheduledWork
    {
        public string QuoteId { get; set; }
        public string Date { get; set; }
        public string Club { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string Job { get; set; }
        public int Sum { get; set; }
    }
}