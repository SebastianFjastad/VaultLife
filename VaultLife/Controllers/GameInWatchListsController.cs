using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vaultlife.Helpers;
using Vaultlife.Models;
using Vaultlife.ViewModels;
using Vaultlife.Service;

namespace Vaultlife.Controllers
{
    [Authorize]
    public class GameInWatchListsController : Controller
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();

        // GET: ProductInWatchLists
        public ActionResult Index()
        {
            var productInWatchLists = db.ProductInWatchLists.Include(p => p.Member).Include(p => p.Product);
           
            return View(productInWatchLists.ToList());
        }

        // GET: ProductInWatchLists
        public ActionResult MyWatchlist()
        {
            //Security to only allow logged in user to see his own watchlist
            AccountService service = new AccountService(db, null);
            Member LoggedInMember = service.findMember(User.Identity.Name);

            //Only return watchlist for logged in user
            var productInWatchLists = db.ProductInWatchLists.Include(p => p.Member).Include(p => p.Product).Where(x => x.MemberID == LoggedInMember.MemberID && (x.IsExpired == null || x.IsExpired == false));

            List<ProductViewModel> pvmList = new List<ProductViewModel>();
            foreach (var piew in productInWatchLists)
            {
                
                ProductViewModel pvm = new ProductViewModel(service.findMember(User.Identity.Name).MemberID);
                pvm.Product = piew.Product;
                pvm.images = pvm.Product.Imagedetails;
                pvmList.Add(pvm);
            }

            return View(pvmList);
        }

        public ActionResult MyVaultItems()
        {
            AccountService service = new AccountService(db, null);
            ////Security to only allow logged in user to see his own watchlist
            //var LoggedInMember = MemberHelper.GetLoggedInMember(User.Identity.Name);

            ////Only return watchlist for logged in user
            //var productInWatchLists = db.ProductInWatchLists.Include(p => p.Member).Include(p => p.Product).Where(x => x.MemberID == LoggedInMember.MemberID && (x.IsExpired == null || x.IsExpired == false));

            //List<ProductViewModel> pvmList = new List<ProductViewModel>();
            //foreach (var piw in productInWatchLists)
            //{
            //    ProductViewModel pvm = new ProductViewModel(MemberHelper.GetLoggedInMember(User.Identity.Name).MemberID);
            //    pvm.Product = piw.Product;
            //    pvmList.Add(pvm);
                              
                
                
            //}
            List<MyVaultItemViewModel> myVaultItems = new List<MyVaultItemViewModel>();

            if (Request.IsAuthenticated)
            {
                int LoggedInMemberID = service.findMember(User.Identity.Name).MemberID;

                //2. Get Top Section

                IQueryable<ProductInWatchList> GameInWatchlistList = db.ProductInWatchLists.Include(x => x.Game).Where(x => x.MemberID == LoggedInMemberID && x.IsExpired != true && ((x.Game.GameState.ToLower() == "prepare" || x.Game.GameState.ToLower() == "active" || x.Game.GameState.ToLower() == "released")));
               foreach(ProductInWatchList game in GameInWatchlistList) {
                    MyVaultItemViewModel myVaultItem = new MyVaultItemViewModel(LoggedInMemberID, game.GameID, game.Game.ProductInGames.First().ProductID);
                   
                    myVaultItems.Add(myVaultItem);

               } 
               /* var productInExtendedWatchLists = db.ExtendedProductInWatchLists.Where(x => x.MemberID == LoggedInMemberID && (x.IsExpired == null || x.IsExpired == false) && (x.GameState.ToLower() == "prepare" || x.GameState.ToLower() == "active" || x.GameState.ToLower() == "released")).OrderBy(x => x.ExcecuteTime);
                foreach (ExtendedProductInWatchList piewl in productInExtendedWatchLists)
                {
                    MyVaultItemViewModel myVaultItem = new MyVaultItemViewModel(LoggedInMemberID, piewl.GameID, piewl.ProductID);
                    myVaultItems.Add(myVaultItem);
                }*/
                
             }
                
            return View(myVaultItems.OrderBy(x=>x.GameScheduleStart));
          }

