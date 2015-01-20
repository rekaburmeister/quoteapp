using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    public class AcceptedWork
    {
        [Key]
        [Required]
        public int AcceptedWorkkId { get; set; }

        [Required]
        public string QuoteId { get; set; } // No foreign key relation here as I don't want a navigation 
                                            // property back to the quote but I do want to identify a set
                                            // Of invoiced worked by the quoteId - no invoiceID available at this stage

        public string WorkType { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Price { get; set; }

        public string CourtName { get; set; }

        public string InvoiceId { get; set; } // Once the work is comepleted I want to assign the invoiceId to these so we'll be able
                                              // to see what work has actually been carried out on a job
        public virtual Invoice Invoice { get; set; }

        public void Add(List<QuotedWork> works, string quoteId)
        {
            foreach (QuotedWork quotedWork in works)
            {
                for (int i = 1; i <= quotedWork.Accepted; i++)
                {
                    Description = quotedWork.QuotedWorkDescription;
                    QuoteId = quoteId;
                    Price = quotedWork.QuotedWorkPrice;
                    CourtName = !string.IsNullOrEmpty(quotedWork.QuotedWorkMainAreaName)
                        ? quotedWork.QuotedWorkMainAreaName
                        : "Court " + i;
                    WorkType = quotedWork.WorkTitle;
                    Add();
                }
            }
        }
        
        public void Add()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.AcceptedWork.Add(this);
                context.SaveChanges();
            }
        }
    }
}