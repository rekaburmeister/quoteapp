using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    public class Quote
    {
        [Key]
        [Required]
        public string QuoteId { get; set; }

        [Required]
        public int WorkLocationId { get; set; }
        public virtual WorkLocation WorkLocation { get; set; }

        [Required]
        public int ContactId { get; set; }
        public virtual Contact Contact { get; set; }

        [Required]
        public DateTime QuoteDate { get; set; }

        public virtual ICollection<QuotedWork> QuotedWorks { get; set; }

        public static void CreateQuote(string quoteId, int locationId, int contactId, string quoteDate,
            List<CourtWorkDetail> works)
        {
            DateTime date = DateTime.ParseExact(quoteDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            Quote quote = new Quote { ContactId = contactId, WorkLocationId = locationId, QuoteDate = date, QuoteId = quoteId };
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Quotes.Add(quote);
                foreach (CourtWorkDetail quoteWorkDetail in works)
                {
                    foreach (WorkItem workItem in quoteWorkDetail.Works)
                    {
                        QuotedWork quotedWork = new QuotedWork()
                        {
                            Quote = quote,
                            QuotedWorkMainAreaName = quoteWorkDetail.AreaName,
                            QuotedWorkDescription = workItem.Description,
                            QuotedWorkSubAreaName = string.IsNullOrEmpty(workItem.WorkArea) ? string.Empty : workItem.WorkArea,
                            QuotedWorkPrice = workItem.Price
                        };
                        context.QuotedWorks.Add(quotedWork);
                    }
                }
                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
        }

        public static List<QuoteSummary> GetQuoteSummaries()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return
                    context.Quotes.ToList().Select(
                        quote =>
                            new QuoteSummary()
                            {
                                ContactEmail = quote.Contact.Email,
                                ContactNumber = quote.Contact.MobileNumber,
                                QuoteDate = quote.QuoteDate.ToString("d"),
                                QuoteId = quote.QuoteId,
                                ContactName = quote.Contact.FirstName + " " + quote.Contact.LastName,
                                LocationAddress = quote.WorkLocation.Town + ", " + quote.WorkLocation.PostCode,
                                LocationName = quote.WorkLocation.WorkLocationName,
                                Sum = quote.QuotedWorks.Sum(work => work.QuotedWorkPrice),
                                Job =
                                    string.Join(", ",
                                        quote.QuotedWorks.Select(areas => areas.QuotedWorkSubAreaName).ToArray())
                            })
                        .ToList();
            }
        } 

        public static bool DoesQuoteIdExist(string quoteId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Quotes.Any(q => q.QuoteId.Equals(quoteId));
            }
        }
    }

    public class QuoteViewModel
    {
        public string WorkLocationName { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime QuoteDate { get; set; }
        [Required]
        public string ContactName { get; set; }
        [EmailAddress]
        [Required]
        [DisplayName("Contact e-mail")]
        public string Email { get; set; }
        [Required]
        public string QuoteId { get; set; }

        public List<WorkViewModel> Works { get; set; }
        public List<string> WorkTypes { get; set; }
    }

    
}