using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Models;
using Vaultlife.Dao;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Vaultlife.Helpers;
using System.Web.Mvc;
using Vaultlife.Managers;


namespace Vaultlife.Service
{
    public class AccountService
    {
        private VaultLifeApplicationEntities db;
        private MemberDao memberDao;
        private MembershipSubscriptionTypeDao memberSubTypeDao;
        private ApplicationUserManager userManager;

        //TODO what does applicationUserManager do ?
        public AccountService(VaultLifeApplicationEntities db, ApplicationUserManager userManager)
        {
            this.db = db;
            this.memberDao = new MemberDao(db);
            this.memberSubTypeDao = new MembershipSubscriptionTypeDao(db);
            this.userManager = userManager;
        }

        public Member findMember(String username)
        {
            return memberDao.findMember(username);
        }

        //TODO if referenced correctly we dont' need 
        public Address findAddress(Member member)
        {
            Address address = db.Addresses.FirstOrDefault(x => x.MemberID == member.MemberID && x.AddressType == "Postal");
            return address;
        }

        //TODO if referenced correctly we dont' need 
        public Address findBillingAddress(Member member)
        {
            Address addressbill = db.Addresses.FirstOrDefault(x => x.MemberID == member.MemberID && x.AddressType == "Billing");
            return addressbill;
        }

        public List<Transaction> findMemberTransactions(Member member)
        {
            TransactionDao transactionDao = new TransactionDao(db);
            List<Transaction> transactions = transactionDao.findWonAndPaidTransactions(member.MemberID);
            return transactions;
        }

        public List<MemberSubscriptionType> getMembershipUpgrades(String username)
        {

            List<MemberSubscriptionType> types = new List<MemberSubscriptionType>();
            Member member = memberDao.findMember(username);
            double memberSubAmount = Convert.ToDouble(member.MemberSubscriptionType.amount);
            String type = member.MemberSubscriptionType.MemberSubscriptionTypeCode;
            List<MemberSubscriptionType> allTypes = memberSubTypeDao.findAll();
            switch (type.Trim())
            {
                case "Free":
                    types = allTypes.Where(t => t.MemberSubscriptionTypeCode != "Free").ToList();
                    break;
                case "Tycoon":
                    //already the highest so do nothing
                    break;
                case "Luxury":
                    MemberSubscriptionType tycoon = allTypes.Find(t => t.MemberSubscriptionTypeCode.Trim() == "Tycoon");
                    tycoon.amount = tycoon.amount - member.MemberSubscriptionType.amount;  //TODO check if this is right
                    types.Add(allTypes.Find(t => t.MemberSubscriptionTypeCode.Trim() == "Tycoon"));
                    break;
                case "Loving-life":
                    //TODO hmmmm.... detach objects just in case???
                    tycoon = allTypes.Find(t => t.MemberSubscriptionTypeCode.Trim() == "Tycoon");
                    MemberSubscriptionType luxury = allTypes.Find(t => t.MemberSubscriptionTypeCode.Trim() == "Luxury");
                    tycoon.amount = tycoon.amount - member.MemberSubscriptionType.amount;  //TODO check if this is right
                    luxury.amount = luxury.amount - member.MemberSubscriptionType.amount;
                    types.Add(tycoon);
                    types.Add(luxury);
                    break;
            }
            return types;
        }

        public bool createUser(Member member, String password, out ApplicationUser user, out IEnumerable<String> errors, UrlHelper urlHelper)
        {
            user = new ApplicationUser() { UserName = member.EmailAddress, Email = member.EmailAddress };
            IdentityResult result = userManager.Create(user, password);
            MemberDao memberDao = new MemberDao(db);
            member.ASPUserId = user.Id;
            if (member.MemberAcquisitionCampaignCode.ToLower().Equals("launch"))
            {
                member.MemberSubscriptionTypeID = db.MemberSubscriptionTypes.Where(x => x.MemberSubscriptionTypeCode.ToLower().Equals("tycoon")).First().MemberSubscriptionTypeID;
            }
            if (result.Succeeded && memberDao.save(member))
            {
                sendWelcomeEmail(user.Id, user.Email, urlHelper, user.Email,null);
                errors = new List<String>();
                return true;
            }
            errors = result.Errors;
            return false;
        }

        public bool createUser(Member member, String password, out ApplicationUser user, out IEnumerable<String> errors, System.Web.Http.Routing.UrlHelper apiURLHelper)
        {
            user = new ApplicationUser() { UserName = member.EmailAddress, Email = member.EmailAddress };
            IdentityResult result = userManager.Create(user, password);
            MemberDao memberDao = new MemberDao(db);
            member.ASPUserId = user.Id;
            if (member.MemberAcquisitionCampaignCode.ToLower().Equals("launch"))
            {
                member.MemberSubscriptionTypeID = db.MemberSubscriptionTypes.Where(x => x.MemberSubscriptionTypeCode.ToLower().Equals("tycoon")).First().MemberSubscriptionTypeID;
            }
            if (result.Succeeded && memberDao.save(member))
            {
                sendWelcomeEmail(user.Id, user.Email, null, user.Email, apiURLHelper);
                errors = new List<String>();
                return true;
            }
            errors = result.Errors;
            return false;
        }

