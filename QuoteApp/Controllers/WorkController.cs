using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuoteApp.Database.Work;

namespace QuoteApp.Controllers
{
    public class WorkController : Controller
    {
        // GET: /Work/
        public ActionResult Index()
        {
            WorkArea workArea = new WorkArea();
            
            return View(workArea.GetWorkAreas());
        }

        public PartialViewResult WorkDescriptions()
        {
            Work work = new Work();
            return PartialView(work.GetWorks());
        }


        public PartialViewResult CreateWork()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult CreateWork(WorkViewModel work)
        {
            if (ModelState.IsValid)
            {
                WorkArea workArea = new WorkArea();
                Work workObject = new Work()
                {
                    WorkArea = workArea.Find(work.WorkAreaId),
                    WorkDescription = work.WorkDescription,
                    WorkName = work.WorkName,
                    WorkPrice = work.WorkPrice
                };
                workObject.Add();
                
                return Json(new { Success = true});
            }
            return Json(new { errorMessage = "ModelState invalid" });
        }

        public ActionResult GetWorkAreasBySearchTerm(string term)
        {
            WorkArea workArea = new WorkArea();
            List<WorkArea> workAreas = workArea.GetWorkAreasBySearchTerm(term);
                
            IEnumerable<SelectListItem> results =
                workAreas.Select(area => new SelectListItem { Value = area.WorkAreaId.ToString(), Text = area.WorkAreaName });
            return Json(results.ToArray(), JsonRequestBehavior.AllowGet);
        }

        // POST: /Work/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        public ActionResult Create(string workAreaName)
        {
            if (ModelState.IsValid)
            {
                WorkArea area = new WorkArea {WorkAreaName = workAreaName};
                area.Add();

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
                workarea.SaveChanges();
                
                return RedirectToAction("Index");
            }
            return View(workarea);
        }

        // POST: /Work/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            WorkArea workarea = new WorkArea();
            workarea = workarea.Find(id);
            workarea.Remove();
            
            return RedirectToAction("Index");
        }
    }
}
