using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vaultlife.Models;
using Quartz;
using Quartz.Impl;
using System.Data.SqlClient;
using System.Globalization;
using Vaultlife.Models.Games;
using Microsoft.AspNet.Identity;
using Vaultlife.ViewModels;
using FluentValidation.Results;
using System.Text;
using Vaultlife.Service;
using Vaultlife.Dao;


using Vaultlife.Helpers;

namespace Vaultlife.Controllers
{
    public class HomeController : Controller
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();
        public ActionResult Index1()
        {
            EmailHelper eh = new EmailHelper();

            eh.MailSubject = "Test VaultLife Subject";
            eh.MailBody = "Test VaultLife Body";
            eh.AddToAddress("dsouchon@gmail.com");
            eh.SendMail();

            return View();
        }

        public ActionResult Index()//ComingSoon()
        {
            int MemberId = 0;
            AccountService service = new AccountService(db, null);
            HomeAuthenticatedViewModel mainModel = new HomeAuthenticatedViewModel();
            if (Request.IsAuthenticated)
            {
                MemberId = service.findMember(User.Identity.Name).MemberID;
                List<MyVaultItemViewModel> topSectionList = FindTopSectionList(MemberId);

                mainModel.TopSection = topSectionList.OrderBy(x => x.GameScheduleStart).Take(10);
                //3.Get Upcoming Vault Items

                List<MyVaultItemViewModel> upcomingVaultItemList = new List<MyVaultItemViewModel>();
                //TODO : Get Vault Items
                mainModel.UpcomingVaultItems = topSectionList.OrderBy(x => x.GameScheduleStart).Take(10);

            }
            GameService gameService = new GameService(db);
            List<ComingSoonViewModel> ComingSoonList = ToCommingSoon(gameService.FindComingSoonGames(MemberId));
            if (ComingSoonList.All(c => c.ComingSoonGameVM.Count() == 0) && MemberId != 0)
            {
                ComingSoonList = ToCommingSoon(gameService.findGlobalGames(MemberId));
            }
            mainModel.ComingSoon = ComingSoonList;
            GameDao gd = new GameDao(db);
            List<dynamic> recentSales = gd.findRecentWinners();
            mainModel.recentWinners = recentSales;
            return View(mainModel);
        }


        private List<ComingSoonViewModel> ToCommingSoon(IEnumerable<Game> ComingSoonGames)
        {

            List<ComingSoonViewModel> ComingSoons = new List<ComingSoonViewModel>();
            ComingSoonViewModel csvFree = new ComingSoonViewModel();
            ComingSoonViewModel csvLovingLife = new ComingSoonViewModel();
            ComingSoonViewModel csvTycoon = new ComingSoonViewModel();
            ComingSoonViewModel csvLuxury = new ComingSoonViewModel();
            foreach (Game iGame in ComingSoonGames)
            {
                switch (iGame.MemberSubscriptionType1.MemberSubscriptionTypeCode.ToLower().Trim())
                {
                    case "free":
                        csvFree.MemberSubscriptionType = iGame.MemberSubscriptionType1;
                        csvFree.ComingSoonGameVM.Add(CreateComingSoonGameVM(iGame));
                        break;
                    case "loving-life":
                        csvLovingLife.MemberSubscriptionType = iGame.MemberSubscriptionType1;
                        csvLovingLife.ComingSoonGameVM.Add(CreateComingSoonGameVM(iGame));
                        break;
                    case "luxury":
                        csvLuxury.MemberSubscriptionType = iGame.MemberSubscriptionType1;
                        csvLuxury.ComingSoonGameVM.Add(CreateComingSoonGameVM(iGame));
                        break;
                    case "tycoon":
                        csvTycoon.MemberSubscriptionType = iGame.MemberSubscriptionType1;
                        csvTycoon.ComingSoonGameVM.Add(CreateComingSoonGameVM(iGame));
                        break;
                }
            }

            ComingSoons.Add(csvTycoon);
            ComingSoons.Add(csvLuxury);
            ComingSoons.Add(csvLovingLife);
            ComingSoons.Add(csvFree);

            return ComingSoons;
        }



