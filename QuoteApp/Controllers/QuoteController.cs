using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using QuoteApp.Models;
using Rotativa;

namespace QuoteApp.Controllers
{
    public class QuoteController : Controller
    {
        private readonly ApplicationDbContext m_DbContext = new ApplicationDbContext();

        // GET: /Quote/
        public ActionResult Index()
        {
            return View(m_DbContext.Quotes.ToList());
        }

        // GET: /Quote/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = m_DbContext.Quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        // GET: /Quote/Create
        public ActionResult Create()
        {
            QuoteViewModel model = new QuoteViewModel()
            {
                WorkTypes = m_DbContext.WorkAreas.Select(area => area.WorkAreaName).ToList(),
                Works = m_DbContext.Works.ToList().Select(work=>new WorkViewModel(work)).ToList()
            };
            return View(model);
        }

        // POST: /Quote/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="QuoteId,QuoteDate")] Quote quote)
        {
            if (ModelState.IsValid)
            {
                m_DbContext.Quotes.Add(quote);
                m_DbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quote);
        }

        public JsonResult CreateQuote(string jsonString)
        {
            WorkFromView works = (WorkFromView)JsonConvert.DeserializeObject(jsonString, typeof(WorkFromView));
            WorkLocation location = WorkLocation.CheckAndUpdateLocation(works.ContactDetails.ClubId, works.ContactDetails.ClubAddress, works.ContactDetails.ClubName);
            Contact contact = Contact.CheckAndUpdateContact(works.ContactDetails.ContactId, works.ContactDetails.ContactName,
                works.ContactDetails.ContactEmail, works.ContactDetails.ContactNumber, location.WorkLocationId);
            Quote.CreateQuote(works.QuoteRef, location, contact, works.QuoteDate, works.CourtWorkDetails);
            return Json(new { Success = true, Message = "Quoted work added" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerifyQuoteId(string quoteId)
        {
            return Json(new{Success = Quote.DoesQuoteIdExist(quoteId)}, JsonRequestBehavior.AllowGet);
        }

        // GET: /Quote/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = m_DbContext.Quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        // POST: /Quote/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="QuoteId,QuoteDate")] Quote quote)
        {
            if (ModelState.IsValid)
            {
                m_DbContext.Entry(quote).State = EntityState.Modified;
                m_DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quote);
        }

        public ActionResult GeneratePdf(QuoteViewModel model)
        {
            return new ViewAsPdf(model);
        }

        public ActionResult GeneratePdf2()
        {
            return new ViewAsPdf();
        }

        // GET: /Quote/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = m_DbContext.Quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        // POST: /Quote/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Quote quote = m_DbContext.Quotes.Find(id);
            m_DbContext.Quotes.Remove(quote);
            m_DbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_DbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
