using System.Collections.Generic;
using System.Linq;
using QuoteApp.Database.Quote;

namespace QuoteApp.Database.Work
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
        public List<WorkViewModel> Works { get; set; }
        public List<QuotedWork> QuotedWorks { get; set; }
        public List<string> WorkTypes { get; set; }

        public ScheduleWorkViewModel()
        {
            
        }

        public ScheduleWorkViewModel(List<QuotedWork> quotedWorks)
        {
            QuotedWorks = quotedWorks;
            TotalPrice = quotedWorks.Sum(w => w.QuotedWorkPrice * w.NumberOfCourts);
            WorkTypes = WorkArea.GetWorkAreas().Select(area => area.WorkAreaName).ToList();
            Work work = new Work();
            Works = work.GetWorkViewModelsForWorks();
        }
    }
}