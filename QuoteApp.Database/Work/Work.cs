using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

        public List<Work> GetWorks()
        {
            using (IApplicationService context = new DatabaseService())
            {
                return context.Works.ToList();
            }
        }

        public List<WorkViewModel> GetWorkViewModelsForWorks()
        {
            using (IApplicationService context = new DatabaseService())
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