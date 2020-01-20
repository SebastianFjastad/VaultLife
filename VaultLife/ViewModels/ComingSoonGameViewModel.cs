using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Models;

namespace Vaultlife.ViewModels
{
    public class ComingSoonGameViewModel
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();
        public ProductInGame ProductInGame { get; set; }
        public Imagedetail  mainImage { get; set; }

        public Game Game { get; set; }
        
        public int? LoggedInMemberID { get; set; }
        public DateTime GameScheduleStart { get; set; }

        public string GameScheduleStartJquery { get; set; }
         
        //TODO Move this out 
        public bool IsInMyWatchList
        {
            get
            {
                var piw = db.ProductInWatchLists.Where(x => x.MemberID == this.LoggedInMemberID && x.ProductID == this.ProductInGame.ProductID && x.GameID == this.Game.GameID && (x.IsExpired == null || x.IsExpired != true));
                if (piw != null && piw.Count() > 0)
                {
                    return true;
                }
                return false;
            }

        }

        public ComingSoonGameViewModel(int GameId)
        {
            
            this.Game = db.Games.Find(GameId);
           
            if (this.Game.GameRules.Where(x => x.GameRuleCode.ToLower() == "startgame").Count() > 0)
            {
                this.GameScheduleStart = this.Game.GameRules.Where(x => x.GameRuleCode.ToLower() == "startgame").First().ExcecuteTime.AddMinutes(-5);
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
    
    
    }
}