﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
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

        [Required]
        public bool Archived { get; set; }
        public DateTime? ScheduledFor { get; set; }
        public int JobLength { get; set; }

        [Required]
        public bool Finished { get; set; }

        public static void CreateQuote(string quoteId, int locationId, int contactId, string quoteDate,
            List<CourtWorkDetail> works)
        {
            DateTime date = DateTime.ParseExact(quoteDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            Quote quote = new Quote { ContactId = contactId, WorkLocationId = locationId, QuoteDate = date, QuoteId = quoteId, Archived = false, Finished = false};
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
                            WorkTitle = workItem.WorkTitle,
                            NumberOfCourts = quoteWorkDetail.NumberOfCourts,
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
                    context.Quotes.ToList().Where(archived => !archived.Archived && archived.ScheduledFor == null).Select(
                        quote =>
                            new QuoteSummary()
                            {
                                ContactEmail = quote.Contact.Email,
                                ContactNumber = quote.Contact.MobileNumber,
                                QuoteDate = quote.QuoteDate.ToString("dd-MM-yyyy"),
                                QuoteId = quote.QuoteId,
                                ContactName = quote.Contact.FirstName + " " + quote.Contact.LastName,
                                LocationAddress = quote.WorkLocation.Town + ", " + quote.WorkLocation.PostCode,
                                LocationName = quote.WorkLocation.WorkLocationName,
                                Sum = quote.QuotedWorks.Sum(work => work.QuotedWorkPrice * work.NumberOfCourts),
                                Job =
                                    string.Join(", ", quote.QuotedWorks
                                                        .GroupBy(w => w.WorkTitle).Select(o => string.Format("{0} ({1})", o.Key, 
                                                            quote.QuotedWorks.Where(q=>q.WorkTitle.Equals(o.Key)).Sum(qw=>qw.NumberOfCourts))))
                            })
                        .ToList();
            }
        }

        public static Quote GetQuote(string quoteId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Quotes.Find(quoteId);
            }
        }

        public static bool DoesQuoteIdExist(string quoteId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Quotes.Any(q => q.QuoteId.Equals(quoteId));
            }
        }

        public static List<QuotedWork> GetWorksForId(string quoteId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.QuotedWorks.Where(q => q.Quote.QuoteId.Equals(quoteId)).ToList();
            }
        }

        public string GetCustomerIdentifier()
        {
            var split = QuoteId.Split('-');
            return string.Join("-", split.Where(s => !s.Equals(split.Last())));
        }

        public void MarkAsFinished()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Finished = true;
                context.Entry(this).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public int GetNextNumberForId(string currentQuoteId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                if (context.Quotes.Find(currentQuoteId) == null)
                {
                    return 0;
                }
                string quoteId = context.Quotes.ToArray().Last(q => q.QuoteId.Contains(currentQuoteId)).QuoteId;
                string quotePart = quoteId.Split('-').Last();
                string number = quotePart.Remove(0, 1);
                return Convert.ToInt16(number) + 1;
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