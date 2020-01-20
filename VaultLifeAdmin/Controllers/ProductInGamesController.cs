using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VaultLifeAdmin.Models;
using VaultLifeAdmin.ViewModels;

namespace VaultLifeAdmin.Controllers
{
    public class ProductInGamesController : Controller
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();

        // GET: ProductInGames
        public ActionResult Index()
        {
            var productInGames = db.ProductInGames.Include(p => p.Game).Include(p => p.Product);
            return View(productInGames.ToList());
        }



        // GET: ProductInGames/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductInGame productInGame = db.ProductInGames.Find(id);
            if (productInGame == null)
            {
                return HttpNotFound();
            }
            return View(productInGame);
        }

        // GET: ProductInGames/Create
        public ActionResult Create()
        {
            ViewBag.CurrencyID = new SelectList(db.Currencies, "CurrencyID", "CurrencyCode");
            ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductSKUCode");


            ProductInGame model = new ProductInGame();
            model = (ProductInGame)Helpers.TableTracker.TrackCreate(model, "USR");
            return View(model);
        }

        // POST: ProductInGames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductInGameID,ProductID, PriceInGame,GameID,Quantity,CurrencyID,DateInserted,DateUpdated,USR")] ProductInGame productInGame)
        {

              ProductInGameValidator validator = new ProductInGameValidator();

            if (ModelState.IsValid)
            {

                //Deduct quantity from Product table if stock available
                //@Patrick 6) Depletes the product Quantity from the "Available Stock" amount on the Product record. 
                //Available Stock must not go negative (Validator checks for this)
                //Get Product
                var product = db.Products.Where(x => x.ProductID == productInGame.ProductID).First();
                //Deplete available stock on hand
                product.AvailableSOH -= productInGame.Quantity;


                //Save
                if (product.AvailableSOH >= 0)
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("QuantityError", "Not enough stock on hand for this product");
                    ViewBag.CurrencyID = new SelectList(db.Currencies, "CurrencyID", "CurrencyCode", productInGame.CurrencyID);
                    ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode", productInGame.GameID);
                    ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductSKUCode", productInGame.ProductID);
                    return View(productInGame);
                }


                db.ProductInGames.Add(productInGame);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = productInGame.ProductInGameID});
            }

            FluentValidation.Results.ValidationResult results = validator.Validate(productInGame);
            IList<ValidationFailure> failures = results.Errors;
            StringBuilder sb = new StringBuilder();
            foreach (var e in results.Errors)
            {

                ModelState.AddModelError(e.PropertyName + "Error", e.ErrorMessage);
                sb.AppendLine(e.ErrorMessage);

            }

            ViewBag.CurrencyID = new SelectList(db.Currencies, "CurrencyID", "CurrencyCode", productInGame.CurrencyID);
            ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode", productInGame.GameID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductSKUCode", productInGame.ProductID);
            return View(productInGame);
        }


        // GET: ProductInGames/Create
        public ActionResult CreateForGame(int gameID)
        {
            ViewBag.Game = db.Games.Find(gameID);

            ViewBag.GameID = gameID;

            ViewBag.CurrencyID = new SelectList(db.Currencies, "CurrencyID", "CurrencyCode");
           
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductSKUCode");
            return View();
        }

        // POST: ProductInGames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateForGame([Bind(Include = "ProductInGameID,ProductID, ReferencePrice, PriceInGame,GameID,Quantity,CurrencyID,DateInserted,DateUpdated,USR")] ProductInGame productInGame)
        {

            ProductInGameValidator validator = new ProductInGameValidator();

            if (ModelState.IsValid)
            {

                //Deduct quantity from Product table if stock available
                //@Patrick 6) Depletes the product Quantity from the "Available Stock" amount on the Product record. 
                //Available Stock must not go negative (Validator checks for this)
                //Get Product
                var product = db.Products.Where(x => x.ProductID == productInGame.ProductID).First();
                //Deplete available stock on hand
                product.AvailableSOH -= productInGame.Quantity;


                //Save
                if (product.AvailableSOH >= 0)
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("QuantityError", "Not enough stock on hand for this product");
                    ViewBag.CurrencyID = new SelectList(db.Currencies, "CurrencyID", "CurrencyCode", productInGame.CurrencyID);
                    ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode", productInGame.GameID);
                    ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductSKUCode", productInGame.ProductID);
                    return View(productInGame);
                }


                db.ProductInGames.Add(productInGame);
                db.SaveChanges();
                

                return RedirectToAction("EditMakeGame", "Games", new { id = productInGame.GameID });
            }

            FluentValidation.Results.ValidationResult results = validator.Validate(productInGame);
            IList<ValidationFailure> failures = results.Errors;
            StringBuilder sb = new StringBuilder();
            foreach (var e in results.Errors)
            {

                ModelState.AddModelError(e.PropertyName + "Error", e.ErrorMessage);
                sb.AppendLine(e.ErrorMessage);

            }

            ViewBag.CurrencyID = new SelectList(db.Currencies, "CurrencyID", "CurrencyCode", productInGame.CurrencyID);
            ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode", productInGame.GameID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductSKUCode", productInGame.ProductID);
            return View(productInGame);
        }

        
        // GET: ProductInGames/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            ProductInGame model = db.ProductInGames.Include(x => x.ProductLocations).Where(x => x.ProductInGameID == id).First();
            model = (ProductInGame)Helpers.TableTracker.TrackEdit(model, "USR");


            if (model == null)
            {
                return HttpNotFound();
            }

            ProductInGameViewModel productInGameViewModel = new ProductInGameViewModel { ProductInGame = model, CurrentQuantity = model.Quantity, ProductLocations = model.ProductLocations};

            ViewBag.ProductOwnerID = new SelectList(db.Owners, "OwnerID", "OwnerCode");
            ViewBag.CurrencyID = new SelectList(db.Currencies, "CurrencyID", "CurrencyCode", model.CurrencyID);
            ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode", model.GameID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductSKUCode", model.ProductID);
            return View(productInGameViewModel);
        }

        // POST: ProductInGames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductInGameViewModel productInGameViewModel)
        {

            ProductInGameValidator validator = new ProductInGameValidator();


            if (ModelState.IsValid && validator.Validate(productInGameViewModel.ProductInGame).IsValid)
            {

                //Deduct quantity from Product table if stock available
                //@Patrick 6) Depletes the product Quantity from the "Available Stock" amount on the Product record. 
                //Available Stock must not go negative (Validator checks for this)
                //Get Product
                var product = db.Products.Where(x => x.ProductID == productInGameViewModel.ProductInGame.ProductID).First();
                //"return" current quantity
                product.AvailableSOH += productInGameViewModel.CurrentQuantity;
                //Deplete available stock on hand with new quantity
                product.AvailableSOH -= productInGameViewModel.ProductInGame.Quantity;

                
                //Save
                if (product.AvailableSOH >= 0 && productInGameViewModel.ProductInGame.Quantity >=0)
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    if (product.AvailableSOH < 0)
                    {
                        ModelState.AddModelError("QuantityError", "Not enough stock on hand for this product");
                    }
                    if (productInGameViewModel.ProductInGame.Quantity < 0)
                    {
                            ModelState.AddModelError("QuantityError", "Quantity cannot be less than 0");
                    }
                    ViewBag.ProductOwnerID = new SelectList(db.Owners, "OwnerID", "OwnerCode", productInGameViewModel.ProductOwnerID);
                    ViewBag.CurrencyID = new SelectList(db.Currencies, "CurrencyID", "CurrencyCode", productInGameViewModel.ProductInGame.CurrencyID);
                    ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode", productInGameViewModel.ProductInGame.GameID);
                    ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductSKUCode", productInGameViewModel.ProductInGame.ProductID);
                    return View(productInGameViewModel);
                }

                if (productInGameViewModel.ProductLocations != null)
                {
                    foreach (var d in productInGameViewModel.ProductLocations)
                    {

                        db.Entry(d).State = EntityState.Modified;

                    }
                }

                db.Entry(productInGameViewModel.ProductInGame).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            FluentValidation.Results.ValidationResult results = validator.Validate(productInGameViewModel.ProductInGame);
            IList<ValidationFailure> failures = results.Errors;
            StringBuilder sb = new StringBuilder();
            foreach (var e in results.Errors)
            {

                ModelState.AddModelError(e.PropertyName + "Error", e.ErrorMessage);
                sb.AppendLine(e.ErrorMessage);

            }
            ViewBag.ProductOwnerID = new SelectList(db.Owners, "OwnerID", "OwnerCode", productInGameViewModel.ProductOwnerID);
            ViewBag.CurrencyID = new SelectList(db.Currencies, "CurrencyID", "CurrencyCode", productInGameViewModel.ProductInGame.CurrencyID);
            ViewBag.GameID = new SelectList(db.Games, "GameID", "GameCode", productInGameViewModel.ProductInGame.GameID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductSKUCode", productInGameViewModel.ProductInGame.ProductID);
            return View(productInGameViewModel);
        }


        

        // GET: ProductInGames/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductInGame productInGame = db.ProductInGames.Find(id);
            if (productInGame == null)
            {
                return HttpNotFound();
            }
            return View(productInGame);
        }

        // POST: ProductInGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductInGame productInGame = db.ProductInGames.Find(id);
            db.ProductInGames.Remove(productInGame);
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
