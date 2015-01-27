using System.Collections.Generic;

namespace QuoteApp.Database.Work
{
    public class Work
    {
        [Key]
        [Required]
        public int WorkId { get; set; }

        [Required]
        public string WorkName { get; set; }

        [Required]
        public string WorkDescription { get; set; }

        [Required]
        public int WorkPrice { get; set; }

        [Required]
        public virtual WorkArea WorkArea { get; set; }

        internal static List<Work> GetWorks()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Works.ToList();
            }
        }

        public List<WorkViewModel> GetWorkViewModelsForWorks()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Works.ToArray().Select(work=>work.GetWorkViewModel()).ToList();
            }
        }

        private WorkViewModel GetWorkViewModel()
        {
            return new WorkViewModel
            {
                WorkId = WorkId,
                WorkName = WorkName,
                WorkDescription = WorkDescription,
                WorkPrice = WorkPrice,
                WorkAreaId = WorkArea.WorkAreaId,
                WorkAreaName = WorkArea.WorkAreaName
            };
            
        }
    }
}