using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Data.Entity;
using Vaultlife.Managers;
using Vaultlife.Models;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using Vaultlife.Service;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using Vaultlife.Dao;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Collections;
using PaymentService;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;

namespace Vaultlife.Services
{
    [ServiceContract(Namespace = "VaultLife.Services")]
    public interface IGameSessionService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/TestJSON", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string TestJSON();

        [OperationContract]
        //[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        [WebGet(UriTemplate = "/GetGameStart/?GameID={GameID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Int32 GetGameStart(int GameID);

        [OperationContract]
        //[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        [WebGet(UriTemplate = "/GetGameEnd/?GameID={GameID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Int32 GetGameEnd(int GameID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/PersistUserInteraction/", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool PersistUserInteraction(int GameID, int MemberID, int OffSet);

        [OperationContract]
        [WebGet(UriTemplate = "/IsGameComplete/?GameID={GameID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool IsGameComplete(int GameID);

        [OperationContract]
        [WebGet(UriTemplate = "/IsGameActive/?GameID={GameID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool IsGameActive(int GameID);

        [OperationContract]
        [WebGet(UriTemplate = "/IsUserWinner/?MemberID={MemberID}&GameID={GameID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        String IsUserWinner(int MemberID, int GameID);

        [OperationContract]
        [WebGet(UriTemplate = "/GetItemsInGamesByMember/?MemberID={MemberID}&MaxReturned={ReturnLimit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetItemsInGamesByMember(int MemberID, int ReturnLimit);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Purchase/" , BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string Purchase(string CCNumber, string CCCVV, string ExYear, string ExMonth, string CCName, string MemberInGameID, string EmailAddress, string CC_Amount, string ip_address, string transactionDateTime);

        [OperationContract]
        [WebGet(UriTemplate = "/GetGameUnlockTime/?GameID={GameID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetGameUnlockTime(int GameID);

        [OperationContract]
        [WebGet(UriTemplate = "/GetServerTime", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetServerTime();


    }


    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class GameSessionService : IGameSessionService
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();
        private TrackingTransactionManager trackingTransactionManager = new TrackingTransactionManager();
        private ProductInGame pig;

        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";


        public string TestJSON()
        {
            CashCloudPaymentTransactionManager paymentTransactionManager = new CashCloudPaymentTransactionManager();
            //paymentTransactionManager.MockCashCloudPurchaseRequest();

            return "This is a JSON test response.";
        }

        // Returns seconds to game start, 0 if 'in progress', -1 if > 5 minutes to start.
        public Int32 GetGameStart(int GameID)
        {
            DateTime gameStartDateTime = new DateTime();
            var gameRules = from gr in db.GameRules where gr.GameID == GameID && gr.GameRuleCode == "StartGame" select gr;
            foreach (var gameRule in gameRules)
            {
                gameStartDateTime = gameRule.ExcecuteTime;
            }

            double secondsToGameStartDouble = gameStartDateTime.Subtract(DateTime.Now).TotalSeconds;
            Int32 secondsToGameStart = Convert.ToInt32(secondsToGameStartDouble);
            if (secondsToGameStart > 300)
            {
                // if start time is >5 minutes, return -1 - not starting anytime soon 
                return -1;
            }
            else
            {
                return (secondsToGameStart < 0) ? 0 : secondsToGameStart;
            }
        }
        public Int32 GetGameEnd(int GameID)
        {
            DateTime gameEndtDateTime = new DateTime();
            var gameRules = from gr in db.GameRules where gr.GameID == GameID && gr.GameRuleCode.ToUpper() == "RESOLVEACTUALWINNERS" select gr;
            foreach (var gameRule in gameRules)
            {
                gameEndtDateTime = gameRule.ExcecuteTime;
            }

            double secondsToGameEndDouble = gameEndtDateTime.Subtract(DateTime.Now).TotalSeconds;
            Int32 secondsToGameStart = Convert.ToInt32(secondsToGameEndDouble);
            return (secondsToGameStart < 0) ? 0 : secondsToGameStart;
           }
        
        // Expects OffSet - This is the time difference between the button going active and the button being clicked. precision is important here.
        public bool PersistUserInteraction(int GameID, int MemberID, int OffSet)
        {
          //  int clickInterval = (int)Math.Truncate(OffSet * 1000); // returns milliseconds

            

            int memberInGameID = 0;
            if (db.ProductInWatchLists.Where(piw => piw.GameID == GameID).Count() > 0)
            {
                ProductInWatchList pinWatchlist = db.ProductInWatchLists.Where(piw => piw.GameID == GameID).First();
                db.ProductInWatchLists.Remove(pinWatchlist);
            }
            //Commented this check out for demo 
            var memberInGames = from m in db.MemberInGames where m.MemberID == MemberID && m.GameID == GameID select m;
            foreach (var memberInGame in memberInGames)
            {
                memberInGameID = memberInGame.MemberInGameID;
            }  
            if (memberInGameID == 0 || memberInGameID == null)
            {
                return false;
            }
           
            var productInGames = from p in db.ProductInGames where p.GameID == GameID select p;

            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("PersistUserInteraction", con))
                {

                    foreach (var productInGame in productInGames)
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@ProductInGameID", SqlDbType.VarChar).Value = productInGame.ProductInGameID;
                        cmd.Parameters.Add("@memberInGameID", SqlDbType.VarChar).Value = memberInGameID;
                        cmd.Parameters.Add("@Offset", SqlDbType.VarChar).Value = OffSet;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            /*try
            {
            foreach (var productInGame in productInGames)
            {
                ProductPlayed productPlayed = new ProductPlayed();
                productPlayed.ProductInGameID = productInGame.ProductInGameID;
                productPlayed.MemberInGameID = memberInGameID;
                productPlayed.ClickInterval = OffSet;
                productPlayed.DateInserted = DateTime.Now;
                db.ProductPlayeds.Add(productPlayed);
            }

            

                db.SaveChanges();
            }
            


        catch (OptimisticConcurrencyException ex)
        {
            // Resolve the concurrency conflict by refreshing the  
            // object context before re-saving changes. 
            var context = ((IObjectContextAdapter)db).ObjectContext;
            foreach (var entry in ex.StateEntries)
            {
                context.Refresh(RefreshMode.ClientWins, entry.Entity);
            }

            context.SaveChanges(); 
            

        }*/
          
            return true;
        }
       
        public bool IsGameComplete(int GameID)
        {
            Game game = db.Games.Find(GameID);
            if (game == null)
            {
                return false;
            }
            if (game.GameState.ToUpper() == "COMPLETED")
            {
                return true;
            }
            return false;
        }

        public bool IsGameActive(int GameID)
        {
            Game game = db.Games.Find(GameID);
            if (game == null)
            {
                return false;
            }
            if (game.GameState != null && game.GameState.ToLower() == "active")
            {
                return true;
            }
            return false;
        }

        public string GetItemsInGamesByMember(int MemberID, int ReturnLimit)
        {
            IQueryable<MemberInGame> memberInGames = db.MemberInGames.Include("Game.ProductInGames.Product.Imagedetails").Include("Game.GameType").Include("Game.GameRules").Where(x => x.MemberID == MemberID).Take(ReturnLimit);

            List<MemberInGameItem> memberInGameItems = new List<MemberInGameItem>();
            
           foreach(MemberInGame memberInGame in memberInGames)
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
                    Imagedetail image = product.Imagedetails.FirstOrDefault();
                        memberInGameItem.ProductImage = image.ImageID.ToString(); 
                    // create collection of image URLs per product

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
            JsonResult json = new JsonResult
            {
                Data = memberInGameItems
            };

            string son = new JavaScriptSerializer().Serialize(json.Data);

            return son;
          
            
        }

        private int callIsWinnerStoredProc(int GameID, int MemberID)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connString))
            {
                int winStatus = -1;
                con.Open();
                using (SqlCommand cmd = new SqlCommand("IsUserWinner", con))
                {
                       cmd.CommandType = CommandType.StoredProcedure;
                       cmd.Parameters.Add("@GameID", SqlDbType.Int).Value = GameID;
                       cmd.Parameters.Add("@MemberID", SqlDbType.Int).Value = MemberID;
                       winStatus = (int) cmd.ExecuteScalar();
                   
                }
                con.Close();
                return winStatus;
            }

        }
        public String IsUserWinner(int MemberID, int GameID)
        {

            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            Game game = db.Games.Where(g => g.GameID == GameID).First();
            if (game.GameState.ToUpper() == "COMPLETED")
            {
                dictionary.Add("Winner", -1);
                dictionary.Add("NextGame", 0);
            }
            else
            {
                int win = callIsWinnerStoredProc(GameID, MemberID);
                int nextGameId = db.Games.Find(GameID).NextGameID.HasValue ? db.Games.Find(GameID).NextGameID.Value : 0;
                dictionary.Add("Winner", win);
                dictionary.Add("NextGame", nextGameId);
            }
            JsonResult json = new JsonResult
            {
                Data = dictionary
            };

            string son = new JavaScriptSerializer().Serialize(json.Data);
            return son;
        }




        //public string Purchase(string CCNumber, string CCCVV, string ExYear, string ExMonth, string CCName, string MemberInGameID, string EmailAddress, string CC_Amount, string ip_address, string transactionDateTime)
        public string Purchase(string CCNumber, string CCCVV, string ExYear, string ExMonth, string CCName, string MemberInGameID, string EmailAddress, string CC_Amount, string ip_address, string transactionDateTime)
        {

            
            int memberInGameID = Convert.ToInt32(MemberInGameID);
            MemberInGame mib = db.MemberInGames.Where(c => c.MemberInGameID == memberInGameID).First();
            Member member = db.Members.Where(m => m.MemberID == mib.MemberID).First();
            trackingTransactionManager.PausePaymentTrackingTransaction(mib.GameID, mib.MemberID);

            //Address billingAddress = db.Addresses.Where(a => a.MemberID == member.MemberID && a.AddressType.ToLower() == "billing").First();
            pig = mib.Game.ProductInGames.FirstOrDefault();

      


            // Setcom Purchase
            SetcomPaymentTransactionManager PayMan = new SetcomPaymentTransactionManager();
            PurchaseTransactionRequest purchaseTransactionRequest = new PurchaseTransactionRequest();

            purchaseTransactionRequest.CCNumber = CCNumber; //"4444444444444444";
            purchaseTransactionRequest.CCCVV = CCCVV;
            purchaseTransactionRequest.ExYear = (ExYear.ToString().Trim().Length > 2) ? ExYear : "20" + ExYear;
            purchaseTransactionRequest.ExMonth = ExMonth;
            purchaseTransactionRequest.CCName = CCName;
            purchaseTransactionRequest.MemberInGameID = mib.MemberInGameID;
            purchaseTransactionRequest.EmailAddress = mib.Member.EmailAddress;
            purchaseTransactionRequest.CC_Amount = pig.PriceInGame.ToString();
            purchaseTransactionRequest.ip_address = ip_address;
            purchaseTransactionRequest.transactionDateTime = DateTime.Now;

            // Additional Non-mandatory fields
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

                trackingTransactionManager.ResumePaymentTrackingTransaction(mib.GameID, mib.MemberID);
                ptRes.timeRemaining = trackingTransactionManager.GetTimeRemaining(mib.GameID, mib.MemberID).ToString();

            }
            else
            {

                trackingTransactionManager.CompletePaymentTrackingTransaction(mib.GameID, mib.MemberID);
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
                Address deliveryAddress = db.Addresses.FirstOrDefault(x => x.MemberID == mib.MemberID && x.AddressType.ToLower() == "postal");

                // get quantity won - divide quantity from pig by number of winners ??? really??? ok then.....
                Game qtyGame = db.Games.Find(mib.GameID);

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

        public string GetGameUnlockTime(int GameID)
        {
            DateTime gameUnlockDateTime = new DateTime();

            var gameRules = from gr in db.GameRules where gr.GameID == GameID && gr.GameRuleCode == "StartGame" select gr;
            foreach (var gameRule in gameRules)
            {
                // Game unlock is 5 min before game start. 
                gameUnlockDateTime = gameRule.ExcecuteTime.AddMinutes(-5);
            }

            return gameUnlockDateTime.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
        }

        public string GetServerTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }

        private void sendWinnermail(String recipientName, String email, Address deliveryAddress, int winQuantity)
        {
            string deliveryAddressFormatted = "No delivery address supplied.";

            if (deliveryAddress != null)
            {
                deliveryAddressFormatted = (deliveryAddress.AddressLine1 != null && deliveryAddress.AddressLine1.Trim() != "") ? deliveryAddress.AddressLine1.Trim() + "<br/>" : "";
                deliveryAddressFormatted = deliveryAddressFormatted + ((deliveryAddress.AddressLine2 != null && deliveryAddress.AddressLine2.Trim() != "") ? deliveryAddress.AddressLine2.Trim() + "<br/>" : "");
                deliveryAddressFormatted = deliveryAddressFormatted + ((deliveryAddress.AddressLine3 != null && deliveryAddress.AddressLine3.Trim() != "") ? deliveryAddress.AddressLine3.Trim() + "<br/>" : "");
                deliveryAddressFormatted = deliveryAddressFormatted + ((deliveryAddress.CityName != null && deliveryAddress.CityName.Trim() != "") ? deliveryAddress.CityName.Trim() + "<br/>" : "");
                deliveryAddressFormatted = deliveryAddressFormatted + ((deliveryAddress.StateOrProvince != null && deliveryAddress.StateOrProvince.Trim() != "") ? deliveryAddress.StateOrProvince.Trim() + "<br/>" : "");
                deliveryAddressFormatted = deliveryAddressFormatted + ((deliveryAddress.ZipOrPostalCode != null && deliveryAddress.ZipOrPostalCode.Trim() != "") ? deliveryAddress.ZipOrPostalCode.Trim() + "<br/>" : "");
                deliveryAddressFormatted = deliveryAddressFormatted + ((deliveryAddress.Country != null && deliveryAddress.Country.Trim() != "") ? deliveryAddress.Country.Trim() + "<br/>" : "");
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
