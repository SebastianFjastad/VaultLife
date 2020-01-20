using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Script.Serialization;
using Vaultlife.Managers;
using Vaultlife.Models;
using Vaultlife.Models.Mobile;
using Vaultlife.Service;
using Vaultlife.Dao;


namespace Vaultlife.Controllers.Api
{
    public class GameSessionController : ApiController
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();
        public TrackingTransactionManager ttm = new TrackingTransactionManager();
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager;
        [HttpGet]
        [ActionName("TestJSON")]
        [AllowAnonymous]
        public string TestJSON()
        {
            /*PaymentTransactionManager paymentTransactionManager = new PaymentTransactionManager();
            paymentTransactionManager.MockPurchaseRequest(); */

             
            System.Web.Mvc.JsonResult json = new System.Web.Mvc.JsonResult
            {
                Data = "This is a JSON test response."
            };

            string son = new JavaScriptSerializer().Serialize(json.Data);

            return son;

        }

        // Returns seconds to game start, 0 if 'in progress', -1 if > 5 minutes to start.
        [HttpPost]
        [ActionName("GetGameStart")]
        [AllowAnonymous]
        public Int32 GetGameStart(FormDataCollection formData)
            {
                if (formData != null)
                {
                    string gameIDString = formData.Get("GameID");
                    int GameID = Convert.ToInt32(gameIDString);
                    if (GameID > 0)
                    {
                        DateTime gameStartDateTime = new DateTime();
                        var gameRules = from gr in db.GameRules where gr.GameID == GameID && gr.GameRuleCode == "StartGame" select gr;
                        foreach (var gameRule in gameRules)
                        {
                            gameStartDateTime = gameRule.ExcecuteTime;
                        }

                        double secondsToGameStartDouble = gameStartDateTime.Subtract(DateTime.Now).TotalSeconds;
                        if (secondsToGameStartDouble > 0)
                        {
                            Int32 secondsToGameStart = Convert.ToInt32(secondsToGameStartDouble);
                            if (secondsToGameStart <= 86400)
                            {
                                return (secondsToGameStart < 0) ? 0 : secondsToGameStart;
                            }
                        }
                    }
                }
                return -1;
        }

