using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using QuoteApp.Models;

namespace QuoteApp.Controllers
{
    public class WorkLocationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Location/
        public ActionResult Index()
        {
            return View(db.WorkLocations.ToList());
        }

        // GET: /Location/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location worklocation = db.WorkLocations.Find(id);
            if (worklocation == null)
            {
                return HttpNotFound();
            }
            return View(worklocation);
        }

        // GET: /Location/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Location/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="WorkLocationId,WorkLocationName,Address1,Address2,Address3,Town,Country,PostCode,PhoneNumber,Email")] Location worklocation)
        {
            if (ModelState.IsValid)
            {
                db.WorkLocations.Add(worklocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(worklocation);
        }

        // GET: /Location/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location worklocation = db.WorkLocations.Find(id);
            if (worklocation == null)
            {
                return HttpNotFound();
            }
            return View(worklocation);
        }

        // POST: /Location/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="WorkLocationId,WorkLocationName,Address1,Address2,Address3,Town,Country,PostCode,PhoneNumber,Email")] Location worklocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(worklocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(worklocation);
        }

        // GET: /Location/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location worklocation = db.WorkLocations.Find(id);
            if (worklocation == null)
            {
                return HttpNotFound();
            }
            return View(worklocation);
        }

        // POST: /Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Location worklocation = db.WorkLocations.Find(id);
            db.WorkLocations.Remove(worklocation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetClubsBySearchTerm(string term)
        {
            IEnumerable<WorkLocationJson> clubs = Location.GetClubsWithTerm(term).Select(location => new WorkLocationJson{Address = location.GetAddress(),
                                                                                                                              id = location.WorkLocationId,
                                                                                                                              label = location.WorkLocationName,
                                                                                                                              text = location.WorkLocationName});
            string jsonstring = JsonConvert.SerializeObject(clubs);
            return Json(jsonstring, JsonRequestBehavior.AllowGet);
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
