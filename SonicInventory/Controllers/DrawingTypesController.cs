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
    public class DrawingTypesController : Controller
    {
        private SonicInventoryContext db = new SonicInventoryContext();

        // GET: DrawingTypes
        public ActionResult Index()
        {
            return View(db.DrawingTypes.OrderBy(p => p.Name).ToList());
        }

        // GET: DrawingTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            drawingType drawingType = db.DrawingTypes.Find(id);
            if (drawingType == null)
            {
                return HttpNotFound();
            }
            return View(drawingType);
        }

        // GET: DrawingTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DrawingTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,Abbreviation")] drawingType drawingType)
        {
            if (ModelState.IsValid)
            {
                db.DrawingTypes.Add(drawingType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(drawingType);
        }

        // GET: DrawingTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            drawingType drawingType = db.DrawingTypes.Find(id);
            if (drawingType == null)
            {
                return HttpNotFound();
            }
            return View(drawingType);
        }

        // POST: DrawingTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Abbreviation")] drawingType drawingType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(drawingType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(drawingType);
        }

        // GET: DrawingTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            drawingType drawingType = db.DrawingTypes.Find(id);
            if (drawingType == null)
            {
                return HttpNotFound();
            }
            return View(drawingType);
        }

        // POST: DrawingTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            drawingType drawingType = db.DrawingTypes.Find(id);
            db.DrawingTypes.Remove(drawingType);
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
