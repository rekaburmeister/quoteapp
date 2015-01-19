using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using QuoteApp.Helpers;
using QuoteApp.Models;
using Rotativa;
using Rotativa.Options;

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

            WorkFromView model = new WorkFromView()
            {
                QuoteRef = quote.QuoteId,
                QuoteDate = quote.QuoteDate.ToString("d"),
                ContactDetails = new ContactDetails(quote.Contact, quote.WorkLocation),
                CourtWorkDetails = CourtWorkDetail.GetCourtWorkDetails(quote.QuotedWorks.ToList())
            };
            return new ViewAsPdf(model) { FileName = quote.QuoteId, 
                                          PageSize = Size.A4, 
                                          PageOrientation = Orientation.Portrait, 
                                          PageMargins = { Left = 15, Bottom = 15, Right = 15, Top = 15 }, 
                                          IsLowQuality = false, 
                                          MinimumFontSize = 14 };
        }

        // GET: /Quote/Create
        public ActionResult Create()
        {
            QuoteViewModel model = new QuoteViewModel()
            {
                WorkTypes = m_DbContext.WorkAreas.Select(area => area.WorkAreaName).ToList(),
                Works = m_DbContext.Works.ToList().Select(work => new WorkViewModel(work)).ToList()
            };
            return View(model);
        }

        public JsonResult CreateQuote(string jsonString)
        {
            WorkFromView works = (WorkFromView)JsonConvert.DeserializeObject(jsonString, typeof(WorkFromView));
            int locationId = WorkLocation.CheckAndUpdateLocation(works.ContactDetails.ClubId, works.ContactDetails.ClubAddress, works.ContactDetails.ClubName);
            int contactId = Contact.CheckAndUpdateContact(works.ContactDetails.ContactId, works.ContactDetails.ContactName,
                works.ContactDetails.ContactEmail, works.ContactDetails.ContactNumber, locationId);
            Quote.CreateQuote(works.QuoteRef, locationId, contactId, works.QuoteDate, works.CourtWorkDetails);
            return Json(new { Success = true, Message = "Quoted work added" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerifyQuoteId(string quoteId)
        {
            return Json(new { Success = Quote.DoesQuoteIdExist(quoteId) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ScheduleQuote(string quoteId)
        {
            return View(new ScheduleWorkViewModel { NumberOfDays = 1, QuoteId = quoteId });
        }

        [HttpPost, ActionName("ScheduleQuote")]
        [ValidateAntiForgeryToken]
        public ActionResult ScheduleQuote([Bind(Include = "QuoteId,NumberOfDays,WorkStarts")] ScheduleWorkViewModel model)
        {
            if (ModelState.IsValid)
            {
                Quote quote = m_DbContext.Quotes.Find(model.QuoteId);
                if (quote != null)
                {
                    quote.ScheduledFor = DateTime.ParseExact(model.WorkStarts, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    quote.JobLength = model.NumberOfDays;
                    m_DbContext.Entry(quote).State = EntityState.Modified;
                    m_DbContext.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ArchiveQuote(string quoteId)
        {
            if (quoteId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = m_DbContext.Quotes.Find(quoteId);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        [HttpPost, ActionName("ArchiveQuote")]
        [ValidateAntiForgeryToken]
        public ActionResult ArchiveConfirmed(string quoteId)
        {
            Quote quote = m_DbContext.Quotes.Find(quoteId);
            quote.Archived = true;
            m_DbContext.Entry(quote).State = EntityState.Modified;
            m_DbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
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
        public ActionResult Edit([Bind(Include = "QuoteId,QuoteDate")] Quote quote)
        {
            if (ModelState.IsValid)
            {
                m_DbContext.Entry(quote).State = EntityState.Modified;
                m_DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quote);
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult GeneratePdf(string html, string header, string footer, string quoteRef)
        {
            html = PdfHelper.FormatBasePdf(html, Server.MapPath("~"));
            MemoryStream stream = new MemoryStream();
            try
            {
                pdfcrowd.Client client = new pdfcrowd.Client("reka_burmeister", "d1023d55b5e3eeb4660c3e8f60188b12");
                client.enableBackgrounds();
                client.enableHyperlinks();
                client.enableImages();
                client.setHeaderHtml(header);
                client.setFooterHtml(footer);
                client.setPageWidth("8.267in");
                client.setPageHeight("11.692in");
                client.setVerticalMargin("1.8in");
                client.convertHtml(html, stream);

            }
            catch (Exception exception)
            {
                return Json(new { errorMessage = exception.Message });
            }

            byte[] content = new byte[stream.Length];
            stream.Read(content, 0, Convert.ToInt32(stream.Length));
            GeneratedPdf pdf = m_DbContext.GeneratedPdfs.Find(quoteRef);
            if (pdf == null)
            {
                pdf = new GeneratedPdf()
                {
                    Content = content,
                    Reference = quoteRef
                };
                m_DbContext.GeneratedPdfs.Add(pdf);
            }
            else
            {
                pdf.Content = content;
                m_DbContext.Entry(pdf).State = EntityState.Modified;
            }

            m_DbContext.SaveChanges();

            return Json(new { Success = true });

        }

        public FileContentResult GetPdf(string reference)
        {
            GeneratedPdf pdf = m_DbContext.GeneratedPdfs.Find(reference);
            MemoryStream stream = new MemoryStream(pdf.Content);
            string fileName = reference + ".pdf";
            return File(stream.ToArray(), MimeMapping.GetMimeMapping(fileName), fileName);
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
