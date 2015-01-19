using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuoteApp.Models;

namespace QuoteApp.Controllers
{
    public class InvoiceController : Controller
    {
        private ApplicationDbContext m_Context = new ApplicationDbContext();

        // GET: /Invoice/
        public ActionResult Index()
        {
            return View(m_Context.Invoices.ToList());
        }

        // GET: /Invoice/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = m_Context.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: /Invoice/Create
        public ActionResult Create(string quoteId)
        {
            Quote quote = Quote.GetQuote(m_Context, quoteId);
            string nextInvoice = Invoice.GetNextInvoiceId();
            return View(new InvoiceViewModel(quote, nextInvoice));
        }

        // POST: /Invoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceId,Date,InvoiceTo,InvoiceToAddress,Reference,CareOf,CareOfEmail,CareOfNumber,Details,Price,ContactId,WorkLocationId")] InvoiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                Invoice invoice = new Invoice
                {
                    InvoiceDate = DateTime.ParseExact(model.Date, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    InvoiceId = model.InvoiceId,
                    ContactId = model.ContactId,
                    Price = model.Price,
                    Details = model.Details,
                    PaidDate = null,
                    WorkLocationId = model.WorkLocationId
                };
                m_Context.Invoices.Add(invoice);
                try
                {
                    m_Context.SaveChanges();
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
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: /Invoice/Edit/5
        public ActionResult Edit(string quoteRef)
        {
            if (quoteRef == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = m_Context.Invoices.Find(quoteRef);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: /Invoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="InvoiceId,InvoiceNumber,InvoiceDate")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                m_Context.Entry(invoice).State = EntityState.Modified;
                m_Context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(invoice);
        }

        // GET: /Invoice/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = m_Context.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        public ActionResult MarkAsPaid(string invoiceId)
        {
            ViewBag.InvoiceId = invoiceId;
            return View();
        }

        public ActionResult PaymentConfirmed(string invoiceId)
        {
            Invoice.MarkAsPaid(invoiceId);
            return RedirectToAction("Index", "Home");
        }

        // POST: /Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Invoice invoice = m_Context.Invoices.Find(id);
            m_Context.Invoices.Remove(invoice);
            m_Context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
