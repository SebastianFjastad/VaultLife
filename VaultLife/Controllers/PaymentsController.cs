using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vaultlife.ViewModels;
using Vaultlife.Models;
using System.Net;
using Vaultlife.Dao;

namespace Vaultlife.Controllers
{
    public class PaymentsController : Controller
    {

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pay([Bind(Include = "MembershipSubscriptionType,PaymentsModel")] UpgradePaymentViewModel paymentViewModel)
        {
            Vaultlife.Service.PaymentService paymentService = new Vaultlife.Service.PaymentService(new VaultLifeApplicationEntities());
            string custIp = Request.ServerVariables["REMOTE_ADDR"];
            bool success = paymentService.pay(paymentViewModel.PaymentsModel, paymentViewModel.MembershipSubscriptionType, User.Identity.Name, this.GetIPAddress(), custIp);
            if (success)
            {
                return Redirect("/?l=" + paymentViewModel.MembershipSubscriptionType); 
            }
            else
            {
                ModelState.AddModelError("ErrorMessage", "There was an error processing your payment");

                // MemberSubscriptionTypeCode and Amount not included in POST, use MembershipSubscriptionType to do lookup and populate paymentViewModel
                VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();
                MembershipSubscriptionTypeDao dao = new MembershipSubscriptionTypeDao(db);
                MemberSubscriptionType mst = dao.findAll().Where(t => t.MemberSubscriptionTypeID == paymentViewModel.MembershipSubscriptionType).First();
                paymentViewModel.amount = mst.amount.ToString("#######"); ;
                paymentViewModel.MembershipSubscriptionCode = mst.MemberSubscriptionTypeCode;

                return View("Pay", paymentViewModel); 
            }
        }

        [HttpGet]
        public ActionResult Pay([Bind(Include = "type")] int? type)
        {
            int ty = type == null ? 1 : (int)type;
            UpgradePaymentViewModel model = new UpgradePaymentViewModel();
            VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();
            MembershipSubscriptionTypeDao dao = new MembershipSubscriptionTypeDao(db);
            MemberSubscriptionType mst =  dao.findAll().Where(t => t.MemberSubscriptionTypeID == ty).First();
            model.MembershipSubscriptionType = ty;
            model.amount = mst.amount.ToString("#######"); ;
            model.MembershipSubscriptionCode = mst.MemberSubscriptionTypeCode;
            return View("Pay", model );

        }

        private string GetIPAddress()
        {
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            return ipAddress.ToString();
        }

	}
}