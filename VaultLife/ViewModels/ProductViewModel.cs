using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Models;

namespace Vaultlife.ViewModels
{
    public class ProductViewModel
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities(); 

        public Models.Product Product { get; set; }
        public IEnumerable<Models.Imagedetail> images { get; set; }

        public int LoggedInMemberID { get; set; }
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
        public ProductViewModel(int MemberID)
        {
            this.LoggedInMemberID = MemberID;
        }

        public ProductViewModel()
        {
         
        }

    }
}