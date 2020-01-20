using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VaultLifeAdmin.ViewModels;
using System.Linq.Expressions;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using VaultLifeAdmin.Models;
using System.Web.Script.Serialization;

namespace VaultLifeAdmin.Controllers
{
    public class MembersInGamesController : Controller
    {

        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();
        
        
        public ActionResult Index(int GameID)
        {
            var model = new MembersInGamesModel();

            List<SelectListItem> AgeGroups = new List<SelectListItem>();
          
            AgeGroups.Add(new SelectListItem
            {
                Text = "Any",
                Value = null
            });
            AgeGroups.Add(new SelectListItem
            {
                Text = "18-25",
                Value = "18-25"
            });
            AgeGroups.Add(new SelectListItem
            {
                Text = "25-35",
                Value = "25-35",
                Selected = true
            });
            AgeGroups.Add(new SelectListItem
            {
                Text = "35-45",
                Value = "35-45"
            });
            AgeGroups.Add(new SelectListItem
            {
                Text = "45-55",
                Value = "45-55"
            });
            AgeGroups.Add(new SelectListItem
            {
                Text = " Older Than 55",
                Value = "55-105"
            });

            List<SelectListItem> Genders = new List<SelectListItem>();
            Genders.Add(new SelectListItem
            {
                Text = "Any",
                Value = null

            });
            Genders.Add(new SelectListItem
            {
                Text = "Male",
                Value = "m"
            });
            Genders.Add(new SelectListItem
            {
                Text = "Female",
                Value = "f",
                
            });
            

            List<SelectListItem> Ethnics = new List<SelectListItem>();
            Ethnics.Add(new SelectListItem
            {
                Text = "Any",
                Value = null
            });
            Ethnics.Add(new SelectListItem
            {
                Text = "African",
                Value = "African"
            });
            Ethnics.Add(new SelectListItem
            {
                Text = "Coloured",
                Value = "Coloured"
                
            });
             Ethnics.Add(new SelectListItem
            {
                Text = "Indian",
                Value = "Indian"
                
            }); Ethnics.Add(new SelectListItem
            {
                Text = "White",
                Value = "White"
                
            });

            

            ViewBag.AgeGroup = new SelectList(AgeGroups,"Value","Text");
         
            ViewBag.Gender = new SelectList(Genders, "Value", "text");
            ViewBag.Ethnicity = new SelectList(Ethnics, "Value", "text");
            ViewBag.Game = new SelectList(db.Games, "GameID", "GameName",GameID);
            List<Country> countries = db.Countries.ToList();
            countries.Add(new Country {CountryName = "All", CountryID = 0});
            ViewBag.Country = new SelectList(countries.OrderBy(c=>c.CountryID), "CountryID", "CountryName");
             List<CountryState> states = db.CountryStates.ToList();
             states.Add(new CountryState { StateName = "All", StateID = 0 });
            ViewBag.State  = new SelectList(states.OrderBy(s=>s.StateName), "StateID", "StateName");
            ViewBag.MemberSubscriptionType = new SelectList(db.MemberSubscriptionTypes, "MemberSubscriptionTypeID", "MemberSubscriptionTypeDescription");
            //ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerCode");

            
            
            return View(model);
            
        }

        [HttpPost]
        public PartialViewResult Count(FormCollection form)
        {

            string MemberSubscriptionTypeid = form["MemberSubscriptionType"].ToString().Trim();
            string AgeGroups                = form["AgeGroup"].ToString();
            string Countryid                = form["Country"].ToString().Trim();
            string Stateid                  = string.IsNullOrEmpty(form["State"])? string.Empty : form["State"].ToString().Trim();
            string Ethnicity                = form["Ethnicity"].ToString();
            string Gender                  = form["Gender"].ToString();

            var Predicates="";

            if (!string.IsNullOrEmpty(MemberSubscriptionTypeid))
            {

                Predicates += " AND MemberSubscriptionTypeid >= " + MemberSubscriptionTypeid + "";
            }
            if (!string.IsNullOrEmpty(AgeGroups))
            {

                string[] Ages = AgeGroups.Split('-');
                int Left = Convert.ToInt32(Ages[0]);
                int Right = Convert.ToInt32(Ages[1]);

                 var leftLimit = (DateTime.Now.AddYears(-Right)).Date;
                 var RightLimit = (DateTime.Now.AddYears(-Left)).Date;


                 Predicates += " AND DateOfBirth Between '" + leftLimit.ToString("yyyy-MM-dd") + "' AND '" + RightLimit.ToString("yyyy-MM-dd") + "'";
            }



            if (!Countryid.Equals("0"))
            {

                Predicates += " AND Countryid = " + Convert.ToInt32(Countryid);
            }

            if (!string.IsNullOrEmpty(Stateid) && !Stateid.Equals("0"))
            {

                Predicates += " AND StateID in ( " + (Stateid) + ")";
            }
            if (!string.IsNullOrEmpty(Ethnicity))
            {

                Predicates += " AND Ethnicity = '" + Ethnicity +"'";
            }
            if (!string.IsNullOrEmpty(Gender))
            {

                Predicates += " AND Gender = '" + Gender +"'";
            }
            //TODO select count
            var SQL = "SELECT * FROM MEMBER WHERE 1=1 " + Predicates.ToString();
            using (var context = new VaultLifeApplicationEntities())
            {
                var MembersCount = context.Members.SqlQuery(SQL).ToList();
                ViewBag.number = MembersCount.Count() ;
             }


                
            return PartialView("_MemberCount");
        }