        private async void sendWelcomeEmail(String userId, String email, UrlHelper urlHelper, string recipientName, System.Web.Http.Routing.UrlHelper apiUrlHelper)
        {
            string CallbackURL = "";
            string code = userManager.GenerateEmailConfirmationToken(userId);
            // webapi will send null urlhelper and defined base URL in CallbackURL
            if (urlHelper != null)
            {
                string protocol = urlHelper.RequestContext.HttpContext.Request.Url.Scheme;
                var callbackUrl = urlHelper.Action("ConfirmEmail", "Account", new { userId = userId, code = code }, protocol: protocol);
                CallbackURL = callbackUrl;
            }
            else
            {

                var callbackUrl = apiUrlHelper.Route("Default", new { userId = userId, code = code });
                CallbackURL = apiUrlHelper.Request.RequestUri.Scheme + "://" + apiUrlHelper.Request.RequestUri.Authority + "/" + callbackUrl;
            }


            EmailManager emailManager = new EmailManager();
            emailManager.NewEmail();
            emailManager.RecipientEmailAddress = email;
            emailManager.RecipientName = recipientName;
            emailManager.EmailSubject = " Welcome to VAULTLife.com! ";
            //emailManager.EmailBodyText = "<!DOCTYPE html><html xmlns='http://www.w3.org/1999/xhtml'><head><title></title><link rel='stylesheet' href='http://www.vaultlife.com/Content/images/email/email-styles.css' /></head><body><link rel='stylesheet' href='http://www.vaultlife.com/Content/images/email/email-styles.css' /><div class='header' style='background-color: black;'><img src='https://www.vaultlife.com/Content/images/logo.png' /></div><div class='content'><h1> WELCOME</h1><p>Welcome to VAULTLife.com, a world in which anything is possible! Its time to stop worrying about probabilities and wondering about possibilities! Begin a journey today with VAULTLife.com where you replace your to-do list with your bucket list! </p><p>We all have different dreams. Our worlds are coloured with different fantasies and we want to help you achieve yours! Based on this, we would like to tailor make your VAULTLife.com experience, so please select the link below to confirm that your details are correct and that we have all we need to make your VAULTLife.com experience once in a lifetime! </p><p>Please confirm your account by clicking <a href=\"" + CallbackURL + "\">here</a></p></div><div class='footer'><table class='signature' cellspacing='0'><tr><td class='details'><img src='http://www.vaultlife.com/Content/images/email/vl-team-sig_01.jpg' alt='Vaultlife.com' /><div class='content'><h2>VAULTLife TEAM</h2><div><strong>Email:</strong> &nbsp; <a href='mailto:info@vaultlife.com'>info@vaultlife.com</a></div><div><strong>Tel:</strong> &nbsp; <span>+27 86 123 6334</span></div><div><strong>Fax:</strong> &nbsp; <span>+27 86 123 6334</span></div><div><span class='small'>The Embassy, 50 Mulbarton Rd, Beverly, Johannesburg, 1296</span></div><div><a href='http://www.vaultlife.com'><strong>www.vaultlife.com</strong></a></div></div></td><td class='promo'><img src='http://www.vaultlife.com/Content/images/email/vl-team-sig_02.jpg' alt='Vaultlife.com' /></td></tr></table></div></body></html>";
            emailManager.EmailBodyText = "<!DOCTYPE html><html xmlns='http://www.w3.org/1999/xhtml'><head><title></title></head><body style='margin: 0px; padding: 0px;'><div class='header' style='background-color: black; padding: 15px 20px;'><img src='http://www.vaultlife.com/Content/images/logo.png' alt='Vaultlife.com' style='width: 127px; height: 39px;' /></div><div class='content' style='padding: 15px 30px; font-family: Segoe UI, Calibri, Arial, Helvetica, sans-serif; max-width: 800px; width: 100%;'><h1>WELCOME</h1><p>Welcome to VAULTLife.com, a world in which anything is possible! Its time to stop worrying about probabilities and wondering about possibilities! Begin a journey today with VAULTLife.com where you replace your to-do list with your bucket list!</p><p>We all have different dreams. Our worlds are coloured with different fantasies and we want to help you achieve yours! Based on this, we would like to tailor make your VAULTLife.com experience, so please select the link below to confirm that your details are correct and that we have all we need to make your VAULTLife.com experience once in a lifetime!</p><p>Please confirm your account by clicking <a href=\"" + CallbackURL + "\">here</a></p><p>code: " + code + "</p></div><div class='footer' style='padding: 15px 30px; font-family: 'Segoe UI', Calibri, Arial, Helvetica, sans-serif;'><table class='signature' cellspacing='0' cellpadding='0'><tr><td class='details' style='width: 308px; vertical-align: top; background-color: #161616;' valign='top'><img src='http://www.vaultlife.com/Content/images/email/vl-team-sig_01.jpg' alt='Vaultlife.com' style='width: 308px; height: 79px;' /><div class='content' style='padding: 0px 15px; font-size: 11px; color: white;'><h2 style='margin: 0px;font-size: 20px;'>VAULTLife TEAM</h2><div><strong>Email:</strong> &nbsp; <a href='mailto:info@vaultlife.com' style='color: #eeeeee;'>info@vaultlife.com</a></div><div><strong>Tel:</strong> &nbsp; <span>+27 86 123 6334</span></div><div><strong>Fax:</strong> &nbsp; <span>+27 86 123 6334</span></div><div><span class='small'>The Embassy, 50 Mulbarton Rd, Beverly, Johannesburg, 1296</span></div><div><a href='http://www.vaultlife.com' style='color: #eeeeee;'><strong>www.vaultlife.com</strong></a></div></div></td><td class='promo' valign='top' style='vertical-align: top; background-color: #161616; width: 220px'><img src='http://www.vaultlife.com/Content/images/email/vl-team-sig_02.jpg' alt='Vaultlife.com' style='width: 228px; height: 220px;' /></td></tr></table></div></body></html>";
            emailManager.QueueEmail();

        }
    }
}


           