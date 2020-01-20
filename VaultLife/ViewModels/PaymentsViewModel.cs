using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vaultlife.ViewModels
{
    public class PaymentsViewModel
    {
        public Models.PaymentsModel PaymentsModel { get; set; }

        public Vaultlife.Models.Game Game { get; set; }

        public Vaultlife.Models.Product Product { get; set; }
    }
}