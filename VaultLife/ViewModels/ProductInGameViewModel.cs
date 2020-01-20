using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vaultlife.Models;

namespace Vaultlife.ViewModels
{
    public class ProductInGameViewModel
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities(); 
        public int ProductOwnerID { get; set; }
        public Models.ProductInGame ProductInGame { get; set; }
        public int CurrentQuantity { get; set; }//Quantity currently issued

        
        [Display(Name = "Product")]
        public int SelectedProductID { get; set; }

        public IEnumerable<SelectListItem> Products
        {
            get { return new SelectList(db.Products, "ProductID", "ProductName"); }
        }

        public IEnumerable<Models.ProductLocation> ProductLocations { get; set; }
    }
}