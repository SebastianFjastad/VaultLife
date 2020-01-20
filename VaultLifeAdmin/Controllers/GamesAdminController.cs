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
    public class GamesAdminController : Controller
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();

        // GET: GamesAdmin
        public ActionResult Index()
        {
            var games = db.Games.Include(g => g.GameType).Include(g => g.MemberSubscriptionType);
            return View(games.ToList());
        }

        // GET: GamesAdmin/Details/5
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

        // GET: GamesAdmin/Create
        public ActionResult Create()
        {
            ViewBag.GameTypeID = new SelectList(db.GameTypes, "GameTypeID", "GameTypeCode");
            
            ViewBag.NextGameID = new SelectList(db.Games, "GameID", "GameCode");
            ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode");
            ViewBag.MemberSubscriptionType = new SelectList(db.MemberSubscriptionTypes, "MemberSubscriptionTypeID", "MemberSubscriptionTypeDescription");
            return View();
        }

        [HttpPost]
        public ActionResult FiveMinDeal(int productID, String numberOfWinners, String quantity, String priceInGame, String referencePrice, String address, String currencyID)
        {
            Game game = GetGame().Game;
            game.GameRules = GetGame().GameRules.ToList();
            Game fiveMin = new Game();
            fiveMin.GameCode = game.GameCode + "5MD";
            fiveMin.GameDescription = game.GameCode + "5MD";
            fiveMin.GameTypeID = 2;
            fiveMin.GameName = game.GameName + "5MD";
            fiveMin.DateInserted = DateTime.Now;
            fiveMin.DateUpdated = DateTime.Now;
            fiveMin.USR = game.USR;
            fiveMin.NumberOfWinners = Convert.ToInt32(numberOfWinners);
            fiveMin.MemberSubscriptionType = game.MemberSubscriptionType;
          //  fiveMin.restart = false;

            fiveMin.Global = game.Global;

            DateTime executeTime = game.GameRules.Where(gr => gr.GameRuleCode.Equals("ResolveActualWinners")).First().ExcecuteTime;

            db.Games.Add(fiveMin);
            fiveMin.GameRules.Add(toRule("PrepareGameRule", fiveMin.GameID, "PrepareGameRule", executeTime, 1));
            fiveMin.GameRules.Add(toRule("StartGame", fiveMin.GameID, "StartGame", executeTime.AddSeconds(20), 1));
            fiveMin.GameRules.Add(toRule("ResolvePotentialWinners", fiveMin.GameID, "ResolvePotentialWinners", executeTime.AddSeconds(30), 1));
            fiveMin.GameRules.Add(toRule("ResolveActualWinners", fiveMin.GameID, "ResolveActualWinners", executeTime.AddMinutes(5).AddSeconds(20), 1));
            db.SaveChanges(); 
            Game g = db.Games.Find(game.GameID);
            g.NextGameID = fiveMin.GameID;
            fiveMin.ProductInGames.Add(toProductInGame(productID, 0, Convert.ToDecimal(priceInGame), Convert.ToDecimal(referencePrice), Convert.ToInt32(quantity)));
            db.SaveChanges();
            fiveMin.ProductInGames.First().ProductLocations.Add(toProductLocation(fiveMin.ProductInGames.First().ProductInGameID, address));
            db.SaveChanges();
            ViewBag.CurrencyID = new SelectList(db.Currencies, "CurrencyID", "CurrencyCode");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductSKUCode");
            return RedirectToAction("../Games/IndexMakeGame");
        }

        private Models.ProductLocation toProductLocation(int productInGameId, string address)
        {
            ProductLocation pl = new ProductLocation();
            pl.ProductInGameID = productInGameId;
            pl.Address = address;
            pl.DateInserted = DateTime.Now;
            pl.DateUpdated = DateTime.Now;
            pl.USR = "Computed";
            return pl;
        }

        private ProductInGame toProductInGame(int productID, int GameId, decimal priceInGame, decimal referencePrice, int quantity)
        {
            ProductInGame pig = new ProductInGame();
            pig.ProductID = productID;
            pig.GameID = GameId;
            pig.Quantity = quantity;
            pig.CurrencyID = 1;
            pig.DateInserted = DateTime.Now;
            pig.DateUpdated = DateTime.Now;
            pig.USR = "Computed";
            pig.PriceInGame = priceInGame;
            pig.ReferencePrice = referencePrice;
            return pig;
        }

        [HttpPost]
        public ActionResult Rules(String excecuteTime, String hours, String minutes, String seconds)
        {
            GameAdminViewModel gameVM = GetGame();
            DateTime executeTime = DateTime.ParseExact(excecuteTime, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
            executeTime = executeTime.AddHours(Convert.ToDouble(hours)).AddMinutes(Convert.ToDouble(minutes)).AddSeconds(Convert.ToDouble(seconds));
            List<GameRule> rules = new List<GameRule>();
            GameRule prepareRule = toRule("PrepareGameRule", gameVM.Game.GameID, "PrepareGameRule", DateTime.Now, 1);
            GameRule startRule = toRule("StartGame", gameVM.Game.GameID, "StartGame", executeTime, 2);
            GameRule potentialRule = toRule("ResolvePotentialWinners", gameVM.Game.GameID, "ResolvePotentialWinners", executeTime.AddSeconds(10), 3);
            GameRule resolveRule = toRule("ResolveActualWinners", gameVM.Game.GameID, "ResolveActualWinners", executeTime.AddMinutes(1), 4);

            rules.Add(prepareRule);
            rules.Add(startRule);
            rules.Add(potentialRule);
            rules.Add(resolveRule);
            gameVM.GameRules = rules;

            db.GameRules.AddRange(rules);
            db.SaveChanges();
            gameVM.GameRules = rules;
            return RedirectToAction("FiveMinDeal");
        }

        private GameRule toRule(String gameRuleCode, int gameID, String GameRuleDetail, DateTime executeTime, int gameTemplate)
        {
            GameRule gameRule = new GameRule();
            gameRule.GameRuleCode = gameRuleCode;
            gameRule.GameID = gameID;
            gameRule.GameRuleDetail = GameRuleDetail;
            gameRule.ExcecuteTime = executeTime;
            gameRule.GameTemplateID = gameTemplate;
            gameRule.DateInserted = DateTime.Now;
            gameRule.DateUpdated = DateTime.Now;
            gameRule.USR = "USR";
            var specifier = "00";
            gameRule.ExecuteHhMmSs = executeTime.Hour.ToString(specifier) + ":" + executeTime.Minute.ToString(specifier) + ":" + executeTime.Second.ToString(specifier);
            return gameRule;
        }

        [HttpGet]
        public ActionResult Rules()
        {
            ViewBag.Hours = new SelectList(getHours(), "Value", "text");
            ViewBag.Minutes = new SelectList(getMinutes(), "Value", "text");
            ViewBag.Seconds = new SelectList(getSeconds(), "Value", "text");
            GameRule rule = new GameRule();
            rule.ExcecuteTime = DateTime.Now;
            return View(rule);
        }

        [HttpGet]
        public ActionResult FiveMinDeal()
        {
            ViewBag.CurrencyID = new SelectList(db.Currencies, "CurrencyID", "CurrencyCode");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductSKUCode");
            return View(new GameAdminViewModel());
        }

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
  
        // POST: GamesAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GameID,GameCode,GameTypeID,GameName,GameDescription,NumberOfWinners,Global")]Game game, string GameTypeID, string MemberSubscriptionType, string prevBtn, string nextBtn)
        {
            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    GameAdminViewModel g = GetGame();
                    game.GameTypeID = Convert.ToInt32(GameTypeID);
                    game.MemberSubscriptionType = Convert.ToInt32(MemberSubscriptionType);
                    g.Game = game;    
                    g.Game.DateInserted = DateTime.Now;
                    g.Game.DateUpdated = DateTime.Now;
             //       g.Game.restart = true;
                    g.Game.USR = "VaultLifeAdmin";
                    Session["game"] = g;
                    return RedirectToAction("ProductInGame");
                }

                ViewBag.GameTypeID = new SelectList(db.GameTypes, "GameTypeID", "GameTypeCode", game.GameTypeID);
                ViewBag.NextGameID = new SelectList(db.Games, "NextGameID", "USR", game.NextGameID);
                ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode", game.GameID);
                ViewBag.MemberSubscriptionType = new SelectList(db.MemberSubscriptionTypes, "MemberSubscriptionTypeID", "MemberSubscriptionTypeCode", game.MemberSubscriptionType);
                    
            }
           
            return View(game);  
        }

        private GameAdminViewModel GetGame()
        {
            if (Session["game"] == null)
            {
                Session["game"] = new GameAdminViewModel();
            }
            return (GameAdminViewModel)Session["game"];
        }

        private void RemoveCustomer()
        {
            Session.Remove("game");
        }


        [HttpPost]
        public ActionResult ProductInGame([Bind(Include = "productID,Quantity,PriceInGame,ReferencePrice,CurrencyID")]ProductInGame data, string prevBtn, string nextBtn)
        {
            GameAdminViewModel game = GetGame();
            if (prevBtn != null)
            {
                GameAdminViewModel g = new GameAdminViewModel();
                g.Game= game.Game;
              
                g.Game.DateInserted = DateTime.Now;
                g.Game.DateUpdated = DateTime.Now;
                g.Game.USR = "VaultLifeAdmin";
                return RedirectToAction("Create",g.Game);

            }
            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    game.ProductInGames = data;
                    game.ProductInGames.DateUpdated = DateTime.Now;
                    game.ProductInGames.USR = "VaultLifeAdmin";
                    Session["game"] = game; 
                    return RedirectToAction("ProductLocation");
                }
            }
            return View();
        }

        public ActionResult ProductInGame()
        {
            ViewBag.CurrencyID = new SelectList(db.Currencies, "CurrencyID", "CurrencyCode");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductSKUCode");
            return View();
        }

        public ActionResult ProductLocation()
        {
            ViewBag.ProductInGameID = new SelectList(db.ProductInGames, "ProductInGameID", "Currency");
            
            return View();
        }

         [HttpPost]
       public ActionResult ProductLocation(ProductLocation data, string prevBtn, string nextBtn)
        {
            GameAdminViewModel game = GetGame();
            if (prevBtn != null)
            {
                GameAdminViewModel g = new GameAdminViewModel();
                g.ProductLocations = game.ProductLocations;
                g.ProductLocations.DateInserted = DateTime.Now;
                g.ProductLocations.DateUpdated = DateTime.Now;
                g.ProductLocations.USR = "VaultLifeAdmin";
                return RedirectToAction("ProductInGames",g);
            }
            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    game.ProductLocations = data;
                    game.ProductLocations.DateInserted = DateTime.Now;
                    game.ProductLocations.DateUpdated = DateTime.Now;
                    game.ProductLocations.USR = "VaultLifeAdmin";
                    if (db.Games.Count(g => g.GameID == game.Game.GameID) > 0)
                    {
                        db.Games.Remove(game.Game);
                    }


                    db.Games.Add(game.Game);
                    db.SaveChanges();
                    game.Game.ProductInGames =new List<ProductInGame> ();
                    game.ProductInGames.GameID = game.Game.GameID;
                    game.Game.ProductInGames.Add(game.ProductInGames);
                    db.SaveChanges();
                    game.ProductLocations.ProductInGameID = game.ProductInGames.ProductInGameID;
                    db.ProductLocations.Add(game.ProductLocations);
                    db.SaveChanges();
                    
                    return RedirectToAction("Rules");
                }
            }
            return View();
        }

      
        // GET: GamesAdmin/Edit/5
        public ActionResult Edit(int? id)
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
                ViewBag.GameTypeID = new SelectList(db.GameTypes, "GameTypeID", "GameTypeCode", game.GameTypeID);
                ViewBag.GameID = new SelectList(db.NextGames, "NextGameID", "USR", game.GameID);
                ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode", game.GameID);
                ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode", game.GameID);
                ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode", game.GameID);
                ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode", game.GameID);
                ViewBag.MemberSubscriptionType = new SelectList(db.MemberSubscriptionTypes, "MemberSubscriptionTypeID", "MemberSubscriptionTypeCode", game.MemberSubscriptionType);
            return View(game);
        }

        // POST: GamesAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameID,GameCode,GameTypeID,GameName,GameDescription,DateInserted,DateUpdated,USR,GameCreationStatus,GameState,NumberOfWinners,NextGameID,MemberSubscriptionType,Global")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
                ViewBag.GameTypeID = new SelectList(db.GameTypes, "GameTypeID", "GameTypeCode", game.GameTypeID);
                ViewBag.GameID = new SelectList(db.NextGames, "NextGameID", "USR", game.GameID);
                ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode", game.GameID);
                ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode", game.GameID);
                ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode", game.GameID);
                ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode", game.GameID);
                ViewBag.MemberSubscriptionType = new SelectList(db.MemberSubscriptionTypes, "MemberSubscriptionTypeID", "MemberSubscriptionTypeCode", game.MemberSubscriptionType);
            return View(game);
        }

        // GET: GamesAdmin/Delete/5
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

        // POST: GamesAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
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
