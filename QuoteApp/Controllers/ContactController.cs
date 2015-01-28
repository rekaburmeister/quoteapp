using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using QuoteApp.Database.Contact;

namespace QuoteApp.Controllers
{
    public class ContactController : Controller
    {

        // GET: /Contact/
        public ActionResult Index()
        {
            Contact contact = new Contact();
            return View(contact.GetContacts());
        }

        // GET: /Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = new Contact();
            contact = contact.Find(Convert.ToInt16(id));
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: /Contact/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Contact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ContactId,FirstName,MiddleName,LastName,Email,MobileNumber,PhoneNumber")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.Add();
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        // GET: /Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Contact contact = new Contact();
            contact = contact.Find(Convert.ToInt16(id));
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: /Contact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ContactId,FirstName,MiddleName,LastName,Email,MobileNumber,PhoneNumber")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.SaveChanges();
                
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        public JsonResult GetContactsBySearchTerm(string term)
        {
            IEnumerable<ContactJson> contacts = Contact.GetContactsWithTerm(term).Select(contact => new ContactJson
            {
                Number = string.Join(", ", new[]{contact.MobileNumber, 
                                                 contact.PhoneNumber}
                                                 .Where(str => !string.IsNullOrEmpty(str))),
                Email = contact.Email,
                id = contact.ContactId,
                label = string.Join(" ", new[]{contact.FirstName, 
                                               contact.MiddleName,
                                               contact.LastName}
                                               .Where(str => !string.IsNullOrEmpty(str))),
                text = string.Join(" ", new[]{contact.FirstName, 
                                               contact.MiddleName,
                                               contact.LastName}
                                               .Where(str => !string.IsNullOrEmpty(str))),
            });
            return Json(JsonConvert.SerializeObject(contacts), JsonRequestBehavior.AllowGet);
        }

        // GET: /Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = new Contact();
            contact = contact.Find(Convert.ToInt16(id));
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: /Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = new Contact();
            contact = contact.Find(Convert.ToInt16(id));
            contact.Remove();
            
            return RedirectToAction("Index");
        }
    }
}
