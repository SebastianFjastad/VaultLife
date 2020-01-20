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
    public class TerritoryController : Controller
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();

        // GET: /Territory/
        public ActionResult Index()
        {
            var territories = db.Territories.Include(t => t.Contract).Include(t => t.Owner);
            return View(territories.ToList());
        }

        // GET: /Territory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Territory territory = db.Territories.Find(id);
            if (territory == null)
            {
                return HttpNotFound();
            }
            return View(territory);
        }

        // GET: /Territory/Create
        public ActionResult Create()
        {
            Territory territory = new Territory();
            territory = (Territory)Helpers.TableTracker.TrackCreate(territory, "USR");
            ViewBag.ContractID = new SelectList(db.Contracts, "ContractID", "ContractCode");
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerCode");
            return View(territory);
        }

        // POST: /Territory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TerritoryID,TerritoryCode,OwnerID,ContractID,TerritoryName,TerritoryDescription,DateInserted,DateUpdated,USR")] Territory territory)
        {
            if (ModelState.IsValid)
            {
                db.Territories.Add(territory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContractID = new SelectList(db.Contracts, "ContractID", "ContractCode", territory.ContractID);
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerCode", territory.OwnerID);
            return View(territory);
        }

        // GET: /Territory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Territory territory = db.Territories.Find(id);
            
            territory = (Territory)Helpers.TableTracker.TrackEdit(territory, "USR");


            if (territory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContractID = new SelectList(db.Contracts, "ContractID", "ContractCode", territory.ContractID);
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerCode", territory.OwnerID);
            return View(territory);
        }

        // POST: /Territory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TerritoryID,TerritoryCode,OwnerID,ContractID,TerritoryName,TerritoryDescription,DateInserted,DateUpdated,USR")] Territory territory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(territory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContractID = new SelectList(db.Contracts, "ContractID", "ContractCode", territory.ContractID);
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerCode", territory.OwnerID);
            return View(territory);
        }

        // GET: /Territory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Territory territory = db.Territories.Find(id);
            if (territory == null)
            {
                return HttpNotFound();
            }
            return View(territory);
        }

        // POST: /Territory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Territory territory = db.Territories.Find(id);
            db.Territories.Remove(territory);
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
