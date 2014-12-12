using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuoteApp.Models;
using RazorPDF;

namespace QuoteApp.Controllers
{
    public class ContactController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Contact/
        public ActionResult Index()
        {
            //return View(db.Contacts.ToList());
            List<Contact> Contacts = new List<Contact>();

            for (int i = 1; i <= 10; i++)
            {
                Contact Contact = new Contact
                {
                    ContactId = i,
                    FirstName = string.Format("FirstName{0}", i.ToString()),
                    LastName = string.Format("LastName{0}", i.ToString())
                };
                Contacts.Add(Contact);
            }
            return View(Contacts);
        }

        public PdfResult PDF()
        {
            List<Contact> Contacts = new List<Contact>();

            for (int i = 1; i <= 10; i++)
            {
                Contact Contact = new Contact
                {
                    ContactId = i,
                    FirstName = string.Format("FirstName{0}", i.ToString()),
                    LastName = string.Format("LastName{0}", i.ToString())
                };
                Contacts.Add(Contact);
            }

            return new RazorPDF.PdfResult(Contacts, "PDF");
        }

        // GET: /Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact Contact = db.Contacts.Find(id);
            if (Contact == null)
            {
                return HttpNotFound();
            }
            return View(Contact);
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
        public ActionResult Create([Bind(Include="ContactId,FirstName,LastName")] Contact Contact)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(Contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Contact);
        }

        // GET: /Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact Contact = db.Contacts.Find(id);
            if (Contact == null)
            {
                return HttpNotFound();
            }
            return View(Contact);
        }

        // POST: /Contact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ContactId,FirstName,LastName")] Contact Contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Contact);
        }

        // GET: /Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact Contact = db.Contacts.Find(id);
            if (Contact == null)
            {
                return HttpNotFound();
            }
            return View(Contact);
        }

        // POST: /Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact Contact = db.Contacts.Find(id);
            db.Contacts.Remove(Contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
