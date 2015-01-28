using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
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

        public List<WorkArea> GetWorkAreasBySearchTerm(string term)
        {
            using (IApplicationService context = new DatabaseService())
            {
                return context.WorkAreas.Where(workArea => workArea.WorkAreaName.StartsWith(term.ToLower())).ToList();
            }
        }

        public int Add()
        {
            using (IApplicationService context = new DatabaseService())
            {
                context.WorkAreas.Add(this);
                return context.SaveChanges();
            }
        }

        public void SaveChanges()
        {
            using (IApplicationService context = new DatabaseService())
            {
                context.Entry(this).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Remove()
        {
            using (IApplicationService context = new DatabaseService())
            {
                context.WorkAreas.Remove(this);
                context.SaveChanges();
            }
        }
    }
}