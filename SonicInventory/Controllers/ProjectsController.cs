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
    public class ProjectsController : Controller
    {
        private SonicInventoryContext db = new SonicInventoryContext();

        // GET: projects
        public ActionResult Index()
        {
            return View(db.Projects.OrderBy(p => p.name).ToList());
        }

        // GET: projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: projects/Create
        public ActionResult CreatePart(int? id)
        {
            ProjectViewModel pvm = new ProjectViewModel();
            pvm.Part = new part();
            pvm.Part.project_id = id.Value;
            pvm.StatusList = db.Status.ToList();
            pvm.SubAssemblyList = db.SubAssemblies.ToList();
            pvm.DesignerList = db.Designers.ToList();
            pvm.FabricatorList = db.Fabricators.ToList();
            return View(pvm);
        }

        // POST: projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,description")] project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // POST: projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePart(ProjectViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                part p = viewModel.Part;
                project prj = db.Projects.Find(viewModel.Part.project_id);
                subAssembly sa = db.SubAssemblies.Find(p.subAssy_id);
                string dnum = prj.name + sa.Abbreviation + "-" + p.drawingType + "-" + p.detailNo;
                p.drawingNo = dnum;
                db.Parts.Add(p);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = p.project_id });
            }

            return View();
        }

        // GET: projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProjectViewModel pvm = new ProjectViewModel();
            pvm.Project = db.Projects.Find(id);
            pvm.Parts = db.Parts.Where(p => p.project_id == id).ToList();

            if (pvm == null)
            {
                return HttpNotFound();
            }
            return View(pvm);
        }

        // POST: projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,description")] project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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
