using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VaultLifeAdmin.Models;
using VaultLifeAdmin.Models.Games;
using Quartz;
using Quartz.Impl;
using System.Data.SqlClient;
using System.Globalization;
using VaultLifeAdmin.ViewModels;
using FluentValidation.Results;
using System.Text;
using VaultLifeAdmin.Helpers;
using System.Web.Script.Serialization;



namespace VaultLifeAdmin.Controllers
{
    public class GamesController : Controller
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();

        public ActionResult Index()
        {
            var games = db.Games.Include(g => g.GameType).Include(g => g.NextGame);
            return View(games.ToList());
        }
         
        public ActionResult IndexMakeGame()
        {
           //Kills Game session
            if (Session["game"] != null)
            {
                Session.Remove("game");
            }

            var games = db.Games.Include(g => g.GameType).Include(g => g.NextGame).OrderByDescending(g=>g.DateUpdated);
            return View(games.ToList());
        }

        public ActionResult StartAll()
        {
            List<Game> games = db.Games.Where(g => g.GameState != "COMPLETED" && g.GameTypeID == 1).ToList();
            foreach (Game game in games) {
                StartGamego(game.GameID);
            }
            return Redirect("/");
        }
        
        /// GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            ViewBag.GameTypeID = new SelectList(db.GameTypes, "GameTypeID", "GameTypeCode");
            ViewBag.GameID = new SelectList(db.NextGames, "GameID", "USR");
            Game model = new Game();
            model = (Game)Helpers.TableTracker.TrackCreate(model, "USR");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GameID,GameCode,GameTypeID,GameName,GameDescription,DateInserted,DateUpdated,USR")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Games.Add(game);
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }

                }
                return RedirectToAction("Index");
            }

            ViewBag.GameTypeID = new SelectList(db.GameTypes, "GameTypeID", "GameTypeCode", game.GameTypeID);
            ViewBag.GameID = new SelectList(db.NextGames, "GameID", "USR", game.GameID);
            return View(game);
        }

                // GET: ProductInGames/Create
        public ActionResult MakeGame()
        {
            ViewBag.GameTypeID = new SelectList(db.GameTypes, "GameTypeID", "GameTypeCode");
            ViewBag.GameID = new SelectList(db.Games, "GameID", "GameName");
            ViewBag.NextGameID = new SelectList(db.Games, "GameID", "GameName");
            ViewBag.MemberSubscriptionType = new SelectList(db.MemberSubscriptionTypes, "MemberSubscriptionTypeID", "MemberSubscriptionTypeDescription");
            Game model = new Game();
            model = (Game)Helpers.TableTracker.TrackCreate(model, "USR");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeGame([Bind(Include = "NumberOfWinners,MemberSubscriptionType,GameID,GameCode,GameTypeID,GameName,GameDescription,NextGameID,DateInserted,DateUpdated,USR")] Game game)
        {


            if (ModelState.IsValid)
            {
                db.Games.Add(game);
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }

                }
                return RedirectToAction("EditMakeGame", new { id = game.GameID });
            }

            ViewBag.GameTypeID = new SelectList(db.GameTypes, "GameTypeID", "GameTypeCode", game.GameTypeID);
            ViewBag.GameID = new SelectList(db.Games, "GameID", "GameName", game.GameID);
            ViewBag.NextGameID = new SelectList(db.Games, "GameID", "GameName");
            ViewBag.MemberSubscriptionType = new SelectList(db.MemberSubscriptionTypes, "MemberSubscriptionTypeID", "MemberSubscriptionTypeDescription");
            return View(game);
        }


        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game model = db.Games.Find(id);
            model = (Game)Helpers.TableTracker.TrackEdit(model, "USR");

            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.GameTypeID = new SelectList(db.GameTypes, "GameTypeID", "GameTypeCode", model.GameTypeID);
            ViewBag.GameID = new SelectList(db.Games, "GameID", "GameName", model.GameID);
            model = (Game)TableTracker.TrackEdit(model, "USR");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberSubscriptionType,GameID,GameCode,GameTypeID,GameName,GameDescription,DateInserted,DateUpdated,USR")] Game game)
        {
            game = (Game)TableTracker.TrackEdit(game, "USR");

            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GameTypeID = new SelectList(db.GameTypes, "GameTypeID", "GameTypeCode", game.GameTypeID);
            ViewBag.GameID = new SelectList(db.Games, "GameID", "GameName", game.GameID);
            ViewBag.NextGameID = new SelectList(db.Games, "GameID", "GameName",game.NextGameID);
            ViewBag.MemberSubscriptionType = new SelectList(db.MemberSubscriptionTypes, "MemberSubscriptionTypeID", "MemberSubscriptionTypeDescription",game.MemberSubscriptionType);
            return View(game);
        }


        // GET: Games/Edit/5
        public ActionResult EditMakeGame(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Game game = db.Games.Include(x => x.ProductInGames).Include(x=>x.GameRules).Where(x => x.GameID == id).First();
            game = (Game)TableTracker.TrackEdit(game, "USR");

            ViewBag.GameTypeID = new SelectList(db.GameTypes, "GameTypeID", "GameTypeCode", game.GameTypeID);
            ViewBag.GameTypeIDList = new SelectList(db.GameTypes, "GameTypeID", "GameTypeCode", game.GameTypeID);
            ViewBag.GameID = new SelectList(db.Games, "GameID", "GameName", game.GameID);
            ViewBag.NextGameID = new SelectList(db.Games, "GameID", "GameName",game.NextGameID);
            ViewBag.CurrencyIDList = new SelectList(db.Currencies, "CurrencyID", "CurrencyCode");
            ViewBag.MemberSubscriptionType = new SelectList(db.MemberSubscriptionTypes, "MemberSubscriptionTypeID", "MemberSubscriptionTypeDescription",game.MemberSubscriptionType);
            ViewBag.Products = db.Products;
            ViewBag.Source = "EditMakeGame";

            var gameRules = db.GameRules.Include(x => x.GameTemplate).Where(x => x.GameID == id).OrderBy(x => x.GameTemplate.OrderInGame).ToList();

            GameViewModel gameVM = new GameViewModel { Game = game, ProductInGames = game.ProductInGames, GameRules = gameRules };

            ViewBag.Hours = new SelectList(getHours(), "Value", "text");
            ViewBag.Minutes = new SelectList(getMinutes(), "Value", "text");
            ViewBag.Seconds = new SelectList(getSeconds(), "Value", "text");

            return View(gameVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMakeGame(GameViewModel gameVM)
        {
            StringBuilder sb = new StringBuilder();
                //HORRIBLE HACK
           // gameVM.Game.NumberOfWinners = 1;
            if (ModelState.IsValid)
            {
               
                db.Entry(gameVM.Game).State = EntityState.Modified;
                     db.SaveChanges();

                if (gameVM.ProductInGames != null)
                {
                    foreach (var d in gameVM.ProductInGames)
                    {
                        ProductInGameValidator validator = new ProductInGameValidator();

                        FluentValidation.Results.ValidationResult results = validator.Validate(d);
                        IList<ValidationFailure> failures = results.Errors;
                       
                        foreach (var e in results.Errors)
                        {

                            ModelState.AddModelError(e.PropertyName + "Error", e.ErrorMessage);
                            sb.AppendLine(e.ErrorMessage);

                        }

                        if (ModelState.IsValid)
                        {

                            db.Entry(d).State = EntityState.Modified;
                            db.SaveChanges();

                            foreach (var l in d.ProductLocations)
                            {
                                db.Entry(l).State = EntityState.Modified;
                                 db.SaveChanges();


                            }
                        }
                        else
                        {
                            ViewBag.ErrorMessage = sb.ToString();
                            ViewBag.GameTypeID = new SelectList(db.GameTypes, "GameTypeID", "GameTypeCode", gameVM.Game.GameTypeID);
                            ViewBag.GameTypeIDList = new SelectList(db.GameTypes, "GameTypeID", "GameTypeCode", gameVM.Game.GameTypeID);
                            ViewBag.GameID = new SelectList(db.NextGames, "GameID", "USR", gameVM.Game.GameID);
                            ViewBag.CurrencyIDList = new SelectList(db.Currencies, "CurrencyID", "CurrencyCode");
                            ViewBag.MemberSubscriptionType = new SelectList(db.MemberSubscriptionTypes, "MemberSubscriptionTypeID", "MemberSubscriptionTypeDescription",gameVM.Game.MemberSubscriptionType);
                            ViewBag.Products = db.Products;
                            ViewBag.Source = "EditMakeGame";
                            gameVM.ProductInGames = db.ProductInGames.Where(x => x.GameID == gameVM.Game.GameID);

                            ViewBag.Hours = new SelectList(getHours(), "Value", "text");
                            ViewBag.Minutes = new SelectList(getMinutes(), "Value", "text");
                            ViewBag.Seconds = new SelectList(getSeconds(), "Value", "text");

                            return View(gameVM);
                        }

                    }
                }
                if (gameVM.GameRules != null)
                {
                    //Removed this to enable Long running games
                    // DateTime rule1DateTime = gameVM.GameRules.Single(gr => gr.GameRuleCode.ToLower() == "preparegamerule").ExcecuteTime;
                    //DateTime rule1DateTime = db.GameRules.Include(x=>x.GameTemplate).Where(x=>x.GameTemplate.OrderInGame ==1).First().ExcecuteTime;
                   // DateTime rulesDateTime = db.games

                    foreach (var rule in gameVM.GameRules)
                    {
                        //all rules must run on same date as rule 1 WHat about TimeZones
                        rule.ExcecuteTime = DateTime.Parse(rule.ExcecuteTime.ToString("yyyy/MM/dd") + " " + rule.ExecuteHhMmSs);

                        db.Entry(rule).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("IndexMakeGame");
            }
            ViewBag.CurrencyIDList = new SelectList(db.Currencies, "CurrencyID", "CurrencyCode");
            ViewBag.GameTypeID = new SelectList(db.GameTypes, "GameTypeID", "GameTypeCode", gameVM.Game.GameTypeID);
            ViewBag.GameTypeIDList = new SelectList(db.GameTypes, "GameTypeID", "GameTypeCode", gameVM.Game.GameTypeID);
            ViewBag.GameID = new SelectList(db.NextGames, "GameID", "USR", gameVM.Game.GameID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductSKUCode");
            ViewBag.MemberSubscriptionType = new SelectList(db.MemberSubscriptionTypes, "MemberSubscriptionTypeID", "MemberSubscriptionTypeDescription", gameVM.Game.MemberSubscriptionType);
            return View(gameVM);
        }


        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult StartGame()
        {

            return View();
        }

        public int StartGamego(int GameID)
        {

            var Game = db.Games.Find(GameID);

            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            FFFGame mygame = new FFFGame(db, scheduler, Game);
            mygame.makeReleased();
            return 1;

        }

        public ActionResult AccessDenied()
        {
            return View();
        }

/*        public int CreateGame()
        {
            AspNetUser user = db.AspNetUsers.Where(x => x.UserName == "USR").First();
            Member member = db.Members.Where(m => m.ASPUserId == user.Id).First();
            // call the stored procedure to get the return value
            System.Nullable<int> iReturnValue = db.MakeANewGame2(member.MemberID).SingleOrDefault();
            return (iReturnValue.HasValue) ? (int)iReturnValue : 0;
        }
        */


        private List<SelectListItem> getSeconds()
        {

            string xstr = "";
            List<SelectListItem> Seconds = new List<SelectListItem>();
            for (int x = 0; x < 60; x++)
            {
                if (x < 10)
                {
                    xstr = "0" + x.ToString();
                }
                else
                {
                    xstr = x.ToString();
                }
                Seconds.Add(new SelectListItem
                {
                    Text = xstr,
                    Value = xstr
                });
            }
            return Seconds;
        }
        private List<SelectListItem> getMinutes()
        {

            string xstr = "";
            List<SelectListItem> Minutes = new List<SelectListItem>();
            for (int x = 0; x < 60; x++)
            {
                if (x < 10)
                {
                    xstr = "0" + x.ToString();
                }
                else
                {
                    xstr = x.ToString();
                }
                Minutes.Add(new SelectListItem
                {
                    Text = xstr,
                    Value = xstr
                });
            }
            return Minutes;
        }
        private List<SelectListItem> getHours()
        {
            string xstr = "";
            //Drop downs for Game Rules
            List<SelectListItem> Hours = new List<SelectListItem>();
            for (int x = 0; x < 24; x++)
            {
                if (x < 10)
                {
                    xstr = "0" + x.ToString();
                }
                else
                {
                    xstr = x.ToString();
                }
                Hours.Add(new SelectListItem
                {
                    Text = xstr,
                    Value = xstr
                });

            }
            return Hours;
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
