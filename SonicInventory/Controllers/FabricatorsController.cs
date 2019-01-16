using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SonicInventory.Models;

namespace SonicInventory.Controllers
{
    public class FabricatorsController : Controller
    {
        private SonicInventoryContext db = new SonicInventoryContext();

        // GET: Fabricators
        public ActionResult Index()
        {
            return View(db.Fabricators.OrderBy(p => p.Name).ToList());
        }

        // GET: Fabricators/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fabricator fabricator = db.Fabricators.Find(id);
            if (fabricator == null)
            {
                return HttpNotFound();
            }
            return View(fabricator);
        }

        // GET: Fabricators/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fabricators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name")] fabricator fabricator)
        {
            if (ModelState.IsValid)
            {
                db.Fabricators.Add(fabricator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fabricator);
        }

        // GET: Fabricators/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fabricator fabricator = db.Fabricators.Find(id);
            if (fabricator == null)
            {
                return HttpNotFound();
            }
            return View(fabricator);
        }

        // POST: Fabricators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name")] fabricator fabricator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fabricator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fabricator);
        }

        // GET: Fabricators/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fabricator fabricator = db.Fabricators.Find(id);
            if (fabricator == null)
            {
                return HttpNotFound();
            }
            return View(fabricator);
        }

        // POST: Fabricators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            fabricator fabricator = db.Fabricators.Find(id);
            db.Fabricators.Remove(fabricator);
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
