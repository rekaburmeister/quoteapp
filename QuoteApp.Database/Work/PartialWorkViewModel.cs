using System.Collections.Generic;

namespace QuoteApp.Database.Work
{
    public class PartialWorkViewModel
    {
        public List<WorkViewModel> Works { get; set; }
        public List<string> WorkTypes { get; set; }
    }
}