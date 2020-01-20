using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VaultLifeAdmin.Models;
using VaultLifeAdmin.ViewModels;

namespace VaultLifeAdmin.Controllers
{
    public class GameRuleController : Controller
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();

        // GET: /GameRule/
        public ActionResult Index()
        {
            var gamerules = db.GameRules.Include(g => g.Game).Include(g => g.GameTemplate);
            return View(gamerules.ToList());
        }

        // GET: /GameRule/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameRule gamerule = db.GameRules.Find(id);
            if (gamerule == null)
            {
                return HttpNotFound();
            }
            return View(gamerule);
        }

        // GET: /GameRule/Create
        public ActionResult Create()
        {
            ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode");
            ViewBag.GameTemplateID = new SelectList(db.GameTemplates, "GameTemplateID", "GameTemplateCode");
            return View();
        }

        // POST: /GameRule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="GameRuleID,GameRuleCode,GameID,FilterCriteria,Schedule,ChainGameRuleID,GameRuleDetail,ExcecuteTime,DateInserted,DateUpdated,USR,GameTemplateID")] GameRule gamerule)
        {
            if (ModelState.IsValid)
            {
                db.GameRules.Add(gamerule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode", gamerule.GameID);
            ViewBag.GameTemplateID = new SelectList(db.GameTemplates, "GameTemplateID", "GameTemplateCode", gamerule.GameTemplateID);
            return View(gamerule);
        }


        public ActionResult CreateForGame(int gameID)
        {
            var game = db.Games.Find(gameID);
            ViewBag.Game = game;
            ViewBag.GameID = gameID;

            //Stored proc to make the rules before passing them to the view
            int success = db.MakeGameRules(game.GameTypeID, game.GameID, "USR");
            var gameRules = db.GameRules.Where(x => x.GameID == gameID);
            GameRuleViewModel gameruleVM = new GameRuleViewModel();

            gameruleVM.GameRules = gameRules;
            gameruleVM.Game = game;

            return RedirectToAction("EditMakeGame", "Games", new { id = gameID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateForGame(GameRuleViewModel gameruleVM)
        {
            if (ModelState.IsValid)
            {
                foreach (var gamerule in gameruleVM.GameRules)
                {
                    db.GameRules.Add(gamerule);
                    db.SaveChanges();
                }
                return RedirectToAction("EditMakeGame", "Games", new { id = gameruleVM.Game.GameID });
            }

            //ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode", gamerule.GameID);
            //ViewBag.GameTemplateID = new SelectList(db.GameTemplates, "GameTemplateID", "GameTemplateCode", gamerule.GameTemplateID);
            return View(gameruleVM);
        }


        // GET: /GameRule/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameRule gamerule = db.GameRules.Find(id);
            if (gamerule == null)
            {
                return HttpNotFound();
            }
            ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode", gamerule.GameID);
            ViewBag.GameTemplateID = new SelectList(db.GameTemplates, "GameTemplateID", "GameTemplateCode", gamerule.GameTemplateID);
            return View(gamerule);
        }

        // POST: /GameRule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="GameRuleID,GameRuleCode,GameID,FilterCriteria,Schedule,ChainGameRuleID,GameRuleDetail,ExcecuteTime,DateInserted,DateUpdated,USR,GameTemplateID")] GameRule gamerule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gamerule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode", gamerule.GameID);
            ViewBag.GameTemplateID = new SelectList(db.GameTemplates, "GameTemplateID", "GameTemplateCode", gamerule.GameTemplateID);
            return View(gamerule);
        }

        // GET: /GameRule/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameRule gamerule = db.GameRules.Find(id);
            if (gamerule == null)
            {
                return HttpNotFound();
            }
            return View(gamerule);
        }

        // POST: /GameRule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GameRule gamerule = db.GameRules.Find(id);
            db.GameRules.Remove(gamerule);
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
