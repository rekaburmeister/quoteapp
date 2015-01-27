using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace QuoteApp.Database.Work
{
    public class WorkLocation
    {
        [Key]
        [Required]
        public int WorkLocationId { get; set; }

        [Required]
        [DisplayName("Club name")]
        public string WorkLocationName { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public virtual Company.Company Company { get; set; }

        public virtual ICollection<Contact.Contact> Contacts { get; set; }

        public static List<WorkLocation> GetClubsWithTerm(string term)
        {
            using (IApplicationService database = new DatabaseService())
            {
                return database.WorkLocations.Where(club => club.WorkLocationName.StartsWith(term.ToLower())).ToList();
            }
        }

        public static int CheckAndUpdateLocation(int clubId, string clubAddress, string clubName)
        {
            using (IApplicationService database = new DatabaseService())
            {
                WorkLocation location = database.WorkLocations.Find(clubId);
                string[] addressLines = clubAddress.Split(',');
                if (location == null)
                {
                    location = new WorkLocation
                    {
                        WorkLocationName = clubName,
                        Address1 = addressLines[0],
                        PostCode = addressLines[addressLines.Length - 1],
                        Town = addressLines[addressLines.Length - 2]
                    };
                    if (addressLines.Length > 3)
                    {
                        location.Address2 = string.Join(", ", addressLines, 1, addressLines.Length - 2);
                    }
                    database.WorkLocations.Add(location);
                    database.SaveChanges();
                }
                //else
                //{
                //    location.WorkLocationName = clubName;
                //    location.Address1 = addressLines[0];
                //    location.PostCode = addressLines[addressLines.Length - 1];
                //    location.Town = addressLines[addressLines.Length - 2];
                //    if (addressLines.Length > 3)
                //    {
                //        location.Address2 = string.Join(" ", addressLines, 1, addressLines.Length - 2);
                //    }
                //    database.Entry(location).State = EntityState.Modified;
                //}
                //database.SaveChanges();
                return location.WorkLocationId;
            }
        }

        public string GetAddress()
        {
            return string.Join(", ",
                new[] {Address1, Address2, Address3, Town, Country, PostCode}.Where(str => !string.IsNullOrEmpty(str)));
        }

        public static WorkLocation GetLocation(int workLocationId)
        {
            using (IApplicationService database = new DatabaseService())
            {
                return database.WorkLocations.Find(workLocationId);
            }
        }
    }
}