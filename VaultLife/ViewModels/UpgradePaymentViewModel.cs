using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vaultlife.ViewModels
{
    public class UpgradePaymentViewModel
    {
        public Models.PaymentsModel PaymentsModel { get; set; }

        public int MembershipSubscriptionType { get; set; }

        public String MembershipSubscriptionCode { get; set; }

        public String amount { get; set; }

    }
}