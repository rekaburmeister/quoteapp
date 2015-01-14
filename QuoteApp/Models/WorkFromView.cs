using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    //public class WorkFromView
    //{
    //    public string AreaName { get; set; }
    //    public List<WorkItem> Works { get; set; }
    //    public string ClubName { get; set; }
    //    public int ClubId { get; set; }
    //    public string ClubAddress { get; set; }
    //    public string ContactName { get; set; }
    //    public string ContactNumber { get; set; }
    //    public string ContactEmail { get; set; }
    //    public int ContactId { get; set; }
    //    public string QuoteId { get; set; }
    //    public string QuoteDate { get; set; }
    //}

    public class ContactDetails
    {
        public string ClubName { get; set; }
        public int ClubId { get; set; }
        public string ClubAddress { get; set; }
        public string ContactName { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public int ContactId { get; set; }        
    }

    public class CourtWorkDetail
    {
        public string AreaName { get; set; }
        public List<WorkItem> Works { get; set; }        
    }

    public class WorkFromView
    {
        public string QuoteRef { get; set; }
        public string QuoteDate { get; set; }
        public ContactDetails ContactDetails { get; set; }
        public List<CourtWorkDetail> CourtWorkDetails{get;set;}
    }

}