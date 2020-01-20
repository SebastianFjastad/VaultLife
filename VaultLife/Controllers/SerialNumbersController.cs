using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Vaultlife.Models;

namespace Vaultlife.Controllers
{
    public class SerialNumbersController : Controller
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();

        // GET: SerialNumbers
        public ActionResult Index()
        {
            var serialNumbers = db.SerialNumbers.Include(s => s.ProductLocation);
            return View(serialNumbers.ToList());
        }

        // GET: SerialNumbers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SerialNumber serialNumber = db.SerialNumbers.Find(id);
            if (serialNumber == null)
            {
                return HttpNotFound();
            }
            return View(serialNumber);
        }

        // GET: SerialNumbers/Create
        public ActionResult Create()
        {
            ViewBag.ProductLocationID = new SelectList(db.ProductLocations, "ProductLocationID", "USR");
            SerialNumber model = new SerialNumber();
            model = (SerialNumber)Helpers.TableTracker.TrackCreate(model, User.Identity.Name, true);
            return View(model);

        }

        // POST: SerialNumbers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SerialNumberID,Serial,ProductLocationID,Used,DateInserted,DateUsed")] SerialNumber serialNumber)
        {
            if (ModelState.IsValid)
            {
                db.SerialNumbers.Add(serialNumber);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductLocationID = new SelectList(db.ProductLocations, "ProductLocationID", "USR", serialNumber.ProductLocationID);
            return View(serialNumber);
        }

        // GET: SerialNumbers/Create
        public ActionResult CreateForProductLocation(int ProductLocationID)
        {
            ViewBag.ProductLocationID = ProductLocationID;
            SerialNumber model = new SerialNumber();
            model = (SerialNumber)Helpers.TableTracker.TrackCreate(model, User.Identity.Name, true);
            return View(model);
        }

        // POST: SerialNumbers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateForProductLocation(SerialNumber serialNumber)
        {

            serialNumber.Used = false;
            serialNumber.DateInserted = DateTime.Now;

            int ProductInGameID = db.ProductLocations.Find(serialNumber.ProductLocationID).ProductInGameID;

            SerialNumberValidator validator = new SerialNumberValidator();

            if (ModelState.IsValid && validator.Validate(serialNumber).IsValid)
            {
                db.SerialNumbers.Add(serialNumber);
                db.SaveChanges();
                return RedirectToAction("EditFromProductInGame", "ProductLocations", new { id = serialNumber.ProductLocationID, productInGameID = ProductInGameID });
            }


            FluentValidation.Results.ValidationResult results = validator.Validate(serialNumber);
            IList<ValidationFailure> failures = results.Errors;
            StringBuilder sb = new StringBuilder();
            foreach (var e in results.Errors)
            {

                ModelState.AddModelError(e.PropertyName + "Error", e.ErrorMessage);
                sb.AppendLine(e.ErrorMessage);

            }

            ViewBag.ProductLocationID = serialNumber.ProductLocationID;
            return View(serialNumber);
        }


        public ActionResult CreateForProductLocationMakeGame(int ProductLocationID)
        {
            ViewBag.ProductLocationID = ProductLocationID;
            SerialNumber model = new SerialNumber();
            model = (SerialNumber)Helpers.TableTracker.TrackCreate(model, User.Identity.Name, true);
            return View(model);
        }

        // POST: SerialNumbers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateForProductLocationMakeGame(SerialNumber serialNumber)
        {

            serialNumber.Used = false;
            serialNumber.DateInserted = DateTime.Now;

            int ProductInGameID = db.ProductLocations.Find(serialNumber.ProductLocationID).ProductInGameID;

            SerialNumberValidator validator = new SerialNumberValidator();

            if (ModelState.IsValid && validator.Validate(serialNumber).IsValid)
            {
                db.SerialNumbers.Add(serialNumber);
                db.SaveChanges();
                return RedirectToAction("EditFromProductInGameMakeGame", "ProductLocations", new { id = serialNumber.ProductLocationID, productInGameID = ProductInGameID });
            }


            FluentValidation.Results.ValidationResult results = validator.Validate(serialNumber);
            IList<ValidationFailure> failures = results.Errors;
            StringBuilder sb = new StringBuilder();
            foreach (var e in results.Errors)
            {

                ModelState.AddModelError(e.PropertyName + "Error", e.ErrorMessage);
                sb.AppendLine(e.ErrorMessage);

            }

            ViewBag.ProductLocationID = serialNumber.ProductLocationID;
            return View(serialNumber);
        }

        // GET: SerialNumbers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SerialNumber model = db.SerialNumbers.Find(id);

            model = (SerialNumber)Helpers.TableTracker.TrackEdit(model, User.Identity.Name);


            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductLocationID = new SelectList(db.ProductLocations, "ProductLocationID", "USR", model.ProductLocationID);
            return View(model);
        }

        // POST: SerialNumbers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SerialNumberID,Serial,ProductLocationID,Used,DateInserted,DateUsed")] SerialNumber serialNumber)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serialNumber).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductLocationID = new SelectList(db.ProductLocations, "ProductLocationID", "USR", serialNumber.ProductLocationID);
            return View(serialNumber);
        }

        // GET: SerialNumbers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SerialNumber serialNumber = db.SerialNumbers.Find(id);
            if (serialNumber == null)
            {
                return HttpNotFound();
            }
            return View(serialNumber);
        }

        // POST: SerialNumbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SerialNumber serialNumber = db.SerialNumbers.Find(id);
            db.SerialNumbers.Remove(serialNumber);
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
