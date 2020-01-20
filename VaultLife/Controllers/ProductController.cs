using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vaultlife.Helpers;
using Vaultlife.Models;
using Vaultlife.ViewModels;
using Vaultlife.Service;
using Vaultlife.Dao;

namespace Vaultlife.Controllers
{
    public class ProductController : Controller
    {

        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();
        public ActionResult DetailsComingSoon(int id)
        {
            AccountService service = new AccountService(db, null);
            int MemberID = 0;

            if (Request.IsAuthenticated)
            {
                MemberID = service.findMember(User.Identity.Name).MemberID;
            }

            ProductInGame pigs = db.ProductInGames.Where(x => x.GameID == id).First();
            if (pigs.Game.GameState.ToLower() == "completed")
            {
                return Redirect("/");
            }
            ProductDisplayViewModel pdvm = new ProductDisplayViewModel(MemberID, pigs.GameID);
            pdvm.Product = pigs.Product;
            pdvm.images = pigs.Product.Imagedetails.Where(w=>w.ImageTypeID==3);
           

            //TODO: DS: What are therules for which p.i.g. to select/display here
             GameDao gd = new GameDao(db);
             List<GameListViewModel> otherGames = gd.getOtherGames(MemberID, pigs.Game.MemberSubscriptionType);
            otherGames.RemoveAll(g => g.gameID == id);    
            pdvm.CurrentProductInGame = pigs;
            pdvm.otherGames = otherGames;
            
            return View(pdvm);
        }


         
    }
}