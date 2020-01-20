using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vaultlife.Models;
using Quartz;
using Quartz.Impl;
using System.Data.SqlClient;
using System.Globalization;
using Vaultlife.Models.Games;
using Microsoft.AspNet.Identity;
using Vaultlife.ViewModels;
using FluentValidation.Results;
using System.Text;
using Vaultlife.Helpers;
using System.Web.Script.Serialization;
using PaymentService;
using Vaultlife.Managers;
using Vaultlife.Service;

namespace Vaultlife.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();
        private TrackingTransactionManager trackingTransactionManager = new TrackingTransactionManager();
        private ProductInGame pig;


        public ActionResult PlayGame(int? GameID)
        {
            if (GameID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(GameID);

            if (game == null)
            {
                return HttpNotFound();
            }
            if (game.GameState.ToUpper() == "COMPLETED")
            {
                return RedirectPermanent("/");
            }

            AspNetUser user = db.AspNetUsers.Where(x => x.UserName == User.Identity.Name).First();
            Member member = db.Members.Where(m => m.ASPUserId == user.Id).First();
            ViewBag.MemberID = member.MemberID;
            var cnt = db.MemberInGames.Count(x => x.MemberID == member.MemberID && x.GameID == GameID);
            
            if (cnt == 0 && game.Global != true)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);  // Throw unauth error here
            }

            ViewBag.GameState = game.GameState;
            ViewBag.NextGameID = game.NextGameID;
            ViewBag.GameID = game.GameID;
            var images = db.ProductInfoes.Where(g => g.GameID == GameID);
            //db.ProductInfoes.Where(g => g.GameID == GameID);
            var image = images.FirstOrDefault();

            ProductInGame pig = db.ProductInGames.Where(pi => pi.GameID == GameID).First();
            Product product = pig.Product;
            ViewBag.Description = product.ProductDescription;
            ViewBag.ProductName = product.ProductName;
            ViewBag.PriceInGame = pig.PriceInGame;
            ViewBag.OldPrice = pig.ReferencePrice;
            ViewBag.Currency = pig.Currency.CurrencySymbol;
            // ViewBag.PriceInGame = ViewBag.PriceInGame.Format(new CultureInfo(CultureInfo.CurrentCulture.Name), "{C}");
            ViewBag.MainImage = game.ProductInGames.First().Product.Imagedetails.First().ImageID != null?  //TODO sort out main image stuff
              ViewBag.MainImage = game.ProductInGames.First().Product.Imagedetails.First().ImageID : 0;  //TODO sort out main image stuff

            return View(images.ToList());
        }

        public ActionResult Deal(int? GameID)
        {
            if (GameID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(GameID);

            if (game == null)
            {
                return HttpNotFound();
            }
            if (game.GameState.ToUpper() == "COMPLETED")
            {
                return RedirectPermanent("/");
            }

            AspNetUser user = db.AspNetUsers.Where(x => x.UserName == User.Identity.Name).First();
            Member member = db.Members.Where(m => m.ASPUserId == user.Id).First();
            ViewBag.MemberID = member.MemberID;
            var cnt = db.MemberInGames.Count(x => x.MemberID == member.MemberID && x.GameID == GameID);

            if (cnt == 0 && game.Global != true)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);  // Throw unauth error here
            }

            ViewBag.GameState = game.GameState;
            ViewBag.NextGameID = game.NextGameID;


            var images = db.ProductInfoes.Where(g => g.GameID == GameID);
            var image = images.FirstOrDefault();

            ProductInGame pig = db.ProductInGames.Where(pi => pi.GameID == GameID).First();
            Product product = pig.Product;
            ViewBag.Description = product.ProductDescription;
            ViewBag.ProductName = product.ProductName;
            ViewBag.PriceInGame = pig.PriceInGame;
            ViewBag.OldPrice = pig.ReferencePrice;
            ViewBag.Currency = pig.Currency.CurrencySymbol;
            // ViewBag.PriceInGame = ViewBag.PriceInGame.Format(new CultureInfo(CultureInfo.CurrentCulture.Name), "{C}");
            ViewBag.MainImage = game.ProductInGames.First().Product.Imagedetails.First().ImageID != null ?  //TODO sort out main image stuff
              ViewBag.MainImage = game.ProductInGames.First().Product.Imagedetails.First().ImageID : 0;  //TODO sort out main image stuff

            return View(images.ToList());
        }

       
        public ActionResult BuyGame(int GameID)
        {

            Game game = db.Games.Find(GameID);

            if (game == null)
            {
                return HttpNotFound();
            }

            AspNetUser user = db.AspNetUsers.Where(x => x.UserName == User.Identity.Name).First();
            Member member = db.Members.Where(m => m.ASPUserId == user.Id).First();
            ViewBag.MemberID = member.MemberID;
            var cnt = db.MemberInGames.Count(x => x.MemberID == member.MemberID && x.GameID == GameID);

            if (cnt == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);  // Throw unauth error here
            }

            var images = db.ProductInfoes.Where(g => g.GameID == GameID);
            var image = images.FirstOrDefault();
            ViewBag.Description = image.ProductDescription;
            ViewBag.ProductName = image.ProductName;
            ViewBag.PriceInGame = image.PriceInGame.ToString();
            //ViewBag.PriceInGame = ViewBag.PriceInGame.Format(new CultureInfo(CultureInfo.CurrentCulture.Name), "{C}");
            ViewBag.MainImage = game.ProductInGames.First().Product.Imagedetails.First().ImageID;   //TODO fix main image stuff
            ViewBag.images = images.ToList();
            //Stop people from cheating

            ViewBag.RemainingTime = trackingTransactionManager.InitiatePaymentTrackingTransaction(GameID, member.MemberID);
            return View();
        }




        public ActionResult AccessDenied(int? CurrentGame)
        {
            if (CurrentGame == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(CurrentGame);

            if (game == null)
            {
                return HttpNotFound();
            }

            GameService gameService = new GameService(db);
            AspNetUser user = db.AspNetUsers.Where(x => x.UserName == User.Identity.Name).First();
            Member member = db.Members.Where(m => m.ASPUserId == user.Id).First();
            ViewBag.MemberID = member.MemberID;
            var cnt = db.MemberInGames.Count(x => x.MemberID == member.MemberID && x.GameID == CurrentGame);

            if (cnt == 0 && game.Global != true)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);  // Throw unauth error here
            }

            ViewBag.GameState = game.GameState;
            ViewBag.NextGameID = game.NextGameID;


            var images = db.ProductInfoes.Where(g => g.GameID == CurrentGame);
            var image = images.FirstOrDefault();

            ProductInGame pig = db.ProductInGames.Where(pi => pi.GameID == CurrentGame).First();
            Product product = pig.Product;
            ViewBag.Description = product.ProductDescription;
            ViewBag.ProductName = product.ProductName;
            ViewBag.PriceInGame = pig.PriceInGame;
            ViewBag.OldPrice = pig.ReferencePrice;
            ViewBag.Currency = pig.Currency.CurrencySymbol;
           // ViewBag.CounterValue = gameService.getMaxTransactionTime(game.GameID);
            // ViewBag.PriceInGame = ViewBag.PriceInGame.Format(new CultureInfo(CultureInfo.CurrentCulture.Name), "{C}");
            ViewBag.MainImage = game.ProductInGames.First().Product.Imagedetails.First().ImageID != null ?  //TODO sort out main image stuff
              ViewBag.MainImage = game.ProductInGames.First().Product.Imagedetails.First().ImageID : 0;  //TODO sort out main image stuff

            return View(images.ToList());
        }

        public ActionResult Sold()
        {
            return View();
        }

        public ActionResult SuccessfulPayment()
        {
            return View();
        }

