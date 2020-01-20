using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Models;
using System.Data.Entity;
using Vaultlife.ViewModels;

namespace Vaultlife.ViewModels
{
    public class ProductDisplayViewModel
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities(); 
        public Product Product { get; set; }
        public Game game { get; set; }
 
        public DateTime GameScheduleStart{ get; set; }

        public string GameScheduleStartJquery { get; set; }

        //+12h36m12s
        public int LoggedInMemberID { get; set; }
        public MemberSubscriptionType MembershipSubscriptionTypeDescription { get; set; }
        public List<GameListViewModel> otherGames { get; set; }

        
        //Only display non-expired Product in Watchlist
        public bool IsInMyWatchList { 
            get
            {
                var piw = db.ProductInWatchLists.Where(x=>x.MemberID ==  this.LoggedInMemberID && x.GameID == this.game.GameID && (x.IsExpired == null || x.IsExpired != true));
                if (piw != null && piw.Count() > 0)
                {
                    return true;
                }
                return false;
            }
        
            
        
        }

        public ProductDisplayViewModel(int MemberID, int GameId)
        {
            this.LoggedInMemberID = MemberID;
            this.game = db.Games.Find(GameId);
            this.CurrentProductInGame = db.ProductInGames.Where(x => x.GameID == GameId && x.Game.GameState.ToLower() !="completed").First();
            this.Product = CurrentProductInGame.Product;
            this.MembershipSubscriptionTypeDescription = db.MemberSubscriptionTypes.Where(x => x.MemberSubscriptionTypeID == game.MemberSubscriptionType).First();
            if (this.game.GameRules.Where(x => x.GameRuleCode.ToLower() == "startgame").Count() > 0)
            {
                this.GameScheduleStart = this.game.GameRules.Where(x => x.GameRuleCode.ToLower() == "startgame").First().ExcecuteTime.AddMinutes(-5);
                if (this.GameScheduleStart <= DateTime.Now)
                {
                    this.GameScheduleStartJquery = "0";
                }
                else
                {
                    this.GameScheduleStartJquery = ConvertToJquery(this.GameScheduleStart);
                }
            }
        }

        private string ConvertToJquery(DateTime dateTime)
        {
            TimeSpan span = dateTime - DateTime.Now;
            long Seconds = (long)span.TotalSeconds;
            long Minutes = Seconds/60;
            Seconds = Seconds % 60;
            long Hours = Minutes/60;
            Minutes = Minutes % 60;
                                    
            
            //+12h36m12s
            return (String.Format("+{0}h{1}m{2}s", Hours.ToString(), Minutes.ToString(), Seconds.ToString()));
        }
        public IEnumerable<Models.Imagedetail> images { get; set; }
        public ProductInGame CurrentProductInGame { get; set; }


        
        
        //TODO : Can be removed.
        private DateTime GetGameStart(ProductInGame CurrentProductInGame)
        {
            var gameRule = db.GameRules.Where(gr => gr.GameID == CurrentProductInGame.GameID && gr.GameRuleCode.ToLower() == "startgame");
            return gameRule.First().ExcecuteTime;
            
        }

      


    }
}