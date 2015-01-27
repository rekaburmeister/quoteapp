using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace QuoteApp.Database.Work
{
    public class WorkArea
    {
        [Key]
        [Required]
        public int WorkAreaId { get; set; }

        [Required]
        public string WorkAreaName { get; set; }

        public virtual ICollection<Work> Works { get; set; }

        public List<WorkArea> GetWorkAreas()
        {
            using (IApplicationService context = new DatabaseService())
            {
                return context.WorkAreas.ToList();
            }
        }

        public WorkArea Find(int workAreaId)
        {
            using (IApplicationService context = new DatabaseService())
            {
                return context.WorkAreas.Find(workAreaId);
            }
        }
    }
}