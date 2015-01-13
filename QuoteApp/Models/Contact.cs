using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

        public virtual Company Company { get; set; }

        public virtual ICollection<WorkLocation> WorkLocations { get; set; }

        public static List<Contact> GetContactsWithTerm(string term)
        {
            using (ApplicationDbContext database = new ApplicationDbContext())
            {
                return database.Contacts.Where(contact => contact.FirstName.StartsWith(term.ToLower()) || contact.LastName.StartsWith(term.ToLower())).ToList();
            }
        }
    }
}