        [HttpPost]
        public PartialViewResult Insert(FormCollection form)
        {

            string Gameid = form["Game"].ToString();
            string MemberSubscriptionTypeid = form["MemberSubscriptionType"].ToString().Trim();
            string AgeGroups = form["AgeGroup"].ToString();
            string Countryid = form["Country"].ToString().Trim();
            string Stateid = string.IsNullOrEmpty(form["State"]) ? string.Empty : form["State"].ToString().Trim();
            string Ethnicity = form["Ethnicity"].ToString();
            string Gender = form["Gender"].ToString();
   
            var Predicates = "";

            if (!string.IsNullOrEmpty(MemberSubscriptionTypeid))
            {

                Predicates += "AND MemberSubscriptionTypeid >= " + MemberSubscriptionTypeid + ""; 
                ///TODO hmmm wrong.... 
            }


            if (!string.IsNullOrEmpty(AgeGroups))
            {

                string[] Ages = AgeGroups.Split('-');
                int Left = Convert.ToInt32(Ages[0]);
                int Right = Convert.ToInt32(Ages[1]);

                var leftLimit = (DateTime.Now.AddYears(-Right)).Date;
                var RightLimit = (DateTime.Now.AddYears(-Left)).Date;


                Predicates += " AND DateOfBirth Between '" + leftLimit.ToString("yyyy-MM-dd") + "' AND '" + RightLimit.ToString("yyyy-MM-dd") + "'";
            }

            
            if (!Countryid.Equals("0"))
            {

                Predicates += " AND Countryid = " + Convert.ToInt32(Countryid);
            }

            if (!string.IsNullOrEmpty(Stateid) && !Stateid.Equals("0"))
            {

                Predicates += " AND StateID in ( " + (Stateid)+")";
            }
            if (!string.IsNullOrEmpty(Ethnicity))
            {

                Predicates += " AND Ethnicity = '" + Ethnicity+"'";
            }
            if (!string.IsNullOrEmpty(Gender))
            {

                Predicates += " AND Gender = '" + Gender+"'";
            }

            var SQL = " DELETE FROM MEMBERINGAME WHERE GAMEID = " + Gameid + "; INSERT INTO MEMBERINGAME (GameId,MemberID,CountryID,StateID,CityID,MemberSubscriptionTypeID,DateInserted,DateUpdated,USR,Winindicator,PaymentIndicator) SELECT " + Gameid + ", MemberId , Countryid,Stateid,0, MemberSubscriptionTypeid,'" + DateTime.Now + "','" + DateTime.Now + "','USR' ,0,0  FROM MEMBER WHERE 1=1" + Predicates.ToString();
            using (var context = new VaultLifeApplicationEntities())
            {
                
               var noOfRowInserted = context.Database.ExecuteSqlCommand(SQL);
               ViewBag.number = noOfRowInserted;
            }

            if (Countryid.Equals("0"))
            {
                int id = Convert.ToInt32(Gameid);
                Game game = db.Games.Where(g => g.GameID == id).First();
                game.Global = true;
                db.SaveChanges();

            }

            return PartialView("_MemberInGameSuccess");
        }


        [HttpPost]
        public JsonResult getStates(FormCollection form)
        {



            string Countryid = form["Country"].ToString().Trim();
            //string[] CountiD = CID.Split('=');

            int CID = Convert.ToInt32(Countryid);

            List<CountryState> states = db.CountryStates.Where(c => c.CountryID == CID).ToList();
               states.Add(new CountryState { StateName = "All", StateID = 0 });
               
            var myStates = from x in states.AsEnumerable()
                           select new { StateID = x.StateID, StateName = x.StateName };

            return Json(myStates.ToArray());
        }

        
    }
}
