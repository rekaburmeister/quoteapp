using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    public class WorkFromView
    {
        public string AreaName { get; set; }
        public List<WorkItem> Works { get; set; }
    }
}