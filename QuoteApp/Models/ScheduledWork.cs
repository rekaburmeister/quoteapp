using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    public class ScheduledWork
    {
        public string QuoteId { get; set; }
        public string Date { get; set; }
        public string Club { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string Job { get; set; }
        public int Sum { get; set; }

        public static List<ScheduledWork> GetScheduledWorks()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return
                    context.Quotes.ToList().Where(q => !q.Archived && q.ScheduledFor != null && ! q.Finished).Select(
                        quote =>
                            new ScheduledWork()
                            {
                                Email = quote.Contact.Email,
                                Number = quote.Contact.MobileNumber,
                                Date = quote.ScheduledFor.ToString(),
                                QuoteId = quote.QuoteId,
                                Contact = quote.Contact.FirstName + " " + quote.Contact.LastName,
                                Address = quote.WorkLocation.Town + ", " + quote.WorkLocation.PostCode,
                                Club = quote.WorkLocation.WorkLocationName,
                                Sum = quote.QuotedWorks.Sum(work => work.QuotedWorkPrice * work.NumberOfCourts),
                                Job =
                                    string.Join(", ",
                                        quote.QuotedWorks.Select(areas => areas.WorkTitle + "(" + areas.NumberOfCourts + ")").ToArray())
                            })
                        .ToList();
            }
        } 
    }
}