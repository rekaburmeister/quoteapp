using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    public class PartialWorkViewModel
    {
        public List<WorkViewModel> Works { get; set; }
        public List<string> WorkTypes { get; set; }
    }
}