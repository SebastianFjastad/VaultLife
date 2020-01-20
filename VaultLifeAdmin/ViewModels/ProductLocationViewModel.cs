using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaultLifeAdmin.ViewModels
{
    public class ProductLocationViewModel
    {

        

        public Models.ProductLocation ProductLocation { get; set; }
        public IEnumerable<Models.Voucher> Vouchers { get; set; }
        public IEnumerable<Models.SerialNumber> SerialNumbers { get; set; }
    }
}