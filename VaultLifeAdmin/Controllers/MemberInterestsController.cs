using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VaultLifeAdmin.Models;

namespace VaultLifeAdmin.Controllers
{
    public class MemberInterestsController : Controller
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();

        // GET: MemberInterests
        public ActionResult Index()
        {
            var memberInterests = db.MemberInterests.Include(m => m.Member).Include(m => m.ProductCategory);
            return View(memberInterests.ToList());
        }

        // GET: MemberInterests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberInterest memberInterest = db.MemberInterests.Find(id);
            if (memberInterest == null)
            {
                return HttpNotFound();
            }
            return View(memberInterest);
        }

        // GET: MemberInterests/Create
        public ActionResult Create()
        {
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "IdentityType");
            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "ProductCategoryCode");
            MemberInterest model = new MemberInterest();
            model = (MemberInterest)Helpers.TableTracker.TrackCreate(model, "USR");
            return View(model);
        }

        // POST: MemberInterests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberInterestID,MemberID,ProductCategoryID,DateInserted,DateUpdated,USR")] MemberInterest memberInterest)
        {
            if (ModelState.IsValid)
            {
                db.MemberInterests.Add(memberInterest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "IdentityType", memberInterest.MemberID);
            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "ProductCategoryCode", memberInterest.ProductCategoryID);
            return View(memberInterest);
        }

        // GET: MemberInterests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberInterest model = db.MemberInterests.Find(id);
            model = (MemberInterest)Helpers.TableTracker.TrackEdit(model, "USR");
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "IdentityType", model.MemberID);
            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "ProductCategoryCode", model.ProductCategoryID);
            return View(model);
        }

        // POST: MemberInterests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberInterestID,MemberID,ProductCategoryID,DateInserted,DateUpdated,USR")] MemberInterest memberInterest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberInterest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "IdentityType", memberInterest.MemberID);
            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "ProductCategoryCode", memberInterest.ProductCategoryID);
            return View(memberInterest);
        }

        // GET: MemberInterests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberInterest memberInterest = db.MemberInterests.Find(id);
            if (memberInterest == null)
            {
                return HttpNotFound();
            }
            return View(memberInterest);
        }

        // POST: MemberInterests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberInterest memberInterest = db.MemberInterests.Find(id);
            db.MemberInterests.Remove(memberInterest);
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
