using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    public class WorkArea
    {
        [Key]
        [Required]
        public int WorkAreaId { get; set; }

        [Required]
        public string WorkAreaName { get; set; }

        public virtual ICollection<Work> Works { get; set; }

        internal static List<WorkArea> GetWorkAreas()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.WorkAreas.ToList();
            }
        }
    }
}