using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
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

        public FileResult DownloadFile(string file)
        {
            string[] filePath = file.Split('_');
            byte[] fileBytes = System.IO.File.ReadAllBytes(file);
            var response = new FileContentResult(fileBytes, "application/octet-stream");
            response.FileDownloadName = filePath[2];
            return response;
        }

        public ActionResult DeleteFile(int id)
        {
            part p = db.Parts.Find(id);
            var logopath = p.file;

            //Delete Logo File from server
            System.IO.File.Delete(logopath);
            p.file = "";
            db.SaveChanges();
            if (p.file == "")
            {
                TempData["SuccessMsg"] = "PO File successfully deleted.";
            }
            else
            {
                TempData["SuccessMsg"] = "Error- PO File was not deleted.";
            }
            return RedirectToAction("EditPart", new { id = p.id });
        }

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
            pvm.DrawingTypeList = db.DrawingTypes.ToList();
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
                //handle if null logo
                if (viewModel.PartFile != null)
                {
                    //Use Namespace called :  System.IO  
                    string FileName = Path.GetFileNameWithoutExtension(viewModel.PartFile.FileName);

                    //To Get File Extension  
                    string FileExtension = Path.GetExtension(viewModel.PartFile.FileName);

                    //Add Current Date To Attached File Name  
                    FileName = DateTime.Now.ToString("yyyyMMdd_hh-mm") + "_" + FileName.Trim() + FileExtension;

                    //Get Upload path from Web.Config file AppSettings.  
                    string UploadPath = ConfigurationManager.AppSettings["PartPath"].ToString();

                    //Its Create complete path to store in server.  
                    viewModel.Part.file = Server.MapPath("~/PartFiles/") + FileName;

                    //To copy and save file into server.  
                    viewModel.PartFile.SaveAs(viewModel.Part.file);
                }

                part p = viewModel.Part;
                project prj = db.Projects.Find(viewModel.Part.project_id);
                subAssembly sa = db.SubAssemblies.Find(p.subAssy_id);
                drawingType dt = db.DrawingTypes.Find(p.drawingType);

                string dnum = prj.name + sa.Abbreviation + "-" + dt.Abbreviation + "-" + p.detailNo;
                p.drawingNo = dnum;
                p.wasModified = false;
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

            pvm.StatusList = db.Status.ToList();
            pvm.SubAssemblyList = db.SubAssemblies.ToList();
            pvm.DesignerList = db.Designers.ToList();
            pvm.FabricatorList = db.Fabricators.ToList();
            pvm.DrawingTypeList = db.DrawingTypes.ToList();

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

        // GET: projects/Create
        public ActionResult EditPart(int? id)
        {
            ProjectViewModel pvm = new ProjectViewModel();
            pvm.Part = db.Parts.Find(id);
            pvm.StatusList = db.Status.ToList();
            pvm.SubAssemblyList = db.SubAssemblies.ToList();
            pvm.DesignerList = db.Designers.ToList();
            pvm.FabricatorList = db.Fabricators.ToList();
            pvm.DrawingTypeList = db.DrawingTypes.ToList();
            return View(pvm);
        }

        // POST: projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPart(ProjectViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                part p = db.Parts.Find(viewModel.Part.id);
                string partDrawingNo = p.drawingNo;

                if (viewModel.Part.file == null)
                {
                    //handle if null logo
                    if (viewModel.PartFile != null)
                    {
                        //Use Namespace called :  System.IO  
                        string FileName = Path.GetFileNameWithoutExtension(viewModel.PartFile.FileName);

                        //To Get File Extension  
                        string FileExtension = Path.GetExtension(viewModel.PartFile.FileName);

                        //Add Current Date To Attached File Name  
                        FileName = DateTime.Now.ToString("yyyyMMdd_hh-mm") + "_" + FileName.Trim() + FileExtension;

                        //Get Upload path from Web.Config file AppSettings.  
                        string UploadPath = ConfigurationManager.AppSettings["PartPath"].ToString();

                        //Its Create complete path to store in server.  
                        viewModel.Part.file = Server.MapPath("~/PartFiles/") + FileName;

                        //To copy and save file into server.  
                        viewModel.PartFile.SaveAs(viewModel.Part.file);
                    }

                    p.file = viewModel.Part.file;
                }

                p.designer_id = viewModel.Part.designer_id;
                p.detailNo = viewModel.Part.detailNo;
                p.drawingNo = partDrawingNo;
                p.drawingType = viewModel.Part.drawingType;
                p.fabricator_id = viewModel.Part.fabricator_id;
                p.revNo = viewModel.Part.revNo;
                p.subAssy_id = viewModel.Part.subAssy_id;
                p.wasModified = viewModel.Part.wasModified;
                db.SaveChanges();

                return RedirectToAction("Edit", new { id = p.project_id });
            }

            return View();
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

        // GET: projects/DeletePart/5
        public ActionResult DeletePart(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectViewModel pvm = new ProjectViewModel();
            pvm.Part = db.Parts.Find(id);
            pvm.StatusList = db.Status.ToList();
            pvm.SubAssemblyList = db.SubAssemblies.ToList();
            pvm.DesignerList = db.Designers.ToList();
            pvm.FabricatorList = db.Fabricators.ToList();
            pvm.DrawingTypeList = db.DrawingTypes.ToList();

            if (pvm == null)
            {
                return HttpNotFound();
            }
            return View(pvm);
        }

        // POST: projects/Delete/5
        [HttpPost, ActionName("DeletePart")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePartConfirmed(int id)
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
