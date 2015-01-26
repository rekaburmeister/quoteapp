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
using Rotativa;
using Rotativa.Options;

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
            string nextInvoice = Invoice.GetNextInvoiceId();
            return View(new CreateInvoiceViewModel(quoteId, nextInvoice));
        }

        // POST: /Invoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "InvoiceId,Date,InvoiceTo,InvoiceToAddress,Reference,CareOf,CareOfEmail,CareOfNumber,InvoiceDetails,ContactId,WorkLocationId,QuoteId")] CreateInvoiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                Invoice invoice = new Invoice
                {
                    InvoiceDate = DateTime.ParseExact(model.Date, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    InvoiceId = model.InvoiceId,
                    ContactId = model.ContactId,
                    InvoiceDetails = model.InvoiceDetails,
                    Reference = model.Reference,
                    Price = model.InvoiceDetails.Sum(p=>p.Price),
                    PaidDate = null,
                    WorkLocationId = model.WorkLocationId
                };
                invoice.Add();
                

                // Only mark the quote finished if the invoice was successfully created
                Quote quote = Quote.GetQuote(model.QuoteId);
                quote.MarkAsFinished();
                return Json(new {Success = true});
            }

            return Json(new {Success = false, errorMessage="ModelState is invalid"});
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

        public ActionResult GetPdfInvoice(string invoiceId)
        {
            if (invoiceId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceViewModel invoice = new InvoiceViewModel(invoiceId);
            if (invoice.InvoiceId == null)
            {
                return HttpNotFound();
            }

            return View(invoice);

            //return new ViewAsPdf(invoice)
            //{
            //    FileName = invoice.InvoiceId,
            //    PageSize = Size.A4,
            //    PageOrientation = Orientation.Portrait,
            //    PageMargins = { Left = 15, Bottom = 15, Right = 15, Top = 15 },
            //    IsLowQuality = false,
            //    MinimumFontSize = 14
            //};
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
