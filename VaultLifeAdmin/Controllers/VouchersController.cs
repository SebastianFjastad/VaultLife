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
using VaultLifeAdmin.Models;

namespace VaultLifeAdmin.Controllers
{
    public class VouchersController : Controller
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();

        // GET: Vouchers
        public ActionResult Index()
        {
            var vouchers = db.Vouchers.Include(v => v.ProductLocation);
            return View(vouchers.ToList());
        }

        // GET: Vouchers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voucher voucher = db.Vouchers.Find(id);
            if (voucher == null)
            {
                return HttpNotFound();
            }
            return View(voucher);
        }

        // GET: Vouchers/Create
        public ActionResult Create()
        {
            ViewBag.ProductLocationID = new SelectList(db.ProductLocations, "ProductLocationID", "USR");
            Voucher model = new Voucher();
            model = (Voucher)Helpers.TableTracker.TrackCreate(model, "USR", true);
            return View(model);
        }

        // POST: Vouchers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VoucherID,VoucherNumber,ProductLocationID,Used,DateInserted,DateUsed")] Voucher voucher)
        {
             VoucherValidator validator = new VoucherValidator();

            if (ModelState.IsValid && validator.Validate(voucher).IsValid)
            {
                db.Vouchers.Add(voucher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductLocationID = new SelectList(db.ProductLocations, "ProductLocationID", "USR", voucher.ProductLocationID);
            return View(voucher);
        }


        // GET: Vouchers/Create
        public ActionResult CreateForProductLocation(int ProductLocationID)
        {
            ViewBag.ProductLocationID = ProductLocationID;
            Voucher model = new Voucher();
            model = (Voucher)Helpers.TableTracker.TrackCreate(model, "USR", true);
            return View(model);
        }

        // POST: Vouchers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateForProductLocation(Voucher voucher)
        {
            voucher.Used = false;
            voucher.DateInserted = DateTime.Now;


            int ProductInGameID = db.ProductLocations.Find(voucher.ProductLocationID).ProductInGameID;

            VoucherValidator validator = new VoucherValidator();

            if (ModelState.IsValid && validator.Validate(voucher).IsValid)
            {
                db.Vouchers.Add(voucher);
                db.SaveChanges();
                return RedirectToAction("EditFromProductInGame", "ProductLocations", new { id = voucher.ProductLocationID, productInGameID = ProductInGameID });
            }


            FluentValidation.Results.ValidationResult results = validator.Validate(voucher);
            IList<ValidationFailure> failures = results.Errors;
            StringBuilder sb = new StringBuilder();
            foreach (var e in results.Errors)
            {

                ModelState.AddModelError(e.PropertyName + "Error", e.ErrorMessage);
                sb.AppendLine(e.ErrorMessage);

            }


            ViewBag.ProductLocationID = voucher.ProductLocationID;
            return View(voucher);
        }


        // GET: Vouchers/Create
        public ActionResult CreateForProductLocationMakeGame(int ProductLocationID)
        {
            ViewBag.ProductLocationID = ProductLocationID;
            Voucher model = new Voucher();
            model = (Voucher)Helpers.TableTracker.TrackCreate(model, "USR", true);
            return View(model);
        }

        // POST: Vouchers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateForProductLocationMakeGame(Voucher voucher)
        {
            voucher.Used = false;
            voucher.DateInserted = DateTime.Now;


            int ProductInGameID = db.ProductLocations.Find(voucher.ProductLocationID).ProductInGameID;

            VoucherValidator validator = new VoucherValidator();

            if (ModelState.IsValid && validator.Validate(voucher).IsValid)
            {
                db.Vouchers.Add(voucher);
                db.SaveChanges();
                return RedirectToAction("EditFromProductInGameMakeGame", "ProductLocations", new { id = voucher.ProductLocationID, productInGameID = ProductInGameID });
            }


            FluentValidation.Results.ValidationResult results = validator.Validate(voucher);
            IList<ValidationFailure> failures = results.Errors;
            StringBuilder sb = new StringBuilder();
            foreach (var e in results.Errors)
            {

                ModelState.AddModelError(e.PropertyName + "Error", e.ErrorMessage);
                sb.AppendLine(e.ErrorMessage);

            }


            ViewBag.ProductLocationID = voucher.ProductLocationID;
            return View(voucher);
        }



        // GET: Vouchers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voucher model = db.Vouchers.Find(id);
            model = (Voucher)Helpers.TableTracker.TrackEdit(model, "USR");


            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductLocationID = new SelectList(db.ProductLocations, "ProductLocationID", "USR", model.ProductLocationID);
            return View(model);
        }

        // POST: Vouchers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VoucherID,VoucherNumber,ProductLocationID,Used,DateInserted,DateUsed")] Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voucher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductLocationID = new SelectList(db.ProductLocations, "ProductLocationID", "USR", voucher.ProductLocationID);
            return View(voucher);
        }

        // GET: Vouchers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voucher voucher = db.Vouchers.Find(id);
            if (voucher == null)
            {
                return HttpNotFound();
            }
            return View(voucher);
        }

        // POST: Vouchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Voucher voucher = db.Vouchers.Find(id);
            db.Vouchers.Remove(voucher);
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