/*        [HttpGet]
        public ActionResult PaymentFailed(int GameID)
        {
            GameService gameService = new GameService(db);
            gameService.paymentFailed(GameID);
            return View();
        }
  */      

        [HttpGet]
        public ActionResult PaymentFailed()
        {
            return View();
        }
       public ActionResult NextGame(int NextGameID)
        {
           if (NextGameID > 0)
           {
               // determine next game type
               var nextGameTypeCode = db.Games.Find(NextGameID).GameType.GameTypeCode;
                ViewBag.GameID = NextGameID;
                switch (nextGameTypeCode.ToString())
                {
                    case "FFF":
                        // FFF game, return FFF game view
                        return View("PlayGame");
                    case "FMD":
                        // deal 5 minute game
                        return View("Deal");
                    default:
                        // return deal screen as default.
                        return View("Deal");
                }
           }
           return RedirectPermanent("/");
        }

        [HttpPost]
        public string Purchase(FormCollection form)
        {


            int GameID = Convert.ToInt32((form["GameID"]));
            AspNetUser user = db.AspNetUsers.Where(x => x.UserName == User.Identity.Name).First();
            Member member = db.Members.Where(m => m.ASPUserId == user.Id).First();


            MemberInGame mib = db.MemberInGames.Where(c => c.MemberID == member.MemberID && c.GameID == GameID).First();
            trackingTransactionManager.PausePaymentTrackingTransaction(GameID, member.MemberID);

            //Address billingAddress = db.Addresses.Where(a => a.MemberID == member.MemberID && a.AddressType.ToLower() == "billing").First();
            pig = mib.Game.ProductInGames.FirstOrDefault();

      


            /* Setcom Purchase */
            SetcomPaymentTransactionManager PayMan = new SetcomPaymentTransactionManager();
            PurchaseTransactionRequest purchaseTransactionRequest = new PurchaseTransactionRequest();

            purchaseTransactionRequest.CCNumber = form["PaymentsModel.CardNumber"]; //"4444444444444444";
            purchaseTransactionRequest.CCCVV = form["PaymentsModel.CVCNumber"];
            purchaseTransactionRequest.ExYear = (form["PaymentsModel.ExpiryDateY"].ToString().Trim().Length > 2) ? form["PaymentsModel.ExpiryDateY"] : "20" + form["PaymentsModel.ExpiryDateY"];
            purchaseTransactionRequest.ExMonth = form["PaymentsModel.ExpiryDateM"];
            purchaseTransactionRequest.CCName = form["PaymentsModel.NameOnCard"];
            purchaseTransactionRequest.MemberInGameID = mib.MemberInGameID;
            purchaseTransactionRequest.EmailAddress = mib.Member.EmailAddress;
            purchaseTransactionRequest.CC_Amount = pig.PriceInGame.ToString();
            purchaseTransactionRequest.ip_address = Request.ServerVariables["REMOTE_ADDR"];
            purchaseTransactionRequest.transactionDateTime = DateTime.Now;

            /* Additional Non-mandatory fields */
            purchaseTransactionRequest.bill_first_name = mib.Member.FirstName;
            purchaseTransactionRequest.bill_last_name = mib.Member.LastName;

            purchaseTransactionRequest.bill_street1 = "";
            purchaseTransactionRequest.bill_street2 = "";
            purchaseTransactionRequest.bill_city = "";
            purchaseTransactionRequest.bill_state = "";
            purchaseTransactionRequest.bill_country = mib.Member.Country.CountryName; ;
            purchaseTransactionRequest.bill_zip = "";
            purchaseTransactionRequest.bill_phone = "";
            purchaseTransactionRequest.bill_title = "";



            PurchaseTransactionResponse ptRes = PayMan.PerformPaymentTransaction(purchaseTransactionRequest);
            ptRes.outcome = ptRes.outcome.ToUpper();
            if (ptRes.outcome.ToUpper() != "APPROVED")
            // Setcom change ends here
            {

                trackingTransactionManager.ResumePaymentTrackingTransaction(GameID, member.MemberID);
                ptRes.timeRemaining = trackingTransactionManager.GetTimeRemaining(GameID, member.MemberID).ToString();

            }
            else
            {

                trackingTransactionManager.CompletePaymentTrackingTransaction(GameID, member.MemberID);
                //Update paymentIndicator
                mib.PaymentIndicator = true;
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


                // get winner address
                Address deliveryAddress = db.Addresses.FirstOrDefault(x => x.MemberID == member.MemberID && x.AddressType.ToLower() == "postal");

                // get quantity won - divide quantity from pig by number of winners ??? really??? ok then.....
                Game qtyGame = db.Games.Find(GameID);

                int winQuantity = 1; //pig.Quantity / qtyGame.NumberOfWinners;
                // send winner email
                this.sendWinnermail(member.FirstName + ' ' + member.LastName, member.EmailAddress, deliveryAddress, winQuantity);

            }

            JsonResult json = new JsonResult
            {
                Data = ptRes
            };

            string son = new JavaScriptSerializer().Serialize(json.Data);

            return son;

        }

        private string GetIPAddress()
        {
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            return ipAddress.ToString();
        }

      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public JsonResult getStates(FormCollection form)
        {
//TODO what's this doing in here?
            string Countryid = form["CountryID"].ToString().Trim();
            //string[] CountiD = CID.Split('=');

            int CID = Convert.ToInt32(Countryid);
            var states = from x in db.CountryStates.AsEnumerable()
                         where x.CountryID.Equals(CID)
                         select new { StateID = x.StateID, StateName = x.StateName };

            return Json(states.ToArray());
        }

        private void sendWinnermail(String recipientName, String email, Address deliveryAddress, int winQuantity)
        {
            string deliveryAddressFormatted = "No delivery address supplied.";

            if (deliveryAddress != null)
            {
                deliveryAddressFormatted = (deliveryAddress.AddressLine1 !=null && deliveryAddress.AddressLine1.Trim() != "") ? deliveryAddress.AddressLine1.Trim() + "<br/>" : "";
                deliveryAddressFormatted = deliveryAddressFormatted + ((deliveryAddress.AddressLine2 !=null && deliveryAddress.AddressLine2.Trim() != "") ? deliveryAddress.AddressLine2.Trim() + "<br/>" : "");
                deliveryAddressFormatted = deliveryAddressFormatted + ((deliveryAddress.AddressLine3 != null && deliveryAddress.AddressLine3.Trim()!="") ? deliveryAddress.AddressLine3.Trim() + "<br/>" : "");
                deliveryAddressFormatted = deliveryAddressFormatted + ((deliveryAddress.CityName !=null && deliveryAddress.CityName.Trim() != "") ? deliveryAddress.CityName.Trim() + "<br/>" : "");
                deliveryAddressFormatted = deliveryAddressFormatted + ((deliveryAddress.StateOrProvince!=null && deliveryAddress.StateOrProvince.Trim() != "") ? deliveryAddress.StateOrProvince.Trim() + "<br/>" : "");
                deliveryAddressFormatted = deliveryAddressFormatted + ((deliveryAddress.ZipOrPostalCode!= null && deliveryAddress.ZipOrPostalCode.Trim() != "") ? deliveryAddress.ZipOrPostalCode.Trim() + "<br/>" : "");
                deliveryAddressFormatted = deliveryAddressFormatted + ((deliveryAddress.Country!=null && deliveryAddress.Country.Trim() != "") ? deliveryAddress.Country.Trim() + "<br/>" : "");
            }
            String orderDetail = "<br/>Item Name: " + pig.Product.ProductName + "<br/>Quantity: " + winQuantity;
            EmailManager emailManager = new EmailManager();
            emailManager.NewEmail();
            emailManager.RecipientEmailAddress = email;
            emailManager.RecipientName = recipientName;
            emailManager.EmailSubject = " VAULTLife.com Purchase details";
            //emailManager.EmailBodyText = "<!DOCTYPE html><html xmlns='http://www.w3.org/1999/xhtml'><head><title></title><link rel='stylesheet' href='http://www.vaultlife.com/Content/images/email/email-styles.css' /></head><body><link rel='stylesheet' href='http://www.vaultlife.com/Content/images/email/email-styles.css' /><div class='header' style='background-color: black;'><img src='https://www.vaultlife.com/Content/images/logo.png' /></div><div class='content'><h1>CONGRATULATIONS</h1><p>Well done! Another item ticked off your bucket list! You successfully outwitted all the other members and have secured your dream item for yourself! Why not share your victory on Facebook so the world can see how serious you are about living the life you always dreamed of!</p><p>VAULTLife.com will deliver your item within the next 5 working days. If you have any inquiries,email us on orders@vaultlife.com</p><h2>ORDER DETAILS</h2><table cellspacing='0'><tr><td><strong>Name and Surname</strong></td><td>" + recipientName + "</td></tr><tr><td>Delivery Address</td><td>" + deliveryAddress + "</td></tr><tr><td>Item Name</td><td>" + pig.Product.ProductName + "</td></tr><tr><td>Quantity</td><td>1</td></tr><tr><td>Description</td><td>" + pig.Product.ProductDescription + "</td></tr><tr><td>Terms and Conditions</td><td>" + pig.Product.terms + "</td></tr></table></div><div class='footer'><table class='signature' cellspacing='0'><tr><td class='details'><img src='http://www.vaultlife.com/Content/images/email/vl-team-sig_01.jpg' alt='Vaultlife.com' /><div class='content'><h2>VAULTLife TEAM</h2><div><strong>Email:</strong> &nbsp; <a href='mailto:info@vaultlife.com'>info@vaultlife.com</a></div><div><strong>Tel:</strong> &nbsp; <span>+27 86 123 6334</span></div><div><strong>Fax:</strong> &nbsp; <span>+27 86 123 6334</span></div><div><span class='small'>The Embassy, 50 Mulbarton Rd, Beverly, Johannesburg, 1296</span></div><div><a href='http://www.vaultlife.com'><strong>www.vaultlife.com</strong></a></div></div></td><td class='promo'><img src='http://www.vaultlife.com/Content/images/email/vl-team-sig_02.jpg' alt='Vaultlife.com' /></td></tr></table></div></body></html>";
            emailManager.EmailBodyText = "<!DOCTYPE html><html xmlns='http://www.w3.org/1999/xhtml'><head><title></title></head><body style='margin: 0px; padding: 0px;'><div class='header' style='background-color: black; padding: 15px 20px;'><img src='http://www.vaultlife.com/Content/images/logo.png' alt='Vaultlife.com' style='width: 127px; height: 39px;' /></div><div class='content' style='padding: 15px 30px; font-family: Segoe UI, Calibri, Arial, Helvetica, sans-serif; max-width: 800px; width: 100%;'><h1>CONGRATULATIONS</h1><p>Well done! Another item ticked off your bucket list! You successfully outwitted all the other members and have secured your dream item for yourself! Why not share your victory on Facebook so the world can see how serious you are about living the life you always dreamed of!</p><p>VAULTLife.com will deliver your item within the next 5 working days. If you have any enquiries, email us on orders@vaultlife.com</p><h2>ORDER DETAILS</h2><table cellspacing='0' cellpadding='10' style='border: 1px solid #aaa; border-bottom: 0px; font-size: 13px;'><tr><td style ='border-bottom: 1px solid #aaa;'><strong>Name and Surname</strong></td><td style='border-bottom: 1px solid #aaa;'>" + recipientName + "</td></tr><tr><td style='border-bottom: 1px solid #aaa;'><strong>Delivery Address</strong></td><td style='border-bottom: 1px solid #aaa;'>" + deliveryAddress + "</td></tr><tr><td style='border-bottom: 1px solid #aaa;'><strong>Item Name</strong></td><td style='border-bottom: 1px solid #aaa;'>" + pig.Product.ProductName + "</td></tr><tr><td style='border-bottom: 1px solid #aaa;'><strong>Quantity</strong></td><td style='border-bottom: 1px solid #aaa;'>1</td></tr><tr><td style='border-bottom: 1px solid #aaa;'><strong>Description</strong></td><td style='border-bottom: 1px solid #aaa;'>" + pig.Product.ProductDescription + "</td></tr><tr><td style='border-bottom: 1px solid #aaa;'><strong>Terms and Conditions</strong></td><td style ='border-bottom: 1px solid #aaa;'>" + pig.Product.terms + "</td></tr></table></div><div class='footer' style='padding: 15px 30px; font-family: 'Segoe UI', Calibri, Arial, Helvetica, sans-serif;'><table class='signature' cellspacing='0' cellpadding='0'><tr><td class='details' style='width: 308px; vertical-align: top; background-color: #161616;' valign='top'><img src='http://www.vaultlife.com/Content/images/email/vl-team-sig_01.jpg' alt='Vaultlife.com' style='width: 308px; height: 79px;' /><div class='content' style='padding: 0px 15px; font-size: 11px; color: white;'><h2 style='margin: 0px;font-size: 20px;'>VAULTLife TEAM</h2><div><strong>Email:</strong> &nbsp; <a href='mailto:info@vaultlife.com' style='color: #eeeeee;'>info@vaultlife.com</a></div><div><strong>Tel:</strong> &nbsp; <span>+27 86 123 6334</span></div><div><strong>Fax:</strong> &nbsp; <span>+27 86 123 6334</span></div><div><span class='small'>The Embassy, 50 Mulbarton Rd, Beverly, Johannesburg, 1296</span></div><div><a href='http://www.vaultlife.com' style='color: #eeeeee;'><strong>www.vaultlife.com</strong></a></div></div></td><td class='promo' valign='top' style='vertical-align: top; background-color: #161616; width: 220px'><img src='http://www.vaultlife.com/Content/images/email/vl-team-sig_02.jpg' alt='Vaultlife.com' style='width: 228px; height: 220px;' /></td></tr></table></div></body></html>";
            emailManager.QueueEmail();
        }
    }
}
