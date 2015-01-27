using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using QuoteApp.Database.Quote;

namespace QuoteApp.Database.Work
{
    public class WorkFromView
    {
        public string QuoteRef { get; set; }
        public string QuoteDate { get; set; }
        public ContactDetails ContactDetails { get; set; }
        public List<CourtWorkDetail> CourtWorkDetails { get; set; }
    }

    public class ContactDetails
    {
        public string ClubName { get; set; }
        public int ClubId { get; set; }
        public string ClubAddress { get; set; }
        public string ContactName { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public int ContactId { get; set; }

        public ContactDetails()
        {
            
        }

        public ContactDetails(Contact.Contact contact, WorkLocation location)
        {
            ClubName = location.WorkLocationName;
            ClubAddress = location.GetAddress();
            ContactName = contact.FirstName + " " + contact.LastName;
            ContactNumber = contact.MobileNumber ?? contact.PhoneNumber;
            ContactEmail = contact.Email;
        }
    }

    public class CourtWorkDetail
    {
        public string AreaName { get; set; }
        public List<WorkItem> Works { get; set; }
        [Required]
        public int NumberOfCourts { get; set; }

        public static List<CourtWorkDetail> GetCourtWorkDetails(List<QuotedWork> quotedWorks)
        {
            var mainAreas = quotedWorks.GroupBy(work => work.QuotedWorkMainAreaName);
            List<CourtWorkDetail> courtWorkDetails = new List<CourtWorkDetail>();
            foreach (var mainArea in mainAreas)
            {
                var worksInArea = quotedWorks.Where(work => work.QuotedWorkMainAreaName.Equals(mainArea.Key));
                QuotedWork quotedWork = worksInArea.FirstOrDefault();
                if (quotedWork == null)
                {
                    throw new Exception("Something went horribly wrong");
                }
                CourtWorkDetail details = new CourtWorkDetail()
                {
                    AreaName = quotedWork.QuotedWorkMainAreaName,
                    NumberOfCourts = quotedWork.NumberOfCourts,
                    Works =
                        worksInArea.Select(
                            workItem =>
                                new WorkItem()
                                {
                                    Description = workItem.QuotedWorkDescription,
                                    WorkArea = workItem.QuotedWorkSubAreaName,
                                    Price = workItem.QuotedWorkPrice
                                }).ToList()
                };
                courtWorkDetails.Add(details);
            }
            return courtWorkDetails;
        }
    }
}