        private ComingSoonGameViewModel CreateComingSoonGameVM(Game iGame)
        {
            AccountService service = new AccountService(db, null);
            ComingSoonGameViewModel ComingSoonGameVM = new ComingSoonGameViewModel(iGame.GameID);
            if (Request.IsAuthenticated)
            {
                ComingSoonGameVM.LoggedInMemberID = service.findMember(User.Identity.Name).MemberID;

            }
            ComingSoonGameVM.Game = iGame;
            ComingSoonGameVM.ProductInGame = iGame.ProductInGames.First();
           
            var product = db.Products.Include(x => x.Imagedetails).Where(x => x.ProductID == ComingSoonGameVM.ProductInGame.ProductID).First();
            if (product.Imagedetails.Count() == 0) {
                Imagedetail empty = new Imagedetail();
                empty.ImageName = "Main Image";
                empty.ImageID = 1;  ///TODO 1 is empty image
                product.Imagedetails.Add(empty);
               
            }
              //  ComingSoonGameVM.mainImage = product.Images.Where(c => c..ToLower() == "main image").Count() > 0 ?
               // ComingSoonGameVM.mainImage = product.Images.Where(c => c..ToLower() == "main image").First() :
            //TODO fix main image thing    
            ComingSoonGameVM.mainImage = product.Imagedetails.First();
            
                return ComingSoonGameVM;

        }


        public IQueryable<Game> FindComingSoonGames(int? MemberId)
        {
            if (MemberId > 0)
            {
                var comingSoonGames = db.Games.Include(x => x.ProductInGames).Where(x => (x.GameState.ToLower() == "prepare" || x.GameState.ToLower() == "released") && x.MemberInGames.Count(y => y.MemberID == MemberId) > 0);
                return comingSoonGames;
            }

            var comingSoonAllGames = db.Games.Include(x => x.ProductInGames).Where(x => (x.GameState.ToLower() == "prepare" || x.GameState.ToLower() == "released"));
            return comingSoonAllGames;
        }
        private List<MyVaultItemViewModel> FindTopSectionList(int LoggedInMemberID)
        {

            List<MyVaultItemViewModel> topSectionList = new List<MyVaultItemViewModel>();
           
            //2. Get Top Section
            IQueryable<ProductInWatchList> GameInWatchlistList = db.ProductInWatchLists.Include(x => x.Game).Where(x => x.MemberID == LoggedInMemberID && x.IsExpired != true && ((x.Game.GameState.ToLower() == "prepare" || x.Game.GameState.ToLower() == "active" || x.Game.GameState.ToLower() == "released")));
            foreach (ProductInWatchList game in GameInWatchlistList)
            {
                MyVaultItemViewModel myVaultItem = new MyVaultItemViewModel(LoggedInMemberID, game.GameID, game.Game.ProductInGames.First().ProductID);
                topSectionList.Add(myVaultItem);
            }
            return topSectionList;
        }


        public ActionResult About()
        {
            ViewBag.Message = "About Us Page.";
            return View();
        }


        public ActionResult HowToPlay()
        {
            ViewBag.Message = "How To Play Page.";

            return View();
        }

        public ActionResult WhatIsVaultlife()
        {
            return View();
        }

        public ActionResult MembershipOptions()
        {
            return View();
        }

        public ActionResult FAQs()
        {
            ViewBag.Message = "Frequently Asked Questions.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult TermsOfUse()
        {
            return View();
        }


        [Authorize]
        public ActionResult Loggedin()
        {
            AspNetUser user = db.AspNetUsers.Where(x => x.UserName == User.Identity.Name).First();
            Member member = db.Members.Where(m => m.ASPUserId == user.Id).First();
            var games = db.PlayableGamesByMembers.Where(g => g.MemberID == member.MemberID).OrderByDescending(g => g.GameID);

            return View(games.ToList());


        }

        public ActionResult DropZone()
        {
            return View();
        }
    }
}