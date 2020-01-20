using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Models;
using System.Data.Entity;

namespace Vaultlife.ViewModels
{
    public class MyVaultItemViewModel
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities(); 

        public Models.Product Product { get; set; }
        public Models.Game Game { get; set; }

        public Models.ProductInGame CurrentProductInGame { get; set; }


        public DateTime GameScheduleStart { get; set; }

        public string PageSection { get; set; }
        public IEnumerable<Models.Imagedetail> images { get; set; }

        public int LoggedInMemberID { get; set; }
        public string GameScheduleStartJquery { get; set; }
        public bool IsInMyWatchList
        {
            get
            {
                var piw = db.ProductInWatchLists.Where(x => x.MemberID == this.LoggedInMemberID && x.ProductID == this.Product.ProductID && (x.IsExpired == null || x.IsExpired != true));
                if (piw != null && piw.Count() > 0)
                {
                    return true;
                }
                return false;
            }



        }
        public MyVaultItemViewModel(int MemberID, int GameID, int ProductID)
        {
            this.LoggedInMemberID = MemberID;
            this.Product = db.Products.Include(x=>x.Imagedetails).Where(x=>x.ProductID == ProductID).First();
            this.Game = db.Games.Include(x=>x.GameRules).Where(x => x.GameID == GameID).FirstOrDefault();
            this.CurrentProductInGame = db.ProductInGames.Where(x => x.GameID == GameID).First();
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
            long Minutes = Seconds / 60;
            Seconds = Seconds % 60;
            long Hours = Minutes / 60;
            Minutes = Minutes % 60;


            //+12h36m12s
            return (String.Format("+{0}h{1}m{2}s", Hours.ToString(), Minutes.ToString(), Seconds.ToString()));
        }

    
    }
}


           
        
        
       