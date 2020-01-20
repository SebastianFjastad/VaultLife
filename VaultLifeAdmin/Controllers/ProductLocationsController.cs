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
using VaultLifeAdmin.ViewModels;

namespace VaultLifeAdmin.Controllers
{
    public class ProductLocationsController : Controller
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();

        // GET: ProductLocations
        public ActionResult Index()
        {
            var productLocations = db.ProductLocations.Include(p => p.ProductInGame);
            return View(productLocations.ToList());
        }

        // GET: ProductLocations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductLocation productLocation = db.ProductLocations.Find(id);
            if (productLocation == null)
            {
                return HttpNotFound();
            }
            return View(productLocation);
        }

        // GET: ProductLocations/Create
        public ActionResult Create()
        {
            ViewBag.ProductInGameID = new SelectList(db.ProductInGames, "ProductInGameID", "Currency");
            ProductInGame model = new ProductInGame();
            model = (ProductInGame)Helpers.TableTracker.TrackCreate(model, "USR");
            return View(model);
        }


        // POST: ProductLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductLocationID,ProductInGameID,Address,DeliveryAgentName,DateInserted,DateUpdated,USR")] ProductLocation productLocation)
        {
            if (ModelState.IsValid)
            {
                db.ProductLocations.Add(productLocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductInGameID = new SelectList(db.ProductInGames, "ProductInGameID", "Currency", productLocation.ProductInGameID);
            return View(productLocation);
        }



        public ActionResult CreateForProductInGameMakeGame(int productInGameID)
        {
            ViewBag.ProductInGame = db.ProductInGames.Find(productInGameID);
            
            ViewBag.ProductInGameID = productInGameID;
            ProductLocation model = new ProductLocation();
            model = (ProductLocation)Helpers.TableTracker.TrackCreate(model, "USR");
            return View(model);

        }

        // POST: ProductLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateForProductInGameMakeGame([Bind(Include = "ProductLocationID,ProductInGameID,Address,DeliveryAgentName,DateInserted,DateUpdated,USR")] ProductLocation productLocation)
        {
            int GameID = db.ProductInGames.Find(productLocation.ProductInGameID).GameID;

            if (ModelState.IsValid)
            {

                productLocation.DateUpdated = DateTime.Now;
                productLocation.DateInserted = DateTime.Now;
                productLocation.USR = "USR";

                db.ProductLocations.Add(productLocation);
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    var ee = e;
                }
                return RedirectToAction("EditMakeGame", "Games", new { id = GameID });
            }
            ViewBag.ProductInGameID = productLocation.ProductInGameID;
            
            return View(productLocation);
        }



        public ActionResult CreateForProductInGame(int productInGameID)
        {
            ViewBag.ProductInGame = db.ProductInGames.Find(productInGameID);
            
            ViewBag.ProductInGameID = productInGameID;

            ProductLocation model = new ProductLocation();
            model = (ProductLocation)Helpers.TableTracker.TrackCreate(model, "USR");
            return View(model);

        }

        // POST: ProductLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateForProductInGame([Bind(Include = "ProductLocationID,ProductInGameID,Address,DeliveryAgentName,DateInserted,DateUpdated,USR")] ProductLocation productLocation)
        {
           

            if (ModelState.IsValid)
            {

           
                db.ProductLocations.Add(productLocation);
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    var ee = e;
                }
                return RedirectToAction("Edit", "ProductInGames", new { id = productLocation.ProductInGameID });
            }
            ViewBag.ProductInGameID = productLocation.ProductInGameID;
            
            return View(productLocation);
        }


        // GET: ProductLocations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductLocation model = db.ProductLocations.Find(id);
            model = (ProductLocation)Helpers.TableTracker.TrackEdit(model, "USR");


            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductInGameID = new SelectList(db.ProductInGames, "ProductInGameID", "Currency", model.ProductInGameID);
            return View(model);
        }

        // POST: ProductLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductLocationID,ProductInGameID,Address,DeliveryAgentName,DateInserted,DateUpdated,USR")] ProductLocation productLocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productLocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductInGameID = new SelectList(db.ProductInGames, "ProductInGameID", "Currency", productLocation.ProductInGameID);
            return View(productLocation);
        }

        

              // GET: ProductLocations/Edit/5
        public ActionResult EditFromProductInGameMakeGame(int? id, int ProductInGameID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductLocation productLocation = db.ProductLocations.Include(x=>x.SerialNumbers).Include(x=>x.Vouchers).Where(x=>x.ProductLocationID == id).First();

            if (productLocation == null)
            {
                return HttpNotFound();
            }
            ProductLocationViewModel plvm = new ProductLocationViewModel();
            plvm.ProductLocation = productLocation;
            plvm.SerialNumbers = productLocation.SerialNumbers;
            plvm.Vouchers = productLocation.Vouchers;

            ViewBag.ProductInGameID = ProductInGameID;
            return View(plvm);
        }

        // POST: ProductLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFromProductInGameMakeGame(ProductLocationViewModel plvm)
        {

            int GameID = db.ProductInGames.Find(plvm.ProductLocation.ProductInGameID).GameID;

            if (ModelState.IsValid)
            {
                db.Entry(plvm.ProductLocation).State = EntityState.Modified;

                foreach (var s in plvm.ProductLocation.SerialNumbers)
                {
                    

                    SerialNumberValidator validator = new SerialNumberValidator();

                    FluentValidation.Results.ValidationResult results = validator.Validate(s);
                    IList<ValidationFailure> failures = results.Errors;
                    StringBuilder sb = new StringBuilder();
                    foreach (var e in results.Errors)
                    {

                        ModelState.AddModelError(e.PropertyName + "Error", e.ErrorMessage);
                        sb.AppendLine(e.ErrorMessage);

                    }
                    if (ModelState.IsValid)
                    {
                        db.Entry(s).State = EntityState.Modified;
                    }
                    else
                    {
                        ViewBag.ProductInGameID = plvm.ProductLocation.ProductInGameID;
                        plvm.ProductLocation.ProductInGame = db.ProductInGames.Find(plvm.ProductLocation.ProductInGameID);
                        return View(plvm);
                    }
                }
                foreach (var v in plvm.ProductLocation.Vouchers)
                {

                    VoucherValidator validator = new VoucherValidator();

                    FluentValidation.Results.ValidationResult results = validator.Validate(v);
                    IList<ValidationFailure> failures = results.Errors;
                    StringBuilder sb = new StringBuilder();
                    foreach (var e in results.Errors)
                    {

                        ModelState.AddModelError(e.PropertyName + "Error", e.ErrorMessage);
                        sb.AppendLine(e.ErrorMessage);

                    }


                    if (ModelState.IsValid)
                    {
                        db.Entry(v).State = EntityState.Modified;
                    }
                    else
                    {
                        ViewBag.ProductInGameID = plvm.ProductLocation.ProductInGameID;
                        plvm.ProductLocation.ProductInGame = db.ProductInGames.Find(plvm.ProductLocation.ProductInGameID);
                        return View(plvm);
                    }
                
                }
                
                db.SaveChanges();
                return RedirectToAction("EditMakeGame", "Games", new { id = GameID });
            }
            ViewBag.ProductInGameID = plvm.ProductLocation.ProductInGameID;
            return View(plvm);
        }



        // GET: ProductLocations/Edit/5
        public ActionResult EditFromProductInGame(int? id, int ProductInGameID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductLocation productLocation = db.ProductLocations.Include(x=>x.SerialNumbers).Include(x=>x.Vouchers).Where(x=>x.ProductLocationID == id).First();

            if (productLocation == null)
            {
                return HttpNotFound();
            }
            ProductLocationViewModel plvm = new ProductLocationViewModel();
            plvm.ProductLocation = productLocation;
            plvm.SerialNumbers = productLocation.SerialNumbers;
            plvm.Vouchers = productLocation.Vouchers;

            ViewBag.ProductInGameID = ProductInGameID;
            return View(plvm);
        }

        // POST: ProductLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFromProductInGame (ProductLocationViewModel plvm)
        {
            StringBuilder sb = new StringBuilder();

            if (ModelState.IsValid)
            {
                db.Entry(plvm.ProductLocation).State = EntityState.Modified;

                foreach (var s in plvm.ProductLocation.SerialNumbers)
                {

                    SerialNumberValidator validator = new SerialNumberValidator();

                    FluentValidation.Results.ValidationResult results = validator.Validate(s);
                    IList<ValidationFailure> failures = results.Errors;
                    
                    foreach (var e in results.Errors)
                    {

                        ModelState.AddModelError(e.PropertyName + "Error", e.ErrorMessage);
                        sb.AppendLine(e.ErrorMessage);

                    }


                    db.Entry(s).State = EntityState.Modified;
                }
                foreach (var v in plvm.ProductLocation.Vouchers)
                {
                    db.Entry(v).State = EntityState.Modified;
                
                }

                if (ModelState.IsValid)
                {
                    db.SaveChanges();
                    return RedirectToAction("Edit", "ProductInGames", new { id = plvm.ProductLocation.ProductInGameID });
                }
                else
                {
                    ViewBag.ProductInGameID = plvm.ProductLocation.ProductInGameID;
                    ViewBag.ErrorMessage = sb.ToString();
                    plvm.ProductLocation.ProductInGame = db.ProductInGames.Find(plvm.ProductLocation.ProductInGameID);
                    return View(plvm);
                }
            }
            ViewBag.ErrorMessage = sb.ToString();
            ViewBag.ProductInGameID = plvm.ProductLocation.ProductInGameID;
            plvm.ProductLocation.ProductInGame = db.ProductInGames.Find(plvm.ProductLocation.ProductInGameID);
            return View(plvm);
        }


        // GET: ProductLocations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductLocation productLocation = db.ProductLocations.Find(id);
            if (productLocation == null)
            {
                return HttpNotFound();
            }
            return View(productLocation);
        }

        // POST: ProductLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductLocation productLocation = db.ProductLocations.Find(id);
            db.ProductLocations.Remove(productLocation);
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
