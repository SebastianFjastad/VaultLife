using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using VaultLifeAdmin.Models;
namespace VaultLifeAdmin.Controllers
{
    public class BannerController : Controller
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();
        // GET: Banner
        public ActionResult Index()
        {
            return View();
        }
     [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadBanner(HttpPostedFileBase uploadFile)
        {
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["uploadFile"];

                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));


                    Banner banner = new Banner();
                    banner.BannerName = fileName;
                    banner.BannerType = 3;
                    banner.DisplayDate = DateTime.Now;
                    banner.BannerImage = fileBytes;
                    db.Banners.Add(banner);


                }
            }
            db.SaveChanges();
            return View("Index");
        }
        // GET: Banner/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Banner/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Banner/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Banner/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Banner/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Banner/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Banner/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
