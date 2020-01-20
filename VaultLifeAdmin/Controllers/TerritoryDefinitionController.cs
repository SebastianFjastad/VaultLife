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
    public class TerritoryDefinitionController : Controller
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();

        // GET: /TerritoryDefinition/
        public ActionResult Index()
        {
            var territorydefinitions = db.TerritoryDefinitions.Include(t => t.Territory);
            return View(territorydefinitions.ToList());
        }

        // GET: /TerritoryDefinition/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TerritoryDefinition territorydefinition = db.TerritoryDefinitions.Find(id);
            if (territorydefinition == null)
            {
                return HttpNotFound();
            }
            return View(territorydefinition);
        }

        // GET: /TerritoryDefinition/Create
        public ActionResult Create()
        {
            ViewBag.TerritoryID = new SelectList(db.Territories, "TerritoryID", "TerritoryCode");
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryName");
            ViewBag.StateID = new SelectList(db.CountryStates, "StateID", "StateName");
            ViewBag.CityID = new SelectList(db.CountryCities, "CityID", "CityName");
            return View();
        }

        // POST: /TerritoryDefinition/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CountryID,StateID,CityID,TerritoryDefinitionID,TerritoryDefinitionCode,TerritoryID,ZipOrPostalCode,IPAddress,PhysicalCoordinates,DateInserted,DateUpdated,USR")] TerritoryDefinition territorydefinition)
        {
            if (ModelState.IsValid)
            {
                db.TerritoryDefinitions.Add(territorydefinition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TerritoryID = new SelectList(db.Territories, "TerritoryID", "TerritoryCode", territorydefinition.TerritoryID);
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryName", territorydefinition.CountryID);
            ViewBag.StateID = new SelectList(db.CountryStates, "StateID", "StateName", territorydefinition.StateID);
            ViewBag.CityID = new SelectList(db.CountryCities, "CityID", "CityName", territorydefinition.CityID);
            return View(territorydefinition);
        }

        // GET: /TerritoryDefinition/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TerritoryDefinition territorydefinition = db.TerritoryDefinitions.Find(id);
            if (territorydefinition == null)
            {
                return HttpNotFound();
            }
            ViewBag.TerritoryID = new SelectList(db.Territories, "TerritoryID", "TerritoryCode", territorydefinition.TerritoryID);

            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryName", territorydefinition.CountryID);
            ViewBag.StateID = new SelectList(db.CountryStates.Where(x => x.CountryID == territorydefinition.CountryID), "StateID", "StateName", territorydefinition.StateID);
            ViewBag.CityID = new SelectList(db.CountryCities.Where(x => x.CountryID == territorydefinition.CountryID && x.StateID == territorydefinition.StateID), "CityID", "CityName", territorydefinition.CityID);
            return View(territorydefinition);
        }

        // POST: /TerritoryDefinition/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CountryID,StateID,CityID,TerritoryDefinitionID,TerritoryDefinitionCode,TerritoryID,ZipOrPostalCode,IPAddress,PhysicalCoordinates,DateInserted,DateUpdated,USR")] TerritoryDefinition territorydefinition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(territorydefinition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TerritoryID = new SelectList(db.Territories, "TerritoryID", "TerritoryCode", territorydefinition.TerritoryID);
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryName", territorydefinition.CountryID);
            ViewBag.StateID = new SelectList(db.CountryStates.Where(x => x.CountryID == territorydefinition.CountryID), "StateID", "StateName", territorydefinition.StateID);
            ViewBag.CityID = new SelectList(db.CountryCities.Where(x => x.CountryID == territorydefinition.CountryID && x.StateID == territorydefinition.StateID), "CityID", "CityName", territorydefinition.CityID);
            return View(territorydefinition);
        }

        // GET: /TerritoryDefinition/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TerritoryDefinition territorydefinition = db.TerritoryDefinitions.Find(id);
            if (territorydefinition == null)
            {
                return HttpNotFound();
            }
            return View(territorydefinition);
        }

        // POST: /TerritoryDefinition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TerritoryDefinition territorydefinition = db.TerritoryDefinitions.Find(id);
            db.TerritoryDefinitions.Remove(territorydefinition);
            db.SaveChanges();
            return RedirectToAction("Index");
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
        [HttpPost]
        public JsonResult getCities(FormCollection form)
        {
            string Countryid = form["CountryID"].ToString().Trim();
            string Stateid = form["StateID"].ToString().Trim();


            int CID = Convert.ToInt32(Countryid);
            int SID = Convert.ToInt32(Stateid);
            var cities = from x in db.CountryCities.AsEnumerable()
                         where x.CountryID.Equals(CID) && x.StateID.Equals(SID)
                         select new { CityID = x.CityID, CityName = x.CityName };


            return Json(cities.ToArray());
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
