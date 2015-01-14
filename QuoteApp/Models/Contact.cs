using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    public class Contact
    {
        [Key]
        [Required]
        public int ContactId { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        
        public string MiddleName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [EmailAddress]
        [Required]
        [DisplayName("Contact e-mail")]
        public string Email { get; set; }

        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }

        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<WorkLocation> WorkLocations { get; set; }

        public static List<Contact> GetContactsWithTerm(string term)
        {
            using (ApplicationDbContext database = new ApplicationDbContext())
            {
                return database.Contacts.Where(contact => contact.FirstName.StartsWith(term.ToLower()) || contact.LastName.StartsWith(term.ToLower())).ToList();
            }
        }

        public static int CheckAndUpdateContact(int contactId, string contactName, string contactEmail, string contactNumber, int clubId)
        {
            using (ApplicationDbContext database = new ApplicationDbContext())
            {
                Contact contact = database.Contacts.Find(contactId);
                WorkLocation location = database.WorkLocations.Find(clubId);
                string[] names = contactName.Split(' ');
                if (contact == null)
                {
                    contact = new Contact
                    {
                        FirstName = names[0],
                        LastName = names[names.Length - 1],
                        Email = contactEmail,
                        MobileNumber = contactNumber
                    };
                    if (names.Length > 2)
                    {
                        contact.MiddleName = string.Join(" ", names, 1, names.Length - 2);
                    }
                    contact.WorkLocations.Add(location);
                    database.Contacts.Add(contact);
                    database.SaveChanges();
                }
                //else
                //{
                //    contact.FirstName = names[0];
                //    contact.LastName = names[names.Length - 1];
                //    if (names.Length > 2)
                //    {
                //        contact.MiddleName = string.Join(" ", names, 1, names.Length - 2);
                //    }
                //    contact.Email = contactEmail;
                //    contact.MobileNumber = contactNumber;
                //    if (!contact.WorkLocations.Contains(location))
                //    {
                //        contact.WorkLocations.Add(location);
                //    }
                //    database.Entry(contact).State = EntityState.Modified;
                //}
                //database.SaveChanges();
                return contact.ContactId;
            }
        }
    }
}