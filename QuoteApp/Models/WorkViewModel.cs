using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    public class WorkViewModel
    {
        

        [Required]
        public int WorkId { get; set; }

        [Required]
        public string WorkName { get; set; }

        [Required]
        public string WorkDescription { get; set; }

        [Required]
        public int WorkPrice { get; set; }

        [Required]
        public int WorkAreaId { get; set; }

        public string WorkAreaName { get; set; }

        public WorkViewModel()
        {

        }

        public WorkViewModel(Work work)
        {
            WorkId = work.WorkId;
            WorkName = work.WorkName;
            WorkDescription = work.WorkDescription;
            WorkPrice = work.WorkPrice;
            WorkAreaId = work.WorkArea.WorkAreaId;
            WorkAreaName = work.WorkArea.WorkAreaName;
        }
    }
}