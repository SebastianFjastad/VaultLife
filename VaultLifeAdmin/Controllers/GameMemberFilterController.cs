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
    public class GameMemberFilterController : Controller
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();

        // GET: /GameMemberFilter/
        public ActionResult Index()
        {

            List<GameMemberFilterViewModel> list = new List<GameMemberFilterViewModel>();

            foreach (var gmf in db.GameMemberFilters.ToList())
            {
                list.Add(new GameMemberFilterViewModel(gmf));
            
            }
            
            return View(list);
        }

        // GET: /GameMemberFilter/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameMemberFilter gamememberfilter = db.GameMemberFilters.Find(id);
            if (gamememberfilter == null)
            {
                return HttpNotFound();
            }
            return View(gamememberfilter);
        }


        [HttpPost]
        public JsonResult getStates(FormCollection form)
        {



            string Countryid = form["CountryID"].ToString().Trim();
            //string[] CountiD = CID.Split('=');

            int CID = Convert.ToInt32(Countryid);
            var states = from x in db.CountryStates.AsEnumerable()
                         where x.CountryID.Equals(CID)
                         select new { StateID = x.StateID, StateName = x.StateName };

            return Json(states.ToArray());
        }

        // GET: /GameMemberFilter/Create
        public ActionResult Create()
        {

            List<SelectListItem> AgeGroups;
            List<SelectListItem> Genders;
            GetAgesAndGenders(out AgeGroups, out Genders);

            ViewBag.AgeBandID = new SelectList(AgeGroups, "Value", "Text");
            ViewBag.GenderID = new SelectList(Genders, "Value", "text");
            ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode");
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryName");
            ViewBag.StateID = new SelectList(db.CountryStates, "StateID", "StateName");
            
            ViewBag.Territory = new SelectList(db.Territories, "TerritoryID", "TerritoryCode");
            ViewBag.MemberSubscriptionTypeID = new SelectList(db.MemberSubscriptionTypes, "MemberSubscriptionTypeID", "MemberSubscriptionTypeCode");

            return View();
        }

        // POST: /GameMemberFilter/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind( Include="GameMemberFilterID,GameID,CountryID,StateID,CityID,AgeBandID,GenderID,Territory,MemberSubscriptionTypeID")] GameMemberFilter gamememberfilter)
        {
            List<SelectListItem> AgeGroups;
            List<SelectListItem> Genders;
            GetAgesAndGenders(out AgeGroups, out Genders);

            if (ModelState.IsValid)
            {
                db.GameMemberFilters.Add(gamememberfilter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AgeBandID = new SelectList(AgeGroups, "Value", "Text");
            ViewBag.GenderID = new SelectList(Genders, "Value", "text");
            ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode");
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryName");
            ViewBag.StateID = new SelectList(db.CountryStates, "StateID", "StateName");

            ViewBag.Territory = new SelectList(db.Territories, "TerritoryID", "TerritoryCode");
            ViewBag.MemberSubscriptionTypeID = new SelectList(db.MemberSubscriptionTypes, "MemberSubscriptionTypeID", "MemberSubscriptionTypeCode");

            return View(gamememberfilter);
        }

        private static void GetAgesAndGenders(out List<SelectListItem> AgeGroups, out List<SelectListItem> Genders)
        {
            AgeGroups = new List<SelectListItem>();

            AgeGroups.Add(new SelectListItem
            {
                Text = "Any",
                Value = "0"
            });
            AgeGroups.Add(new SelectListItem
            {
                Text = "18-25",
                Value = "1"
            });
            AgeGroups.Add(new SelectListItem
            {
                Text = "25-35",
                Value = "2",
                Selected = true
            });
            AgeGroups.Add(new SelectListItem
            {
                Text = "35-45",
                Value = "3"
            });
            AgeGroups.Add(new SelectListItem
            {
                Text = "45-55",
                Value = "4"
            });
            AgeGroups.Add(new SelectListItem
            {
                Text = " Older Than 55",
                Value = "5"
            });

            Genders = new List<SelectListItem>();
            Genders.Add(new SelectListItem
            {
                Text = "Any",
                Value = "0"

            });
            Genders.Add(new SelectListItem
            {
                Text = "Male",
                Value = "1"
            });
            Genders.Add(new SelectListItem
            {
                Text = "Female",
                Value = "2",

            });
        }

        // GET: /GameMemberFilter/Edit/5
        public ActionResult Edit(int? id)
        {

            List<SelectListItem> AgeGroups;
            List<SelectListItem> Genders;
            GetAgesAndGenders(out AgeGroups, out Genders);

            ViewBag.AgeBandID = new SelectList(AgeGroups, "Value", "Text");
            ViewBag.GenderID = new SelectList(Genders, "Value", "text");
            ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode");
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryName");
            ViewBag.StateID = new SelectList(db.CountryStates, "StateID", "StateName");

            ViewBag.Territory = new SelectList(db.Territories, "TerritoryID", "TerritoryCode");
            ViewBag.MemberSubscriptionTypeID = new SelectList(db.MemberSubscriptionTypes, "MemberSubscriptionTypeID", "MemberSubscriptionTypeCode");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameMemberFilter gamememberfilter = db.GameMemberFilters.Find(id);
            if (gamememberfilter == null)
            {
                return HttpNotFound();
            }
            return View(gamememberfilter);
        }

        // POST: /GameMemberFilter/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="GameMemberFilterID,GameID,CountryID,StateID,CityID,AgeBandID,GenderID,Territory,MemberSubscriptionTypeID")] GameMemberFilter gamememberfilter)
        {
            List<SelectListItem> AgeGroups;
            List<SelectListItem> Genders;
            GetAgesAndGenders(out AgeGroups, out Genders);

            if (ModelState.IsValid)
            {
                db.Entry(gamememberfilter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AgeBandID = new SelectList(AgeGroups, "Value", "Text");
            ViewBag.GenderID = new SelectList(Genders, "Value", "text");
            ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode", gamememberfilter.GameID);
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryName", gamememberfilter.CountryID);
            ViewBag.StateID = new SelectList(db.CountryStates, "StateID", "StateName", gamememberfilter.StateID);

            ViewBag.Territory = new SelectList(db.Territories, "TerritoryID", "TerritoryCode", gamememberfilter.Territory);
            ViewBag.MemberSubscriptionTypeID = new SelectList(db.MemberSubscriptionTypes, "MemberSubscriptionTypeID", "MemberSubscriptionTypeCode", gamememberfilter.MemberSubscriptionTypeID);

            return View(gamememberfilter);


        }

        // GET: /GameMemberFilter/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameMemberFilter gamememberfilter = db.GameMemberFilters.Find(id);
            if (gamememberfilter == null)
            {
                return HttpNotFound();
            }
            return View(gamememberfilter);
        }

        // POST: /GameMemberFilter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GameMemberFilter gamememberfilter = db.GameMemberFilters.Find(id);
            db.GameMemberFilters.Remove(gamememberfilter);
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
