using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vaultlife.Models;

namespace Vaultlife.Controllers
{
    public class LanguageController : Controller
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();

        // GET: /Language/
        public ActionResult Index()
        {
            return View(db.Languages.ToList());
        }

        // GET: /Language/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Language language = db.Languages.Find(id);
            if (language == null)
            {
                return HttpNotFound();
            }
            return View(language);
        }

        // GET: /Language/Create
        public ActionResult Create()
        {
            Language model = new Language();
            model = (Language)Helpers.TableTracker.TrackCreate(model, User.Identity.Name);
            return View(model);
        }


        // POST: /Language/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="LanguageID,LanguageName,LanguageTwoLetters,LanguageThreeLetters")] Language language)
        {
            if (ModelState.IsValid)
            {
                db.Languages.Add(language);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(language);
        }

        // GET: /Language/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Language language = db.Languages.Find(id);
            if (language == null)
            {
                return HttpNotFound();
            }
            return View(language);
        }

        // POST: /Language/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="LanguageID,LanguageName,LanguageTwoLetters,LanguageThreeLetters")] Language language)
        {
            if (ModelState.IsValid)
            {
                db.Entry(language).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(language);
        }

        // GET: /Language/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Language language = db.Languages.Find(id);
            if (language == null)
            {
                return HttpNotFound();
            }
            return View(language);
        }

        // POST: /Language/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Language language = db.Languages.Find(id);
            db.Languages.Remove(language);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Clone()
        {
           
            ViewBag.LanguageID = new SelectList(db.Languages, "LanguageID", "LanguageName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Clone([Bind(Include = "LanguageID,LanguageName,LanguageTwoLetters,LanguageThreeLetters")] Language language)
        {
            if (ModelState.IsValid)
            {
                //Get old ID
                int oldID = language.LanguageID;

                var entries = from e in db.LanguageItems where e.LanguageID == oldID select e;

                db.Languages.Add(language);
                db.SaveChanges();
                int NewID = language.LanguageID;
            
                
                foreach (var Item in entries)

                {
                    LanguageItem LanguageItems = new LanguageItem();
                    LanguageItems.LanguageID = NewID;
                    LanguageItems.LanguageItemKey=Item.LanguageItemKey;
                    LanguageItems.LanguageItemValue=Item.LanguageItemValue;
                    db.LanguageItems.Add(LanguageItems);  
                };

                db.SaveChanges();
             return RedirectToAction("Index");
               
            }
            return View(language);
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
