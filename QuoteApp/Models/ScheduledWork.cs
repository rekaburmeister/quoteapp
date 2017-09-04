using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Schema;

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
                var quoteIds =
                    context.Quotes.Where(q => !q.Archived && q.ScheduledFor != null && !q.Finished).Select(quote => quote.QuoteId);
                var workForQuotes = context.AcceptedWork.Where(work => quoteIds.Contains(work.QuoteId)).ToArray();

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
                                Address = quote.Location.Town + ", " + quote.Location.PostCode,
                                Club = quote.Location.WorkLocationName,
                                Sum = workForQuotes.Where(work=>work.QuoteId.Equals(quote.QuoteId)).Sum(w=>w.Price),
                                Job = string.Join(", ", workForQuotes
                                                        .Where(work=>work.QuoteId.Equals(quote.QuoteId))
                                                        .GroupBy(w=>w.WorkType).Select(o=> string.Format("{0} ({1})", o.Key, o.Count())))
                            })
                        .OrderBy(work=>work.Date)
                        .ToList();
            }
        } 
    }
}