using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using QuoteApp.Database.Work;

namespace QuoteApp.Database.Contact
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
        public virtual Company.Company Company { get; set; }

        public virtual ICollection<WorkLocation> WorkLocations { get; set; }

        public static List<Contact> GetContactsWithTerm(string term)
        {
            using (IApplicationService database = new DatabaseService())
            {
                return database.Contacts.Where(contact => contact.FirstName.StartsWith(term.ToLower()) || contact.LastName.StartsWith(term.ToLower())).ToList();
            }
        }

        public static int CheckAndUpdateContact(int contactId, string contactName, string contactEmail, string contactNumber, int clubId)
        {
            using (IApplicationService database = new DatabaseService())
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
                    contact.WorkLocations = new Collection<WorkLocation> {location};
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

        public string GetName()
        {
            return string.Join(" ", new[] {FirstName, MiddleName, LastName}.Where(str => !string.IsNullOrEmpty(str)));
        }

        public static Contact GetContact(int contactId)
        {
            using (IApplicationService database = new DatabaseService())
            {
                return database.Contacts.Find(contactId);
            }
        }

        public List<Contact> GetContacts()
        {
            using (IApplicationService database = new DatabaseService())
            {
                return database.Contacts.ToList();
            }
        }

        public Contact Find(int id)
        {
            using (IApplicationService database = new DatabaseService())
            {
                return database.Contacts.Find(id);
            }
        }

        public int Add()
        {
            using (IApplicationService database = new DatabaseService())
            {
                database.Contacts.Add(this);
                return database.SaveChanges();
            }
        }

        public void SaveChanges()
        {
            using (IApplicationService database = new DatabaseService())
            {
                database.Entry(this).State = EntityState.Modified;
                database.SaveChanges();
            }
        }

        public void Remove()
        {
            using (IApplicationService database = new DatabaseService())
            {
                database.Contacts.Remove(this);
                database.SaveChanges();
            }
        }
    }
}