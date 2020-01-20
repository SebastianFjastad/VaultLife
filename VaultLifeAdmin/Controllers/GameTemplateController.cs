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
    public class GameTemplateController : Controller
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();

        // GET: /GameTemplate/
        public ActionResult Index()
        {
            var gametemplates = db.GameTemplates.Include(g => g.GameType);
            return View(gametemplates.ToList());
        }

        // GET: /GameTemplate/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameTemplate gametemplate = db.GameTemplates.Find(id);
            if (gametemplate == null)
            {
                return HttpNotFound();
            }
            return View(gametemplate);
        }

        // GET: /GameTemplate/Create
        public ActionResult Create()
        {
            ViewBag.GameTypeID = new SelectList(db.GameTypes, "GameTypeID", "GameTypeCode");
            return View();
        }

        // POST: /GameTemplate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="GameTemplateID,GameTemplateCode,GameTypeID,DateInserted,DateUpdated,USR,GameRuleCode,GameRuleDetail,OrderInGame")] GameTemplate gametemplate)
        {
            if (ModelState.IsValid)
            {
                db.GameTemplates.Add(gametemplate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GameTypeID = new SelectList(db.GameTypes, "GameTypeID", "GameTypeCode", gametemplate.GameTypeID);
            return View(gametemplate);
        }

        // GET: /GameTemplate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameTemplate gametemplate = db.GameTemplates.Find(id);
            if (gametemplate == null)
            {
                return HttpNotFound();
            }
            ViewBag.GameTypeID = new SelectList(db.GameTypes, "GameTypeID", "GameTypeCode", gametemplate.GameTypeID);
            return View(gametemplate);
        }

        // POST: /GameTemplate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="GameTemplateID,GameTemplateCode,GameTypeID,DateInserted,DateUpdated,USR,GameRuleCode,GameRuleDetail,OrderInGame")] GameTemplate gametemplate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gametemplate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GameTypeID = new SelectList(db.GameTypes, "GameTypeID", "GameTypeCode", gametemplate.GameTypeID);
            return View(gametemplate);
        }

        // GET: /GameTemplate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameTemplate gametemplate = db.GameTemplates.Find(id);
            if (gametemplate == null)
            {
                return HttpNotFound();
            }
            return View(gametemplate);
        }

        // POST: /GameTemplate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GameTemplate gametemplate = db.GameTemplates.Find(id);
            db.GameTemplates.Remove(gametemplate);
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
