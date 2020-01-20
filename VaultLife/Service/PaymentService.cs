using PaymentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Managers;
using Vaultlife.Models;
using Vaultlife.Dao;
using log4net;


namespace Vaultlife.Service
{
    public class PaymentService
    {
        private VaultLifeApplicationEntities db;
        private MemberDao memberDao;
        private MembershipSubscriptionTypeDao subscriptionTypeDao;
        private ILog log = LogManager.GetLogger(typeof(PaymentService));

        public PaymentService(VaultLifeApplicationEntities dbEntities)
        {
            this.db = dbEntities;
            this.memberDao = new MemberDao(db);
            this.subscriptionTypeDao = new MembershipSubscriptionTypeDao(db);
        }


        public bool pay(PaymentsModel model, int membershipSubscriptionStatus, string username, string ipAddress, String custIp)
        {
            Member member = memberDao.findMember(username);
            double amount = subscriptionTypeDao.findAmount(membershipSubscriptionStatus) - Convert.ToDouble(member.MemberSubscriptionType.amount);
            SetcomPaymentTransactionManager PayMan = new SetcomPaymentTransactionManager();

            PurchaseTransactionRequest purchaseTransactionRequest = new PurchaseTransactionRequest();

            purchaseTransactionRequest.CCNumber = model.CardNumber;
            purchaseTransactionRequest.CCCVV = model.CVCNumber;
            purchaseTransactionRequest.ExYear = (model.ExpiryDateY.Trim().Length > 2) ? model.ExpiryDateY : "20" + model.ExpiryDateY;
            purchaseTransactionRequest.ExMonth = model.ExpiryDateM;
            purchaseTransactionRequest.CCName = HttpUtility.UrlEncode(model.NameOnCard); 
            purchaseTransactionRequest.MemberInGameID = null;
            purchaseTransactionRequest.buyer_id = "";
            purchaseTransactionRequest.bill_first_name =  HttpUtility.UrlEncode(member.FirstName);
            purchaseTransactionRequest.bill_last_name =  HttpUtility.UrlEncode(member.LastName);
            purchaseTransactionRequest.bill_street1 = "";
            purchaseTransactionRequest.bill_street2 = "";
            purchaseTransactionRequest.bill_city = "";
            purchaseTransactionRequest.bill_state = "";
            purchaseTransactionRequest.bill_country = "";
            purchaseTransactionRequest.bill_zip = "";
            purchaseTransactionRequest.EmailAddress =  HttpUtility.UrlEncode(member.EmailAddress);
            purchaseTransactionRequest.CC_Amount = amount.ToString();
            purchaseTransactionRequest.ip_address = custIp;
            purchaseTransactionRequest.transactionDateTime = DateTime.Now;

            PurchaseTransactionResponse ptRes = PayMan.PerformPaymentTransaction(purchaseTransactionRequest);

            if (ptRes.outcome.ToUpper() == "APPROVED")
            {
                member.MemberSubscriptionTypeID = membershipSubscriptionStatus;
                memberDao.save(member);
                this.sendUpgradeEmail(member.FirstName + ' ' + member.LastName, member.EmailAddress);
                return true;
            }
            log.Info("PaymentService: error processing payment: " + ptRes.outcome + ", " + ptRes.responseIndicator);
            return false;

        }

        private void sendUpgradeEmail(String recipientName, String email)
        {
            EmailManager emailManager = new EmailManager();
            emailManager.NewEmail();
            emailManager.RecipientEmailAddress = email;
            emailManager.RecipientName = recipientName;
            emailManager.EmailSubject = "VAULTLife.com - Upgrade Successful!";
            emailManager.EmailBodyText = "Congratulations! You have taken a bold step closer to crossing those dreams and experiances off your bucket list! By upgrading, you will have far more items available to choose from, and better yet, they will cost you even less! The world is at your feet and there is no stopping you now!";
            emailManager.QueueEmail();
        }
    }
}