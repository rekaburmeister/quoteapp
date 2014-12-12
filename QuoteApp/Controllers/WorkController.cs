using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuoteApp.Models;

namespace QuoteApp.Controllers
{
    public class WorkController : Controller
    {
        private ApplicationDbContext m_DbContext = new ApplicationDbContext();

        // GET: /Work/
        public ActionResult Index()
        {
            return View(m_DbContext.WorkAreas.ToList());
        }

        public PartialViewResult WorkDescriptions()
        {
            return PartialView(m_DbContext.Works.ToList());
        }


        public PartialViewResult CreateWork()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult CreateWork(WorkViewModel work)
        {
            if (ModelState.IsValid)
            {
                Work workObject = new Work()
                {
                    WorkArea = m_DbContext.WorkAreas.Find(work.WorkAreaId),
                    WorkDescription = work.WorkDescription,
                    WorkName = work.WorkName,
                    WorkPrice = work.WorkPrice
                };
                m_DbContext.Works.Add(workObject);
                m_DbContext.SaveChanges();
                return PartialView();
            }
            return PartialView();
        }

        public ActionResult GetWorkAreasBySearchTerm(string term)
        {
            List<WorkArea> workAreas =
                m_DbContext.WorkAreas.Where(workArea => workArea.WorkAreaName.StartsWith(term.ToLower())).ToList();
            IEnumerable<SelectListItem> results =
                workAreas.Select(workArea => new SelectListItem { Value = workArea.WorkAreaId.ToString(), Text = workArea.WorkAreaName });
            return Json(results.ToArray(), JsonRequestBehavior.AllowGet);
        }

        // POST: /Work/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        public ActionResult Create(string workAreaName)
        {
            if (ModelState.IsValid)
            {
                m_DbContext.WorkAreas.Add(new WorkArea{WorkAreaName = workAreaName});
                m_DbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


        // POST: /Work/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="WorkAreaId,WorkAreaName")] WorkArea workarea)
        {
            if (ModelState.IsValid)
            {
                m_DbContext.Entry(workarea).State = EntityState.Modified;
                m_DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workarea);
        }

        // POST: /Work/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkArea workarea = m_DbContext.WorkAreas.Find(id);
            m_DbContext.WorkAreas.Remove(workarea);
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
