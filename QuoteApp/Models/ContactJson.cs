using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    public class ContactJson
    {
        public int id { get; set; }
        public string label { get; set; }
        public string text { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
    }
}