        // Expects OffSet - This is the time difference between the button going active and the button being clicked. precision is important here.
/*        [HttpPost]
        [ActionName("PersistUserInteraction")]
        [AllowAnonymous]
        public bool PersistUserInteraction(FormDataCollection formData)
        {
            if (formData != null)
            {
                string gameIDString = formData.Get("GameID");
                string memberIDString = formData.Get("MemberID");
                string offsetString = formData.Get("OffSet");
                int GameID = Convert.ToInt32(gameIDString);
                int MemberID = Convert.ToInt32(memberIDString);
                double OffSet = Convert.ToDouble(offsetString);
                if (GameID > 0 && MemberID > 0 && OffSet > 0)
                {
                    int clickInterval = (int)Math.Truncate(OffSet * 1000); // returns milliseconds

                    int memberInGameID = 0;

                    //Commented this check out for demo 
                    var memberInGames = from m in db.MemberInGames where m.MemberID == MemberID && m.GameID == GameID select m;
                    foreach (var memberInGame in memberInGames)
                    {
                        memberInGameID = memberInGame.MemberInGameID;
                    }


                    var productInGames = from p in db.ProductInGames where p.GameID == GameID select p;
                    foreach (var productInGame in productInGames)
                    {
                        ProductPlayed productPlayed = new ProductPlayed();
                        productPlayed.ProductInGameID = productInGame.ProductInGameID;
                        productPlayed.MemberInGameID = memberInGameID;
                        productPlayed.ClickInterval = clickInterval;
                        productPlayed.DateInserted = DateTime.Now;
                        db.ProductPlayeds.Add(productPlayed);
                    }

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
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }
*/
        [HttpPost]
        [ActionName("IsGameComplete")]
        [AllowAnonymous]
        public bool IsGameComplete(FormDataCollection formData)
        {
            if (formData != null)
            {
                string gameIDString = formData.Get("GameID");
                int GameID = Convert.ToInt32(gameIDString);
                if (GameID > 0)
                {
                    Game game = db.Games.Find(GameID);
                    if (game == null)
                    {
                        return true;
                    }
                    if (game.GameState == "COMPLETE")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        [HttpPost]
        [ActionName("IsGameActive")]
        [AllowAnonymous]
        public bool IsGameActive(FormDataCollection formData)
        {
            if (formData != null)
            {
                string gameIDString = formData.Get("GameID");
                int GameID = Convert.ToInt32(gameIDString);
                if (GameID > 0)
                {
                    Game game = db.Games.Find(GameID);
                    if (game == null)
                    {
                        return true;
                    }
                    if (game.GameState == "ACTIVE")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        [HttpPost]
        [ActionName("GetItemsInGamesByMember")]
        [AllowAnonymous]
          public string GetItemsInGamesByMember(FormDataCollection formData)
        {
            if (formData != null)
            {
                string memberIDString = formData.Get("MemberID");
                string returnLimitString = formData.Get("ReturnLimit");
                int MemberID = Convert.ToInt32(memberIDString);
                int ReturnLimit = Convert.ToInt32(returnLimitString);
                if (MemberID > 0 && ReturnLimit > 0)
                {
                    IQueryable<MemberInGame> memberInGames = db.MemberInGames.Include("Game.ProductInGames.Product.Imagedetails").Include("Game.GameType").Include("Game.GameRules").Where(x => x.MemberID == MemberID).Take(ReturnLimit);

                    List<MemberInGameItem> memberInGameItems = new List<MemberInGameItem>();

                    foreach (MemberInGame memberInGame in memberInGames)
                    {
                        MemberInGameItem memberInGameItem = new MemberInGameItem();
                        // This should happen in the constructor of MemberInGameItem.
                        memberInGameItem.GameDateTime = DateTime.MinValue;
                        memberInGameItem.DateTimeInserted = DateTime.MinValue;
                        memberInGameItem.DateTimeUpdated = DateTime.MinValue;

                        Game game = memberInGame.Game;
                        // Set Game properties
                        memberInGameItem.GameID = game.GameID;
                        memberInGameItem.GameCode = game.GameCode;
                        memberInGameItem.GameDescription = game.GameDescription;
                        memberInGameItem.GameName = game.GameName;
                        memberInGameItem.GameTypeName = game.GameType.GameTypeName;
                        //memberInGameItem.GameDateTime = ; comes from game rules where state = ?
                        //memberInGameItem.GamePrice = ;
                        //memberInGameItem.GameStatus = ;
                        //memberInGameItem.GameStatusColor = ;

                        // set product properties - this needs to be a collection in the MemberInGameItem class, it's not a 1:1 relationship, 1 game can have * products.
                        // I'm only taking the first one at this point.
                        foreach (ProductInGame productInGame in game.ProductInGames)
                        {
                            Product product = productInGame.Product;
                            memberInGameItem.ProductID = product.ProductID;
                            memberInGameItem.ProductName = product.ProductName;
                            memberInGameItem.ProductDescription = product.ProductDescription;
                            memberInGameItem.ProductPrice = (product.ProductPrice != null) ? (decimal)product.ProductPrice : 0;
                            memberInGameItem.OwnerID = product.OwnerID;

                            // loop through product categories.... because one product can live in multiple categories!!!
                            // I'm only taking the first one at this point.
                            foreach (ProductInCategory productInCategory in product.ProductInCategories)
                            {
                                ProductCategory productCategory = productInCategory.ProductCategory;
                                memberInGameItem.ProductCategory = productCategory.ProductCategoryName;
                                break;
                            }

                            //Quick hack to get the 1st image 
                            Imagedetail image = db.Imagedetails.Where(x => x.ProductId == product.ProductID).First();

                            memberInGameItem.ProductImage = image.ImageID.ToString();

                            break;

                        }

                        memberInGameItems.Add(memberInGameItem);
                    }

                    //  string returnJSONResult = "";
                    /*using (MemoryStream memoryStream = new MemoryStream())
                    {
                        DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(MemberInGameItem));
                        dataContractJsonSerializer.WriteObject(memoryStream, memberInGameItems);

                        memoryStream.Position = 0;
                        var sr = new StreamReader(memoryStream);
                        returnJSONResult = sr.ReadToEnd();
                    }
                    return returnJSONResult; */
                    System.Web.Mvc.JsonResult json = new System.Web.Mvc.JsonResult
                    {
                        Data = memberInGameItems
                    };

                    string son = new JavaScriptSerializer().Serialize(json.Data);

                    return son;
                }
            }
            return "";
            
        }

        [HttpPost]
        [ActionName("InitiateTimer")]
        [AllowAnonymous]
        public string InitiateTimer(FormDataCollection formData)
        {
            if (formData != null)
            {
                string gameIDString = formData.Get("GameID");
                string memberIDString = formData.Get("MemberID");
                int GameID = Convert.ToInt32(gameIDString);
                int MemberID = Convert.ToInt32(memberIDString);

                if (GameID > 0 && MemberID > 0)
                {
                    var res1 = ttm.InitiatePaymentTrackingTransaction(GameID, MemberID);
                    return this.ToJSONString(res1.ToString());
                }
            }
            return "-1";
        }

        [HttpPost]
        [ActionName("PausePaymentTrackingTransaction")]
        [AllowAnonymous]
        public string PausePaymentTrackingTransaction(FormDataCollection formData)
        {
            if (formData != null)
            {
                string gameIDString = formData.Get("GameID");
                string memberIDString = formData.Get("MemberID");
                int GameID = Convert.ToInt32(gameIDString);
                int MemberID = Convert.ToInt32(memberIDString);

                if (GameID > 0 && MemberID > 0)
                {
                    return this.ToJSONString(ttm.PausePaymentTrackingTransaction(GameID, MemberID).ToString());
                }
            }
            return "false";
        }

        [HttpPost]
        [ActionName("ResumePaymentTrackingTransaction")]
        [AllowAnonymous]
        public string ResumePaymentTrackingTransaction(FormDataCollection formData)
        {
            string resumeAction = "-1";
            if (formData != null)
            {
                string gameIDString = formData.Get("GameID");
                string memberIDString = formData.Get("MemberID");
                int GameID = Convert.ToInt32(gameIDString);
                int MemberID = Convert.ToInt32(memberIDString);

                if (GameID > 0 && MemberID > 0)
                {

                    if (ttm.ResumePaymentTrackingTransaction(GameID, MemberID))
                    {
                        resumeAction = ttm.GetTimeRemaining(GameID, MemberID).ToString();
                    }
                }
            }
            return this.ToJSONString(resumeAction);
        }
        // mobile service methods
        [HttpPost]
        [ActionName("ComingSoon")]
        [AllowAnonymous]
        public PaginatedItems ComingSoon(FormDataCollection formData)
        {
            AccountService service = new AccountService(db, UserManager);
            Member member = null;
            int MemberId = 0;
            int GameID = 0;
            int PageNo = 1;
            int _PAGE_LIMIT = 50;
            if (this.User.Identity != null)
            {
                member = service.findMember(this.User.Identity.Name);
            }
            if (member != null)
            {
                MemberId = member.MemberID;
            }
                 
            if (formData != null)
            {
                string pageNumberString = formData.Get("Page");
                if (pageNumberString != null)
                {
                    PageNo = Convert.ToInt32(pageNumberString);
                }
                string gameIDString = formData.Get("GameID");
                if (gameIDString != null)
                {
                    GameID = Convert.ToInt32(gameIDString);
                }

            }

            GameService gameService = new GameService(db);
            List<Game> ComingSoonList = gameService.FindComingSoonGames(MemberId).ToList();
            
            if (ComingSoonList.Count() == 0)
            {
                ComingSoonList = gameService.findGlobalGames(MemberId).ToList();
            }

            if (GameID > 0)
            {
                PageNo = 1;
                ComingSoonList = new List<Game>();
                // This gets ANY game, not just a coming soon game as per requirement change - 2014-11-27
                Game foundGame = db.Games.Find(GameID);
                if (foundGame != null)
                {
                    ComingSoonList.Add(foundGame);
                }
            }
            // build models/mobile/Items collection as per requirement.
            PaginatedItems paginatedItems = new PaginatedItems();
            // Add Banner Adverts
            paginatedItems.BannerAdverts = new List<BannerAdvert>();


            // Add items / coming soon / global games
            paginatedItems.Items = new List<Item>();
            int pageCounter = 0;
            if (ComingSoonList.Count() > 0)
            {
                if (ComingSoonList.Count() >= ((_PAGE_LIMIT*PageNo)-_PAGE_LIMIT))
                {

                    foreach (Game ComingSoongame in ComingSoonList)
                    {
                        pageCounter++;
                        int currentPage = Math.Abs((_PAGE_LIMIT + pageCounter) / _PAGE_LIMIT);
                        if (currentPage == PageNo)
                        {
                            Item item = new Item();
                            item.GameCode = ComingSoongame.GameCode;
                            ComingSoongame.Global = (ComingSoongame.Global == null) ? false : (bool)ComingSoongame.Global;
                            {
                                item.GameCountry = (((bool)ComingSoongame.Global) == true) ? "Global" : "South Africa";
                            }

                            item.GameCurrency = ComingSoongame.ProductInGames.FirstOrDefault().Currency.CurrencyCode;
                            item.GameDescription = ComingSoongame.GameDescription;
                            item.GameExpiryDateTime = ComingSoongame.GameRules.Where(x => x.GameRuleCode.ToUpper() == "RESOLVEACTUALWINNERS").FirstOrDefault().ExcecuteTime.ToString();
                            item.GameUnlockDateTime = ComingSoongame.GameRules.Where(x => x.GameRuleCode.ToUpper() == "STARTGAME").FirstOrDefault().ExcecuteTime.AddMinutes(-5).ToString();
                            item.GamePrepareDateTime = ComingSoongame.GameRules.Where(x => x.GameRuleCode.ToUpper() == "PREPAREGAMERULE").FirstOrDefault().ExcecuteTime.ToString();
                            item.GameResolveWinnersDateTime = ComingSoongame.GameRules.Where(x => x.GameRuleCode.ToUpper() == "RESOLVEACTUALWINNERS").FirstOrDefault().ExcecuteTime.ToString();
                            item.GameID = ComingSoongame.GameID.ToString();
                            item.GameName = ComingSoongame.GameName;
                            item.GamePrice = ComingSoongame.ProductInGames.FirstOrDefault().PriceInGame.ToString();
                            item.GameStatus = ComingSoongame.MemberSubscriptionType1.MemberSubscriptionTypeDescription;
                            switch (item.GameStatus.ToLower().Trim())
                            {
                                case "bronze":
                                    item.GameStatusColor = "#5b4839";
                                    break;
                                case "silver":
                                    item.GameStatusColor = "#505054";
                                    break;
                                case "gold":
                                    item.GameStatusColor = "#AC882F";
                                    break;
                                case "platinum":
                                    item.GameStatusColor = "#424A5B";
                                    break;
                            }
                            item.GameType = ComingSoongame.GameType.GameTypeName;
                            item.LastUpdated = ComingSoongame.DateUpdated.ToString();
                            // Requirement - 2014-11-27 - Add <FiveMinDeal> Boolean tag if has a five min deal, added NextGame??? properties for futureproofing.
                            item.FiveMinDeal = "0";
                            if (ComingSoongame.NextGameID != null)
                            {
                                Game nextGame = db.Games.Find(ComingSoongame.NextGameID);
                                item.FiveMinDeal = (nextGame.GameType.GameTypeName.ToLower().Contains("fiveminute")) ? "1" : "0";
                                item.NextGameID = nextGame.GameID.ToString();
                                item.NextGameType = nextGame.GameType.GameTypeName;
                                item.NextGameTypeID = nextGame.GameType.GameTypeID.ToString();
                            }

                            foreach (ProductInGame productInGame in ComingSoongame.ProductInGames)
                            {
                                Product product = productInGame.Product;
                                item.ProductID = product.ProductID.ToString();
                                item.ProductName = product.ProductName;
                                item.ProductDescription = product.ProductDescription;
                                item.ProductPrice = ((productInGame.ReferencePrice != null) ? (decimal)productInGame.ReferencePrice : 0).ToString();
                                item.ProductTerms = (product.terms != null) ? product.terms : "";
                                item.ProductWebsite = "";


                                // loop through product categories.... because one product can live in multiple categories!!!
                                // I'm only taking the first one at this point.
                                foreach (ProductInCategory productInCategory in product.ProductInCategories)
                                {
                                    ProductCategory productCategory = productInCategory.ProductCategory;
                                    item.ProductCategory = productCategory.ProductCategoryName;
                                    break;
                                }

                                item.ProductImages = new List<ProductImage>();
                                foreach (Imagedetail image in product.Imagedetails)
                                {
                                    ProductImage productImage = new ProductImage();
                                    productImage.ImageID = image.ImageID.ToString();
                                    item.ProductImages.Add(productImage);
                                }
                            }
                            item.TerritoryFlag = "/Content/Images/Flags/South-Africa.png";
                            if (item.GameCountry.ToLower() == "global")
                            {
                                item.TerritoryFlag = "/Content/Images/Flags/world.png";
                            }
                            item.TerritoryName = "";
                            paginatedItems.Items.Add(item);
                        }
                    }
                }
            }
            return paginatedItems;

        }

        [HttpPost]
        [ActionName("MemberGames")]
        [AllowAnonymous]
        public List<dynamic> MemberGames(int MemberID)
        {
            List<dynamic> dynamicResult = new List<dynamic>();

            if (MemberID > 0)
            {
                GameService gameService = new GameService(db);
                List<Game> ComingSoonList = gameService.FindComingSoonGames(MemberID).ToList();
                dynamicResult = ComingSoonList.Select(x => new { GameID = x.GameID }).ToList<dynamic>();
            }
            return dynamicResult;
        }

        private string ToJSONString(string ConvertStringToJSON)
        {
            string ConvertedToJSONString = "";
            System.Web.Mvc.JsonResult json = new System.Web.Mvc.JsonResult
            {
                Data = ConvertStringToJSON
            };

            ConvertedToJSONString = new JavaScriptSerializer().Serialize(json.Data);

            return ConvertedToJSONString;
        }
    }
}