using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using QuoteApp.Database.Contact;
using QuoteApp.Database.Pdf;
using QuoteApp.Database.Quote;
using QuoteApp.Database.Work;
using QuoteApp.Helpers;

namespace QuoteApp.Controllers
{
    public class QuoteController : Controller
    {
        // GET: /Quote/
        public ActionResult Index()
        {
            Quote quote = new Quote();
            return View(quote.GetQuotes());
        }

        // GET: /Quote/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = new Quote();
            quote = quote.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }

            WorkFromView model = new WorkFromView()
            {
                QuoteRef = quote.QuoteId,
                QuoteDate = quote.QuoteDate.ToString("dd-MM-yyyy"),
                ContactDetails = new ContactDetails(quote.Contact, quote.WorkLocation),
                CourtWorkDetails = CourtWorkDetail.GetCourtWorkDetails(quote.QuotedWorks.ToList())
            };

            //string header = ToHtml("_PdfHeader", new ViewDataDictionary{ {"QuoteRef", model.QuoteRef}, {"QuoteDate", model.QuoteDate}});
            //string footer = ToHtml("_PdfFooter", new ViewDataDictionary());

            //string cusomtSwitches = string.Format("--print-media-type --footer-center {0} --footer-spacing -10 --header-center {1} --header-spacing -10",footer, header);

            return View(model);

            //return new ViewAsPdf(model)
            //{
            //    FileName = quote.QuoteId,
            //    PageSize = Size.A4,
            //    PageOrientation = Orientation.Portrait,
            //    PageMargins = { Left = 15, Bottom = 15, Right = 15, Top = 15 },
            //    IsLowQuality = false,
            //    MinimumFontSize = 14,
            //    CustomSwitches = cusomtSwitches
            //};
        }

        private string ToHtml(string viewToRender, ViewDataDictionary viewData)
        {
            var result = ViewEngines.Engines.FindView(ControllerContext, viewToRender, null);

            StringWriter output;
            using (output = new StringWriter())
            {
                var viewContext = new ViewContext(ControllerContext, result.View, viewData, 
                    ControllerContext.Controller.TempData, output);
                result.View.Render(viewContext, output);
                result.ViewEngine.ReleaseView(ControllerContext, result.View);
            }

            return output.ToString();
        }

        public ActionResult Footer()
        {
            return View("_PdfFooter");
        }

        public ActionResult Header()
        {
            return View("_PdfHeader");
        }

        // GET: /Quote/Create
        public ActionResult Create()
        {
            Work work = new Work();
            WorkArea workArea = new WorkArea();
            QuoteViewModel model = new QuoteViewModel()
            {

                WorkTypes = workArea.GetWorkAreas().Select(area => area.WorkAreaName).ToList(),

                Works = work.GetWorkViewModelsForWorks()
            };
            return View(model);
        }

        [HttpPost]
        public JsonResult CreateQuote(string jsonString)
        {
            WorkFromView works = JsonConvert.DeserializeObject<WorkFromView>(jsonString);
            WorkLocation worklocation = new WorkLocation();
            int locationId = worklocation.CheckAndUpdateLocation(works.ContactDetails.ClubId, works.ContactDetails.ClubAddress, works.ContactDetails.ClubName);
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
            ScheduleWorkViewModel model = new ScheduleWorkViewModel(Quote.GetWorksForId(quoteId))
            {
                NumberOfDays = 1,
                QuoteId = quoteId,
            };
            return View(model);
        }

        [HttpPost]
        public JsonResult ScheduleQuoteJson(string scheduleWorkViewModel)
        {
            ScheduleWorkViewModel model = JsonConvert.DeserializeObject<ScheduleWorkViewModel>(scheduleWorkViewModel);
            if (ModelState.IsValid)
            {
                if (!model.QuotedWorks.Any())
                {
                    return Json(new { errorMessage = "Didn't capture any work" }, JsonRequestBehavior.AllowGet);
                }
                AcceptedWork acceptedWork = new AcceptedWork(); // will be changed once the service is implemented
                acceptedWork.Add(model.QuotedWorks, model.QuoteId);

                // TODO: extract this
                Quote quote = new Quote();
                quote = quote.Find(model.QuoteId);
                if (quote != null)
                {
                    quote.ScheduledFor = DateTime.ParseExact(model.WorkStarts, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    quote.JobLength = model.NumberOfDays;
                    quote.SaveChanges();
                }
                return Json(new { Success = true, Message = "Work scheduled" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { errorMessage = "ModelState invalid:" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ArchiveQuote(string quoteId)
        {
            if (quoteId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = new Quote();
            quote = quote.Find(quoteId);
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
            Quote quote = new Quote();
            quote = quote.Find(quoteId);
            quote.Archived = true;
            quote.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        // GET: /Quote/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = new Quote();
            quote = quote.Find(id);
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
                quote.SaveChanges();
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
            GeneratedPdf pdf = new GeneratedPdf();
            pdf = pdf.Find(quoteRef);
            if (pdf == null)
            {
                pdf = new GeneratedPdf()
                {
                    Content = content,
                    Reference = quoteRef
                };
                pdf.Add();
            }
            else
            {
                pdf.Content = content;
                pdf.SaveChanges();
            }

            return Json(new { Success = true });

        }

        public FileContentResult GetPdf(string reference)
        {
            GeneratedPdf pdf = new GeneratedPdf();
            pdf = pdf.Find(reference);
            MemoryStream stream = new MemoryStream(pdf.Content);
            string fileName = reference + ".pdf";
            return File(stream.ToArray(), MimeMapping.GetMimeMapping(fileName), fileName);
        }

        // GET: /Quote/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = new Quote();
            quote = quote.Find(id);
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
            Quote quote = new Quote();
            quote = quote.Find(id);
            quote.Remove();
            return RedirectToAction("Index");
        }

        public JsonResult CheckAvailableQuoteId(string currentQuoteId)
        {
            Quote quoteModel = new Quote();
            int nextAvaialbleNumber = quoteModel.GetNextNumberForId(currentQuoteId);
            return Json(new { id = nextAvaialbleNumber, Success = true }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Cancel(string quoteid)
        {
            return View();
        }
    }
}