        // GET: ProductInWatchLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductInWatchList productInWatchList = db.ProductInWatchLists.Find(id);
            if (productInWatchList == null)
            {
                return HttpNotFound();
            }
            return View(productInWatchList);
        }

        // GET: ProductInWatchLists/Create
        public ActionResult Create()
        {
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "IdentityType");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductSKUCode");
            ProductInWatchList model = new ProductInWatchList();
            model = (ProductInWatchList)Helpers.TableTracker.TrackCreate(model, User.Identity.Name);
            return View(model);
        }

        // POST: ProductInWatchLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductInWatchListID,MemberID,ProductID,DateInserted,DateUpdated,USR")] ProductInWatchList productInWatchList)
        {
            if (ModelState.IsValid)
            {
                db.ProductInWatchLists.Add(productInWatchList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "IdentityType", productInWatchList.MemberID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductSKUCode", productInWatchList.ProductID);
            return View(productInWatchList);
        }

        // GET: ProductInWatchLists/Create
        public ActionResult AddToWatchList(int GameID, string FromWhere = "")
        {
            AccountService service = new AccountService(db, null);
            var member = service.findMember(User.Identity.Name);
            var product = db.ProductInGames.Where(x => x.GameID == GameID).First(); 
            ProductInWatchList piw = new ProductInWatchList();
            piw.MemberID = member.MemberID;
            piw.GameID = GameID;
            piw.ProductID = product.ProductID;
            piw.DateInserted = DateTime.Now;
            piw.USR = User.Identity.Name;
            
            db.ProductInWatchLists.Add(piw);
            db.SaveChanges();

            switch (FromWhere.ToLower())
            {
                case "detail": return RedirectToAction("DetailsComingSoon", "Product", new { id = GameID });

                case "comingsoon": return new RedirectResult(Url.Action("Index", "Home") + "#browse-items");

                default: return RedirectToAction("DetailsComingSoon", "Product", new { id = GameID }); ;
            }
            return RedirectToAction("MyWatchList", "ProductInWatchLists");
        }



        public ActionResult RemoveFromWatchList(int GameID, string FromWhere = "")
        {

            AccountService service = new AccountService(db, null);
            var member = service.findMember(User.Identity.Name);

            int MemberID = member.MemberID;

            var productInWatchLists = db.ProductInWatchLists.Where(x => x.GameID == GameID && x.MemberID == MemberID);

            if (productInWatchLists != null && productInWatchLists.Count() > 0)
            {
                db.ProductInWatchLists.Remove(productInWatchLists.First());
                db.SaveChanges();
            }
            switch (FromWhere.ToLower())
            {
                case "detail": return RedirectToAction("DetailsComingSoon", "Product", new { id = GameID });

                case "comingsoon": return new RedirectResult(Url.Action("Index", "Home") + "#browse-items");

                case "watchlist": return RedirectToAction("MyWatchList", "ProductInWatchLists");

                default: return RedirectToAction("DetailsComingSoon", "Product", new { id = GameID }); ;
            }
            return RedirectToAction("MyWatchList", "ProductInWatchLists");


        }


        // GET: ProductInWatchLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductInWatchList model = db.ProductInWatchLists.Find(id);

            model = (ProductInWatchList)Helpers.TableTracker.TrackEdit(model, User.Identity.Name);


            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "IdentityType", model.MemberID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductSKUCode", model.ProductID);
            return View(model);
        }

        // POST: ProductInWatchLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductInWatchListID,MemberID,ProductID,DateInserted,DateUpdated,USR")] ProductInWatchList productInWatchList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productInWatchList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "IdentityType", productInWatchList.MemberID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductSKUCode", productInWatchList.ProductID);
            return View(productInWatchList);
        }

        // GET: ProductInWatchLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductInWatchList productInWatchList = db.ProductInWatchLists.Find(id);
            if (productInWatchList == null)
            {
                return HttpNotFound();
            }
            return View(productInWatchList);
        }

        // POST: ProductInWatchLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductInWatchList productInWatchList = db.ProductInWatchLists.Find(id);
            db.ProductInWatchLists.Remove(productInWatchList);
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
