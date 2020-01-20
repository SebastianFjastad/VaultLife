using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaultLifeAdmin.Models;

namespace VaultLifeAdmin.ViewModels
{
    public class ProductViewModel
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities(); 

        public Models.Product Product { get; set; }
        public IEnumerable<ImageViewModel> Images { get; set; }

        public ProductViewModel()
        {
         
        }

    }
}