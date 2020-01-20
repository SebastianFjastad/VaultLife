using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using FluentValidation.Results;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using System.Globalization;
using Vaultlife.Models;
using System.Web.SessionState;
using Vaultlife.ViewModels;
using Vaultlife.Dao;
using Vaultlife.Service;
using System.Data;
using System.Configuration;
using Vaultlife.Helpers;
using Vaultlife.Managers;
using System.Text.RegularExpressions;

namespace Vaultlife.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult getStates(FormCollection form)
        {
            //TODO can we inject in the controller?
            AddressService service = new AddressService(db);
            int? country = Convert.ToInt32(form["Country"].ToString().Trim());
            List<CountryState> states = service.getStates(country).ToList();
            var s = states.Select(x => new { StateID = x.StateID, StateName = x.StateName });
            return Json(s.ToArray());
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult getBillStates(FormCollection form)
        {
            //TODO can we inject in the controller?
            AddressService service = new AddressService(db);
            int? country = Convert.ToInt32(form["CountryBill"].ToString().Trim());
            List<CountryState> states = service.getStates(country).ToList();
            var s = states.Select(x => new { StateID = x.StateID, StateName = x.StateName });
            return Json(s.ToArray());
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult getCities(FormCollection form)
        {
            AddressService service = new AddressService(db);
            int? country = Convert.ToInt32(form["Country"].ToString().Trim());
            int? state = Convert.ToInt32(form["State"].ToString().Trim());
            IEnumerable<CountryCity> cities = service.getCities(country, state);
            var s = cities.Select(x => new { CityID = x.CityID, CityName = x.CityName });
            return Json(s.ToArray());
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult getBillCities(FormCollection form)
        {
            AddressService service = new AddressService(db);
            int? country = Convert.ToInt32(form["CountryBill"].ToString().Trim());
            int? state = Convert.ToInt32(form["StateBill"].ToString().Trim());
            IEnumerable<CountryCity> cities = service.getCities(country, state);
            var s = cities.Select(x => new { CityID = x.CityID, CityName = x.CityName });
            return Json(s.ToArray());
        }

        [AllowAnonymous]
        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            this.Session["Culture"] = new CultureInfo(lang);
            return Redirect(returnUrl);
            
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                UserService userService = new UserService(db, _userManager);
                if (userService.hasPassword(model.Email))
                {
                    var user = await UserManager.FindAsync(model.Email, model.Password);
                    if (user != null)
                    {
                        await SignInAsync(user, model.RememberMe);
                        GameDao gd = new GameDao(db);
                        gd.AddMemberToGame(user);   
                        return RedirectToLocal(returnUrl);
                    }
                }
                else
                {
                    return RedirectToLocal("/Account/ForgotPassword");
                }
            }
            ModelState.AddModelError("", "Invalid username or password.");
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(Vaultlife.Models.RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Create", "Members");
                }
                AddErrors(result.Errors);
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult RegisterFull()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }
            
            AccountViewModel avm = new AccountViewModel();

            ViewBag.Country = new SelectList(db.Countries, "CountryID", "CountryName");
            ViewBag.State = new SelectList(db.CountryStates, "StateID", "StateName");
            ViewBag.City = new SelectList(db.CountryCities, "CityID", "CityName");
            List<MemberSubscriptionType> subList = new List<MemberSubscriptionType>();
            MemberSubscriptionType memberSubscriptionType;
            foreach (var item in db.MemberSubscriptionTypes)
            {
                memberSubscriptionType = new MemberSubscriptionType();
                memberSubscriptionType.MemberSubscriptionTypeID = item.MemberSubscriptionTypeID;
                memberSubscriptionType.MemberSubscriptionTypeCode = item.MemberSubscriptionTypeCode;

                if (item.MemberSubscriptionTypeDescription == "Bronze")
                {
                    memberSubscriptionType.MemberSubscriptionTypeDescription = item.MemberSubscriptionTypeDescription + " - " + item.MemberSubscriptionTypeCode;
                }
                else
                {
                    memberSubscriptionType.MemberSubscriptionTypeDescription = item.MemberSubscriptionTypeDescription;

                }
                subList.Add(memberSubscriptionType);
            }

            ViewBag.MemberSubscriptionType = new SelectList(subList, "MemberSubscriptionTypeID", "MemberSubscriptionTypeDescription");




            return View(avm);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> RegisterFull([Bind(Include = "Email,Password,ConfirmPassword,FirstName,LastName,MobileNumber,PromoCode,MemberSubscriptionType")] AccountViewModel avm, FormCollection form)
        { 
            ModelState.Remove("Email");
            ModelState.Remove("Password");
            ModelState.Remove("TermsAndConditions");
            avm.CountryID = Convert.ToInt32(form["Country"].ToString().Trim());
            if (form["State"].ToString().Trim() != "")
            {
                avm.StateID = Convert.ToInt32(form["State"].ToString().Trim());
            }
            if (form["City"].ToString().Trim() != "")
            {
                avm.CityID = Convert.ToInt32(form["City"].ToString().Trim());
            }
            avm.OwnerID = getOwner(avm.CountryID, avm.StateID, avm.CityID, avm.PromoCode);
            avm.MobileNumber = Regex.Replace(form["MobileNumber"].ToString(), @"\s+", "");
            if (ModelState.IsValid)
            {
                AccountService service = new AccountService(db, UserManager);
                IEnumerable<String> errors;
                ApplicationUser user;
                UrlHelper urlHelper = new UrlHelper(Request.RequestContext);
                if (service.createUser(toMember(avm), avm.Password, out user, out errors, urlHelper))
                {
                    GameDao gd = new GameDao(db);
                    gd.AddMemberToGame(user);
                   await SignInAsync(user, isPersistent: false);
                    // Adds new users to (world) games
                  
                  
                    if (Convert.ToInt32(avm.MemberSubscriptionType) != 1 && !avm.PromoCode.ToLower().Equals("launch"))
                    {
                        return Redirect("/Payments/Pay?type=" + avm.MemberSubscriptionType);
                    }
                    else
                    {
                        return RedirectToAction("Manage", "Account");
                    }
                }
                AddErrors(errors, "CustomPassword");
            }

            AddressService addressService = new AddressService(db);
            IEnumerable<CountryState> states = addressService.getStates(avm.CountryID);
            IEnumerable<CountryCity> cities = addressService.getCities(avm.CountryID, avm.StateID);

            ViewBag.Country = new SelectList(db.Countries, "CountryID", "CountryName", avm.CountryID);
            ViewBag.State = new SelectList(states, "StateID", "StateName", avm.StateID);
            ViewBag.City = new SelectList(cities, "CityID", "CityName", avm.CityID);
            ViewBag.MemberSubscriptionType = new SelectList(db.MemberSubscriptionTypes, "MemberSubscriptionTypeID", "MemberSubscriptionTypeDescription", avm.MemberSubscriptionType);
            return View(avm);
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null) 
            {
                return View("Error");
            }

            IdentityResult result = await UserManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                return View("ConfirmEmail");
            }
            else
            {
                AddErrors(result.Errors);
                return View();
            }
        }


        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    ModelState.AddModelError("", "The user either does not exist or is not confirmed.");
                    return View();
                }

                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);

                EmailManager emailManager = new EmailManager();
                emailManager.NewEmail();
                emailManager.RecipientEmailAddress = user.Email;
                emailManager.RecipientName = user.UserName;
                emailManager.EmailSubject = " Reset Password";
                //emailManager.EmailBodyText = "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>";
                //emailManager.EmailBodyText = "<!DOCTYPE html><html xmlns='http://www.w3.org/1999/xhtml'><head><title></title><link rel='stylesheet' href='http://www.vaultlife.com/Content/images/email/email-styles.css' /></head><body><link rel='stylesheet' href='http://www.vaultlife.com/Content/images/email/email-styles.css' /><div class='header' style='background-color: black;'><img src='https://www.vaultlife.com/Content/images/logo.png' /></div><div class='content'><h1> RESET PASSWORD</h1><p>Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a></p></div><div class='footer'><table class='signature' cellspacing='0'><tr><td class='details'><img src='http://www.vaultlife.com/Content/images/email/vl-team-sig_01.jpg' alt='Vaultlife.com' /><div class='content'><h2>VAULTLife TEAM</h2><div><strong>Email:</strong> &nbsp; <a href='mailto:info@vaultlife.com'>info@vaultlife.com</a></div><div><strong>Tel:</strong> &nbsp; <span>+27 86 123 6334</span></div><div><strong>Fax:</strong> &nbsp; <span>+27 86 123 6334</span></div><div><span class='small'>The Embassy, 50 Mulbarton Rd, Beverly, Johannesburg, 1296</span></div><div><a href='http://www.vaultlife.com'><strong>www.vaultlife.com</strong></a></div></div></td><td class='promo'><img src='http://www.vaultlife.com/Content/images/email/vl-team-sig_02.jpg' alt='Vaultlife.com' /></td></tr></table></div></body></html>";
                emailManager.EmailBodyText = "<!DOCTYPE html><html xmlns='http://www.w3.org/1999/xhtml'><head><title></title></head><body style='margin: 0px; padding: 0px;'><div class='header' style='background-color: black; padding: 15px 20px;'><img src='http://www.vaultlife.com/Content/images/logo.png' alt='Vaultlife.com' style='width: 127px; height: 39px;'/></div><div class='content' style='padding: 15px 30px; font-family: Segoe UI, Calibri, Arial, Helvetica, sans-serif; max-width: 800px; width: 100%;'><h1>RESET PASSWORD</h1><p>Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a></p></div><div class='footer' style='padding: 15px 30px; font-family: 'Segoe UI', Calibri, Arial, Helvetica, sans-serif;'><table class='signature' cellspacing='0' cellpadding='0'><tr><td class='details' style='width: 308px; vertical-align: top; background-color: #161616;' valign='top'><img src='http://www.vaultlife.com/Content/images/email/vl-team-sig_01.jpg' alt='Vaultlife.com' style='width: 308px; height: 79px;' /><div class='content' style='padding: 0px 15px; font-size: 11px; color: white;'><h2 style='margin: 0px;font-size: 20px;'>VAULTLife TEAM</h2><div><strong>Email:</strong> &nbsp; <a href='mailto:info@vaultlife.com' style='color: #eeeeee;'>info@vaultlife.com</a></div><div><strong>Tel:</strong> &nbsp; <span>+27 86 123 6334</span></div><div><strong>Fax:</strong> &nbsp; <span>+27 86 123 6334</span></div><div><span class='small'>The Embassy, 50 Mulbarton Rd, Beverly, Johannesburg, 1296</span></div><div><a href='http://www.vaultlife.com' style='color: #eeeeee;'><strong>www.vaultlife.com</strong></a></div></div></td><td class='promo' valign='top' style='vertical-align: top; background-color: #161616; width: 220px'><img src='http://www.vaultlife.com/Content/images/email/vl-team-sig_02.jpg' alt='Vaultlife.com' style='width: 228px; height: 220px;' /></td></tr></table></div></body></html>";

                emailManager.QueueEmail();

                // send emails - this would be called by the scheduler
            //    EmailSendManager esm = new EmailSendManager();
              //  esm.SendQueuedEmail();

                /*EmailHelper emailHelper = new EmailHelper();
                emailHelper.AddToAddress(user.Id);
                emailHelper.MailSubject = "Reset Password";
                emailHelper.MailBody = "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>";
                emailHelper.SendMail();
                */
                //await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
	
        
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            if (code == null) 
            {
                return View("Error");
            }
            return View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "No user found.");
                    return View();
                }
                IdentityResult result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                }
                else
                {
                    AddErrors(result.Errors);
                    return View();
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                await SignInAsync(user, isPersistent: false);
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }


    
        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message, int? id)
        {
            AccountService service = new AccountService(db, UserManager);
            AddressService addressService = new AddressService(db);
            Member member = service.findMember(User.Identity.Name);
            if (member == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //what does this do?
            member = (Member)Helpers.TableTracker.TrackEdit(member, User.Identity.Name);
            if (member == null)
            {
                return HttpNotFound();
            }

            if (member.DateOfBirth <= Convert.ToDateTime("1900-01-01 12:00:00"))
            {

                member.DateOfBirth = DateTime.Now ;
            }

            AccountManageViewModel amvm = toAccountManageViewModel(member, service.findAddress(member), service.findBillingAddress(member));

            amvm.CityID = (amvm.CityID == null) ? member.CityID : amvm.CityID;
            amvm.StateID = (amvm.StateID == null) ? member.StateID : amvm.StateID;
            amvm.CountryID = (amvm.CountryID == null) ? member.CountryID : amvm.CountryID;

            amvm.CityBillID = (amvm.CityBillID == null) ? member.CityID : amvm.CityBillID;
            amvm.StateBillID = (amvm.StateBillID == null) ? member.StateID : amvm.StateBillID;
            amvm.CountryBillID = (amvm.CountryBillID == null) ? member.CountryID : amvm.CountryBillID;

            
            amvm.Transactions = service.findMemberTransactions(member);
            
            ViewBag.Transactions = amvm.Transactions;

            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            ViewBag.Designation = new SelectList(amvm.allDesignations(), "Value", "text", amvm.Designation);
            ViewBag.Industry = new SelectList(amvm.allIndustry(), "Value", "text", amvm.Industry);
            ViewBag.MaritalStatus = new SelectList(amvm.allMaritalStatus(), "Value", "text", amvm.MaritalStatus);
            ViewBag.Children = new SelectList(amvm.allChildren(), "Value", "text", amvm.Children);
            ViewBag.Ethnicity = new SelectList(amvm.allEthnics(), "Value", "text", amvm.Ethnicity);
     
            ViewBag.Country = new SelectList(db.Countries, "CountryID", "CountryName", amvm.CountryID);
            IEnumerable<CountryState> states = addressService.getStates(amvm.CountryID);
            ViewBag.State = new SelectList(states, "StateID", "StateName", amvm.StateID);
            IEnumerable<CountryCity> cities = addressService.getCities(amvm.CountryID, amvm.StateID);
            ViewBag.City = new SelectList(cities, "CityID", "CityName", amvm.CityID);

            ViewBag.CountryBill = new SelectList(db.Countries, "CountryID", "CountryName", amvm.CountryBillID);
            ViewBag.StateBill = new SelectList(db.CountryStates.Where(x=>x.CountryID==amvm.CountryBillID), "StateID", "StateName", amvm.StateBillID);
            ViewBag.CityBill = new SelectList(db.CountryCities.Where(x=>x.StateID==amvm.StateBillID), "CityID", "CityName", amvm.CityBillID);
            ViewBag.MemberSubscriptionTypeID = new SelectList(db.MemberSubscriptionTypes, "MemberSubscriptionTypeID", "MemberSubscriptionTypeDescription");
           
            ViewBag.MembershipSubscriptionTypes = service.getMembershipUpgrades(User.Identity.Name);
            return View(amvm);
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model, [Bind(Include = "FirstName,LastName,Email,Gender,MobileNumber,DateOfBirth,AddressLineOne," +
                                                                                          " AddressLineTwo, AddressZip, AddressBillLineOne, AddressBillLineTwo," +
                                                                                          " AddressBillZip, MaritalStatus, Children, Ethnicity, Industry, Designation," +
                                                                                          " MemberInterestsAutomotive, MemberInterestsArtCollectibles, MemberInterestsHomeAppliances," +
                                                                                          " MemberInterestsToys, MemberInterestsEntertainment, MemberInterestsDecoreDesign," +
                                                                                          " MemberInterestsHealthBeauty, MemberInterestsTravel, MemberInterestsFashionAccessories," +
                                                                                          " MemberInterestsExperiences, MemberInterestsTechnology, MemberInterestsWiningDining," +
                                                                                          " MemberInterestsOther")] AccountManageViewModel amvm, FormCollection form)
        {
            // Code for function 1
            ModelState.Remove("OldPassword");
            ModelState.Remove("NewPassword");
            ModelState.Remove("ConfirmPassword");
            ModelState.Remove("FirstName");
            ModelState.Remove("LastName");
            ModelState.Remove("Email");
            ModelState.Remove("ConfirmEmail");
            ModelState.Remove("Gender");
            ModelState.Remove("AddressLineOne");
            ModelState.Remove("AddressLineTwo");
            ModelState.Remove("AddressZip");
            ModelState.Remove("AddressBillLineOne");
            ModelState.Remove("AddressBillLineTwo");
            ModelState.Remove("AddressBillZip");
            ModelState.Remove("MobileNumber");
            ModelState.Remove("DateOfBirth");
            ModelState.Remove("Ethicity");
            ModelState.Remove("MaritalStatus");
            ModelState.Remove("Children");
            ModelState.Remove("Industry");
            ModelState.Remove("Designation");

            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");

            //password form in accordion
            if (Request.Form["passwordsubmit"] != null)
            {

                if (hasPassword)
                {
                    if (ModelState.IsValid)
                    {
                        IdentityResult result =
                            await
                                UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                                    model.NewPassword);
                        if (result.Succeeded)
                        {
                            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                            await SignInAsync(user, isPersistent: false);
                            //return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                            TempData["messagePasswordSubmit"] = "Successfuly submitted!";
                            return new RedirectResult(Url.Action("Manage", new { Message = ManageMessageId.SetPasswordSuccess }) + "#change-password");
                        }
                        else
                        {
                            TempData["messagePasswordSubmit"] = "Submit failed.";
                            AddErrors(result.Errors);
                        }
                    }
                }
                else
                {
                    // User does not have a password so remove any validation errors caused by a missing OldPassword field
                    ModelState state = ModelState["OldPassword"];
                    if (state != null)
                    {
                        state.Errors.Clear();
                    }

                    if (ModelState.IsValid)
                    {
                        IdentityResult result =
                            await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                        if (result.Succeeded)
                        {
                            TempData["messagePasswordSubmit"] = "Successfuly submitted!";
                            return new RedirectResult(Url.Action("Manage", new { Message = ManageMessageId.SetPasswordSuccess }) + "#change-password");
                        }
                        else
                        {
                            TempData["messagePasswordSubmit"] = "Submit failed.";
                            AddErrors(result.Errors);
                        }
                    }
                }
            }
            //personal info form in accordion
            else if (Request.Form["submitpersonal"] != null)
            {

                string FirstName = amvm.FirstName;
                string LastName = amvm.LastName;
                string MobileNumber = amvm.MobileNumber;
              
                string Email = amvm.Email;
                Member member = new Member();
                string currentUserId = User.Identity.GetUserId();
                member = db.Members.FirstOrDefault(x => x.ASPUserId == currentUserId);
                member.FirstName = FirstName;
                member.LastName = LastName;
                member.Gender = amvm.Gender;
                member.TelephoneMobile = Regex.Replace(MobileNumber, @"\s+", "");
                member.DateOfBirth =  (DateTime)amvm.DateOfBirth;
                member.EmailAddress = Email;
                member.USR = Email;
                member.DateUpdated = DateTime.Now;
                string CountryID = form["Country"].ToString().Trim();
                string CountryBillID = form["CountryBill"].ToString().Trim();
                string StateID;
                string StateBillID;
                string CityID;
                string CityBillID;


                //member.IdentityNumber = amvm.IDNumber;
                member.MaritalStatus = amvm.MaritalStatus;
                member.Ethnicity = amvm.Ethnicity;
                member.Children = amvm.Children;
                member.Industry = amvm.Industry;
                member.Designation = amvm.Designation;

                Address address = new Address();
                Address addressnew = new Address();
                Address addressbill = new Address();
                Address addressbillnew = new Address();
                address = db.Addresses.FirstOrDefault(x => x.MemberID == member.MemberID && x.AddressType == "Postal");
                addressbill = db.Addresses.FirstOrDefault(x => x.MemberID == member.MemberID && x.AddressType == "Billing");
                int countryID = Convert.ToInt32(CountryID);
                int countryBillID = Convert.ToInt32(CountryBillID);
                Country c = db.Countries.FirstOrDefault(x => x.CountryID == countryID);
                Country cb = db.Countries.FirstOrDefault(x => x.CountryID == countryBillID);

                if (address != null)
                {
                    address.Country = c.CountryName;

                    if (form["State"] != null && !form["State"].Equals(""))
                    {
                        StateID = form["State"].ToString().Trim();
                        int stateID = Convert.ToInt32(StateID);
                        CountryState cs = db.CountryStates.FirstOrDefault(x => x.StateID == stateID);
                        address.StateOrProvince = cs.StateName;
                    }
                    else
                    {
                        address.StateOrProvince = "NA";
                    }
                    if (form["City"] != null && !form["City"].Equals(""))
                    {
                        CityID = form["City"].ToString().Trim();
                        int cityID = Convert.ToInt32(CityID);
                        CountryCity cc = db.CountryCities.FirstOrDefault(x => x.CityID == cityID);
                        address.CityName = cc.CityName;
                    }
                    else
                    {
                        address.CityName = "NA";
                    }

                    address.AddressLine1 = amvm.AddressLineOne;
                    address.AddressLine2 = amvm.AddressLineTwo;
                    address.ZipOrPostalCode = amvm.AddressZip;
                    address.AddressType = "Postal";
                    address.USR = member.USR;
                    address.MemberID = member.MemberID;
                    address.DateInserted = DateTime.Now;
                    address.DateUpdated = DateTime.Now;
                }
                else
                {
                    addressnew.Country = c.CountryName;

                    if (form["State"] != null && !form["State"].Equals(""))
                    {
                        StateID = string.IsNullOrEmpty(form["State"]) ? string.Empty : form["State"].ToString().Trim(); 
                        int stateID = Convert.ToInt32(StateID);
                        CountryState cs = db.CountryStates.FirstOrDefault(x => x.StateID == stateID);
                        addressnew.StateOrProvince = cs.StateName;
                    }
                    else
                    {
                        addressnew.StateOrProvince = "NA";
                    }
                    if (form["City"] != null && !form["City"].Equals(""))
                    {
                        CityID = form["City"].ToString().Trim();
                        int cityID = Convert.ToInt32(CityID);
                        CountryCity cc = db.CountryCities.FirstOrDefault(x => x.CityID == cityID);
                        addressnew.CityName = cc.CityName;
                    }
                    else
                    {
                        addressnew.CityName = "NA";
                    }

                    addressnew.AddressLine1 = amvm.AddressLineOne;
                    addressnew.AddressLine2 = amvm.AddressLineTwo;
                    addressnew.ZipOrPostalCode = amvm.AddressZip;
                    addressnew.AddressType = "Postal";
                    addressnew.USR = member.USR;
                    addressnew.MemberID = member.MemberID;
                    addressnew.DateInserted = DateTime.Now;
                    addressnew.DateUpdated = DateTime.Now;

                }

                if (addressbill != null)
                {
                    addressbill.Country = cb.CountryName;

                    if (form["StateBill"] != null && !form["StateBill"].Equals(""))
                    {
                        StateBillID = form["StateBill"].ToString().Trim();
                        int stateBillID = Convert.ToInt32(StateBillID);
                        CountryState csb = db.CountryStates.FirstOrDefault(x => x.StateID == stateBillID);
                        addressbill.StateOrProvince = csb.StateName;
                    }
                    else
                    {
                        addressbill.StateOrProvince = "NA";
                    }
                    if (form["CityBill"] != null && !form["CityBill"].Equals(""))
                    {
                        CityBillID = form["CityBill"].ToString().Trim();
                        int cityBillID = Convert.ToInt32(CityBillID);
                        CountryCity ccb = db.CountryCities.FirstOrDefault(x => x.CityID == cityBillID);
                        addressbill.CityName = ccb.CityName;
                    }
                    else
                    {
                        addressbill.CityName = "NA";
                    }

                    addressbill.AddressLine1 = amvm.AddressBillLineOne;
                    addressbill.AddressLine2 = amvm.AddressBillLineTwo;
                    addressbill.ZipOrPostalCode = amvm.AddressBillZip;
                    addressbill.AddressType = "Billing";
                    addressbill.USR = member.USR;
                    addressbill.MemberID = member.MemberID;
                    addressbill.DateInserted = DateTime.Now;
                    addressbill.DateUpdated = DateTime.Now;
                }
                else
                {
                    addressbillnew.Country = c.CountryName;

                    if (form["StateBill"] != null && !form["StateBill"].Equals(""))
                    {
                        StateBillID = form["StateBill"].ToString().Trim();
                        int stateBillID = Convert.ToInt32(StateBillID);
                        CountryState csb = db.CountryStates.FirstOrDefault(x => x.StateID == stateBillID);
                        addressbillnew.StateOrProvince = csb.StateName;
                    }
                    else
                    {
                        addressbillnew.StateOrProvince = "NA";
                    }
                    if (form["CityBill"] != null && !form["CityBill"].Equals(""))
                    {
                        CityBillID = form["CityBill"].ToString().Trim();
                        int cityBillID = Convert.ToInt32(CityBillID);
                        CountryCity ccb = db.CountryCities.FirstOrDefault(x => x.CityID == cityBillID);
                        addressbillnew.CityName = ccb.CityName;
                    }
                    else
                    {
                        addressbillnew.CityName = "NA";
                    }

                    addressbillnew.AddressLine1 = amvm.AddressBillLineOne;
                    addressbillnew.AddressLine2 = amvm.AddressBillLineTwo;
                    addressbillnew.ZipOrPostalCode = amvm.AddressBillZip;
                    addressbillnew.AddressType = "Billing";
                    addressbillnew.USR = member.USR;
                    addressbillnew.MemberID = member.MemberID;
                    addressbillnew.DateInserted = DateTime.Now;
                    addressbillnew.DateUpdated = DateTime.Now;

                }

                MemberValidator validator = new MemberValidator();
                if (ModelState.IsValid) // && validator.Validate(member).IsValid)
                {
                    if (address != null)
                    {
                        db.Entry(address).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(addressnew).State = EntityState.Added;
                        db.SaveChanges();
                    }

                    if (addressbill != null)
                    {

                        db.Entry(addressbill).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(addressbillnew).State = EntityState.Added;
                        db.SaveChanges();
                    }

                    db.Entry(member).State = EntityState.Modified;
                    db.SaveChanges();

                    //ViewBag.result = "Record Inserted Successfully!";
                    //amvm.SubmitMessage = "Success!";
                    //Session["Success"] = "Submission successful!";
                    TempData["messageSubmit"] = "Successfuly submitted!";
                    return new RedirectResult(Url.Action("Manage") + "#personal-information");

                }
                ValidationResult results = validator.Validate(member);
                IList<ValidationFailure> failures = results.Errors;
                StringBuilder sb = new StringBuilder();
                foreach (var e in results.Errors)
                {

                    ModelState.AddModelError(e.PropertyName + "Error", e.ErrorMessage);
                    sb.AppendLine(e.ErrorMessage);

                }

                // If we got this far, something failed, redisplay form
                AddressService addressService = new AddressService(db);
                IEnumerable<CountryState> states = addressService.getStates(member.CountryID);
                IEnumerable<CountryCity> cities = addressService.getCities(member.CountryID, member.StateID);

                ViewBag.Designation = new SelectList(amvm.allDesignations(), "Value", "text");
                ViewBag.Industry = new SelectList(amvm.allIndustry(), "Value", "text");
                ViewBag.MaritalStatus = new SelectList(amvm.allMaritalStatus(), "Value", "text");
                ViewBag.Children = new SelectList(amvm.allChildren(), "Value", "text");
                ViewBag.Ethnicity = new SelectList(amvm.allEthnics(), "Value", "text");
                ViewBag.Country = new SelectList(db.Countries, "CountryID", "CountryName", countryID);
                ViewBag.State = new SelectList(states, "StateID", "StateName");
                ViewBag.City = new SelectList(db.CountryCities, "CityID", "CityName");
                ViewBag.CountryBill = new SelectList(db.Countries, "CountryID", "CountryName", countryBillID);
                ViewBag.StateBill = new SelectList(states, "StateID", "StateName");
                ViewBag.CityBill = new SelectList(db.CountryCities, "CityID", "CityName");
                ViewBag.MemberSubscriptionTypeID = new SelectList(db.MemberSubscriptionTypes, "MemberSubscriptionTypeID", "MemberSubscriptionTypeCode");
                TempData["messageSubmit"] = "Submission failed";
            }
            //member interests form in accordion
            else if (Request.Form["submitinterests"] != null)
            {
                Member member = new Member();
                string currentUserId = User.Identity.GetUserId();
                member = db.Members.FirstOrDefault(x => x.ASPUserId == currentUserId);
                MemberInterest memberinterest = new MemberInterest();
                MemberInterest memberinterestnew = new MemberInterest();
                memberinterest  = db.MemberInterests.FirstOrDefault(x => x.USR == member.USR );

                if (memberinterest != null)
                {
                    memberinterest.MemberInterestAutomotive = amvm.MemberInterestsAutomotive;
                    memberinterest.MemberInterestArtCollectibles = amvm.MemberInterestsArtCollectibles;
                    memberinterest.MemberInterestDecorDesign = amvm.MemberInterestsDecoreDesign;
                    memberinterest.MemberInterestEntertainment = amvm.MemberInterestsEntertainment;
                    memberinterest.MemberInterestExperiences = amvm.MemberInterestsExperiences;
                    memberinterest.MemberInterestFashionAccessories = amvm.MemberInterestsFashionAccessories;
                    memberinterest.MemberInterestHealthBeauty = amvm.MemberInterestsHealthBeauty;
                    memberinterest.MemberInterestHomeAppliances = amvm.MemberInterestsHomeAppliances;
                    memberinterest.MemberInterestOther = amvm.MemberInterestsOther;
                    memberinterest.MemberInterestTechnology = amvm.MemberInterestsTechnology;
                    memberinterest.MemberInterestToys = amvm.MemberInterestsToys;
                    memberinterest.MemberInterestTravel = amvm.MemberInterestsTravel;
                    memberinterest.MemberInterestWiningDining = amvm.MemberInterestsWiningDining;  
                    memberinterest.DateUpdated = DateTime.Now;
                }
                else
                {
                    memberinterestnew.MemberInterestAutomotive = amvm.MemberInterestsAutomotive;
                    memberinterestnew.MemberInterestArtCollectibles = amvm.MemberInterestsArtCollectibles;
                    memberinterestnew.MemberInterestDecorDesign = amvm.MemberInterestsDecoreDesign;
                    memberinterestnew.MemberInterestEntertainment = amvm.MemberInterestsEntertainment;
                    memberinterestnew.MemberInterestExperiences = amvm.MemberInterestsExperiences;
                    memberinterestnew.MemberInterestFashionAccessories = amvm.MemberInterestsFashionAccessories;
                    memberinterestnew.MemberInterestHealthBeauty = amvm.MemberInterestsHealthBeauty;
                    memberinterestnew.MemberInterestHomeAppliances = amvm.MemberInterestsHomeAppliances;
                    memberinterestnew.MemberInterestOther = amvm.MemberInterestsOther;
                    memberinterestnew.MemberInterestTechnology = amvm.MemberInterestsTechnology;
                    memberinterestnew.MemberInterestToys = amvm.MemberInterestsToys;
                    memberinterestnew.MemberInterestTravel = amvm.MemberInterestsTravel;
                    memberinterestnew.MemberInterestWiningDining = amvm.MemberInterestsWiningDining;  
                    memberinterestnew.MemberID = member.MemberID;
                    memberinterestnew.ProductCategoryID = 1;
                    memberinterestnew.DateInserted = DateTime.Now;
                    memberinterestnew.DateUpdated = DateTime.Now;
                    memberinterestnew.USR = member.USR;
                }

                if (ModelState.IsValid) // && validator.Validate(member).IsValid)
                {
                    if (memberinterest != null)
                    {
                        db.Entry(memberinterest).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(memberinterestnew).State = EntityState.Added;
                        db.SaveChanges();
                    }

                    TempData["messageInterestSubmit"] = "Successfuly submitted!";
                    return new RedirectResult(Url.Action("Manage") + "#interests");
                }

                //submit failed
                TempData["messageInterestSubmit"] = "Submit failed.";
            }
            return View(amvm);
        }

        //
        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("FBRegisterFull", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                GameDao gameDao = new GameDao(db);
                gameDao.AddMemberToGame(user);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
                        
                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // SendEmail(user.Email, callbackUrl, "Confirm your account", "Please confirm your account by clicking this link");
                        
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result.Errors);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

		public ActionResult MyVaultItems()
		{
			return View();
		}

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }
        //
        // GET: /Account/RegisterFull
        [AllowAnonymous]
        public async Task<ActionResult> FBRegisterFull(string returnUrl)
        {

            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }
            var firstNameClaim = loginInfo.ExternalIdentity.Claims.First(c => c.Type == "urn:facebook:first_name");
            var lastNameClaim = loginInfo.ExternalIdentity.Claims.First(c => c.Type == "urn:facebook:last_name");
            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }

            AccountViewModel avm = new AccountViewModel();
            // use first and last name from facebook.
            avm.FirstName = firstNameClaim.Value.ToString();
            avm.LastName = lastNameClaim.Value.ToString();
            ViewBag.Country = new SelectList(db.Countries, "CountryID", "CountryName");
            ViewBag.State = new SelectList(db.CountryStates, "StateID", "StateName");
            ViewBag.City = new SelectList(db.CountryCities, "CityID", "CityName");
            ViewBag.MemberSubscriptionType = new SelectList(db.MemberSubscriptionTypes, "MemberSubscriptionTypeID", "MemberSubscriptionTypeCode");
            return View(avm);
        }

        //
        // POST: /Account/FBRegisterFull
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> FBRegisterFull(Vaultlife.Models.RegisterViewModel model, [Bind(Include = "FirstName,LastName,MobileNumber,PromoCode,Gender,DateOfBirth")] AccountViewModel avm, FormCollection form)
        {


            ModelState.Remove("Email");
            ModelState.Remove("Password");
            ModelState.Remove("TermsAndConditions");
            avm.Email = model.Email;
            avm.Password = model.Password;
            avm.ConfirmPassword = model.ConfirmPassword;
            string CountryID = form["Country"].ToString().Trim();
            string StateID;
            string CityID;
            if (form["State"] != null && !form["State"].Equals(""))
            {
                StateID = form["State"].ToString().Trim();
            }
            else
            {
                StateID = string.Empty;
            }

            if (form["City"] != null && !form["City"].Equals(""))
            {
                CityID = form["City"].ToString().Trim();
            }
            else
            {
                CityID = string.Empty;
            }



            string IdentityTypeID = form["IdentityType"].ToString();
            string MemberSubscriptionTypeID = form["MemberSubscriptionType"].ToString().Trim();

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();

                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                avm.Email = info.Email;
                var user = new ApplicationUser() { UserName = info.Email, Email = info.Email };
                IdentityResult result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {

                        await SignInAsync(user, isPersistent: false);

                        Member member = new Member();

                        member.EmailAddress = avm.Email;
                        member.IdentityType = IdentityTypeID;
                        member.MemberSubscriptionTypeID = Convert.ToInt32(MemberSubscriptionTypeID);
                        member.FirstName = avm.FirstName;
                        member.LastName = avm.LastName;
                        member.TelephoneMobile = avm.MobileNumber;
                        member.MemberAcquisitionCampaignCode = avm.PromoCode;
                        member.CountryID = Convert.ToInt32(CountryID);//avm.CountryID;


                        if (StateID == string.Empty)
                        {
                            member.StateID = null;
                        }
                        else
                        {
                            member.StateID = Convert.ToInt32(StateID);
                        }

                        if (CityID == string.Empty)
                        {
                            member.CityID = null;
                        }
                        else
                        {
                            member.CityID = Convert.ToInt32(CityID);
                        }


                        
                        member.AddressID = -1;
                        member.DateInserted = DateTime.Today;
                        member.DateUpdated = DateTime.Today;
                        member.ASPUserId = user.Id;
                        member.USR = user.UserName;
                        member.ActiveIndicator = true;
                        member.OwnerID = getOwner(member.CountryID, member.StateID, member.CityID, avm.PromoCode);
                        db.Members.Add(member);
                        try
                        {
                            db.SaveChanges();
                            this.sendWelcomeEmail(member.ASPUserId, member.EmailAddress, member.FirstName + ' ' + member.LastName);

                            GameDao gd = new GameDao(db);
                            gd.AddMemberToGame(user);
                            try
                            {
                                db.SaveChanges();
                                return RedirectToAction("Manage", "Account");
                            }
                            catch (System.Data.Entity.Validation.DbEntityValidationException e)
                            {
                                foreach (var eve in e.EntityValidationErrors)
                                {
                                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                                    foreach (var ve in eve.ValidationErrors)
                                    {
                                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                            ve.PropertyName, ve.ErrorMessage);
                                    }
                                }

                            }


                        }
                        catch (System.Data.Entity.Validation.DbEntityValidationException e)
                        {
                            foreach (var eve in e.EntityValidationErrors)
                            {
                                Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                        ve.PropertyName, ve.ErrorMessage);
                                }
                            }

                        }
                    }
                }
            }
            //MemberValidator validator = new MemberValidator();
            // If we got this far, something failed, redisplay form


            ViewBag.Country = new SelectList(db.Countries, "CountryID", "CountryName", CountryID);
            ViewBag.State = new SelectList(db.CountryStates, "StateID", "StateName", StateID);
            ViewBag.City = new SelectList(db.CountryCities, "CityID", "CityName", CityID);
            ViewBag.MemberSubscriptionType = new SelectList(db.MemberSubscriptionTypes, "MemberSubscriptionTypeID", "MemberSubscriptionTypeCode", MemberSubscriptionTypeID);
            return View(avm);
        }

        private int? getOwner(int? CountryID, int? StateId, int? CityID, string promoCode)
        {
            int? retvar = null;
            if (String.IsNullOrEmpty(promoCode))
            {
                Territory territory = db.Territories.Where(x => x.TerritoryDefinitions.Where(t => t.CountryID == CountryID && t.StateID == StateId && t.CityID == CityID).Count() == 1).FirstOrDefault();
                retvar = (territory == null) ? retvar : territory.OwnerID;
            }
            else
            {
                IEnumerable<MemberAcquisitionCampaign> codes = db.MemberAcquisitionCampaigns.Where(m => m.MemberAcquisitionCampaignCode.ToLower().Equals(promoCode.ToLower()));
                if (codes.Count() > 0) {
                    retvar = codes.First().OwnerID;
                }
            }

            return retvar;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

       
        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        private void AddErrors(IEnumerable<String> errors)
        {
            foreach (String error in errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private void AddErrors(IEnumerable<String> errors, string strCustom)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(strCustom, error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private void SendEmail(string email, string callbackUrl, string subject, string message)
        {
            // For information on sending mail, please visit http://go.microsoft.com/fwlink/?LinkID=320771
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private Member toMember(AccountViewModel avm)
        {
            Member member = new Member();

            member.EmailAddress = avm.Email;
            member.IdentityType = "ID Document";
            member.MemberSubscriptionTypeID = Convert.ToInt32(1);  //TODO very suckey... create enum to represent db primary keys
            member.FirstName = avm.FirstName;
            member.LastName = avm.LastName;
            member.TelephoneMobile = avm.MobileNumber;
            member.MemberAcquisitionCampaignCode = avm.PromoCode;
            member.CountryID = Convert.ToInt32(avm.CountryID);
            member.StateID = avm.StateID;
            member.CityID = avm.CityID;
            member.AddressID = -1;
            member.DateInserted = DateTime.Today;
            member.DateUpdated = DateTime.Today;
            member.USR = avm.Email;
            member.ActiveIndicator = true;
            member.OwnerID = avm.OwnerID;
            // Setting Date Of Birth to Minimum Date...
            member.DateOfBirth = DateTime.MinValue;
            return member;

        }

        private List<SelectListItem> getIdentityTypes()
        {
            List<SelectListItem> IdentityTypes = new List<SelectListItem>();
            IdentityTypes.Add(new SelectListItem { Text = "ID Document", Value = "ID Document" });
            IdentityTypes.Add(new SelectListItem { Text = "Passport", Value = "Passport" });
            IdentityTypes.Add(new SelectListItem { Text = "Drivers License", Value = "Drivers License" });
            IdentityTypes.Add(new SelectListItem { Text = "Social Security", Value = "Social Security" });
            return IdentityTypes;
        }

        private AccountManageViewModel toAccountManageViewModel(Member member, Address postalAddress, Address billingAddress)
        {
            AddressService addressService = new AddressService(db);

            AccountManageViewModel amvm = new AccountManageViewModel();
            amvm.FirstName = member.FirstName;
            amvm.LastName = member.LastName;
            amvm.Email = member.EmailAddress;
            amvm.ConfirmEmail = member.EmailAddress;
            amvm.Gender = member.Gender;
            amvm.MobileNumber = member.TelephoneMobile;
            amvm.DateOfBirth = member.DateOfBirth;
            amvm.Industry = member.Industry;
            amvm.Designation = member.Designation;
            amvm.Ethnicity = member.Ethnicity;
            amvm.Children = member.Children;
            amvm.MaritalStatus = member.MaritalStatus;

            if (member.MemberInterests.Count > 0)
            {
                //TODO 1 to many???
                MemberInterest interest = member.MemberInterests.First();
                amvm.MemberInterestsAutomotive = interest.MemberInterestAutomotive;
                amvm.MemberInterestsArtCollectibles = interest.MemberInterestArtCollectibles;
                amvm.MemberInterestsDecoreDesign = interest.MemberInterestDecorDesign;
                amvm.MemberInterestsEntertainment = interest.MemberInterestEntertainment;
                amvm.MemberInterestsExperiences = interest.MemberInterestExperiences;
                amvm.MemberInterestsFashionAccessories = interest.MemberInterestFashionAccessories;
                amvm.MemberInterestsHealthBeauty = interest.MemberInterestHealthBeauty;
                amvm.MemberInterestsHomeAppliances = interest.MemberInterestHomeAppliances;
                amvm.MemberInterestsOther = interest.MemberInterestOther;
                amvm.MemberInterestsTechnology = interest.MemberInterestTechnology;
                amvm.MemberInterestsToys = interest.MemberInterestToys;
                amvm.MemberInterestsTravel = interest.MemberInterestTravel;
                amvm.MemberInterestsWiningDining = interest.MemberInterestWiningDining;
            }

            if (postalAddress != null)
            {
                amvm.AddressLineOne = postalAddress.AddressLine1;
                amvm.AddressLineTwo = postalAddress.AddressLine2;
                amvm.AddressZip = postalAddress.ZipOrPostalCode;
                Country country = addressService.getCountry(postalAddress.Country);
                CountryState state = addressService.getState(postalAddress.StateOrProvince);
                CountryCity city = addressService.getCity(postalAddress.CityName);
                if (country != null) { amvm.CountryID = country.CountryID; }
                if (state != null) { amvm.StateID = state.StateID; }
                if (city != null) { amvm.CityID = city.CityID;  }

            }

            if (billingAddress != null)
            {
                amvm.AddressBillLineOne = billingAddress.AddressLine1;
                amvm.AddressBillLineTwo = billingAddress.AddressLine2;
                amvm.AddressBillZip = billingAddress.ZipOrPostalCode;

                Country country = addressService.getCountry(billingAddress.Country);
                CountryState state = addressService.getState(billingAddress.StateOrProvince);
                CountryCity city = addressService.getCity(billingAddress.CityName);
                if (country != null) { amvm.CountryBillID = country.CountryID; }
                if (state != null) { amvm.StateBillID = state.StateID; }
                if (city != null) { amvm.CityBillID = city.CityID; }

            }
            return amvm;
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        private void sendWelcomeEmail(String userId, String email, string recipientName)
        {
            /*No confirm for facebook . external logins
             * string code = userManager.GenerateEmailConfirmationToken(userId);
            string protocol = urlHelper.RequestContext.HttpContext.Request.Url.Scheme;
            var callbackUrl = urlHelper.Action("ConfirmEmail", "Account", new { userId = userId, code = code }, protocol: protocol); */

            EmailManager emailManager = new EmailManager();
            emailManager.NewEmail();
            emailManager.RecipientEmailAddress = email;
            emailManager.RecipientName = recipientName;
            emailManager.EmailSubject = " Welcome to VAULTLife.com! ";
//            emailManager.EmailBodyText = "<!DOCTYPE html><html xmlns='http://www.w3.org/1999/xhtml'><head><title></title></head><body><style type='text/css'>* {font-family: 'segoe ui', 'Calibri Light', Arial, Helvetica, sans-serif;}div {padding: 15px;}div.content {padding: 25px;}table {border: 1px solid #999999;}table td {border: 1px solid #efefef;margin: 0px;padding: 5px;}table.signature, table.signature td {border: none;}table.signature td {height: 220px;width: 308px;background: #161616;vertical-align: top;padding: 0px;}table.signature td.promo {width: 228px;}table.signature td.details div.content {padding: 0px 15px;color: #efefef;font-size: 12px;}table.signature td.details div.content strong, table.signature td.details div.content b {font-weight: bold;color: white;font-size: 12px;line-height: 1em;}table.signature td.details div.content h2 {margin: 0px;font-size: 22px;font-weight: normal;color: white;margin-top: -5px;margin-bottom: 5px;}table.signature td.details div.content strong, table.signature td.details div.content div {padding: 0px;}table.signature td.details div.content strong, table.signature td.details div.content div a,table.signature td.details div.content strong, table.signature td.details div.content div span {color: #dfdfdf;}table.signature td.details div.content div span.small {font-size: 11px;}</style><div class='header' style='background-color: black;'><img src='https://www.vaultlife.com/Content/images/logo.png' /></div><div class='content'><h1> WELCOME</h1><p>Welcome to VAULTLife.com, a world in which anything is possible! Its time to stop worrying about probabilities and wondering about possibilities! Begin a journey today with VAULTLife.com where you replace your to-do list with your bucket list! </p><p>We all have different dreams. Our worlds are coloured with different fantasies and we want to help you achieve yours! Based on this, we would like to tailor make your VAULTLife.com experience, so please select the link below to confirm that your details are correct and that we have all we need to make your VAULTLife.com experience once in a lifetime! </p><p>Please confirm your account by clicking <a href=\"" + CallbackURL + "\">here</a></p></div><div class='footer'><table class='signature' cellspacing='0'><tr><td class='details'><img src='http://www.vaultlife.com/Content/images/email/vl-team-sig_01.jpg' alt='Vaultlife.com' /><div class='content'><h2>VAULTLife TEAM</h2><div><strong>Email:</strong> &nbsp; <a href='mailto:info@vaultlife.com'>info@vaultlife.com</a></div><div><strong>Tel:</strong> &nbsp; <span>+27 86 123 6334</span></div><div><strong>Fax:</strong> &nbsp; <span>+27 86 123 6334</span></div><div><span class='small'>The Embassy, 50 Mulbarton Rd, Beverly, Johannesburg, 1296</span></div><div><a href='http://www.vaultlife.com'><strong>www.vaultlife.com</strong></a></div></div></td><td class='promo'><img src='http://www.vaultlife.com/Content/images/email/vl-team-sig_02.jpg' alt='Vaultlife.com' /></td></tr></table></div></body></html>";
            emailManager.EmailBodyText = "<!DOCTYPE html><html xmlns='http://www.w3.org/1999/xhtml'><head><title></title></head><body style='margin: 0px; padding: 0px;'><div class='header' style='background-color: black; padding: 15px 20px;'><img src='http://www.vaultlife.com/Content/images/logo.png' alt='Vaultlife.com' style='width: 127px; height: 39px;' /></div><div class='content' style='padding: 15px 30px; font-family: Segoe UI, Calibri, Arial, Helvetica, sans-serif; max-width: 800px; width: 100%;'><h1>WELCOME</h1><p>Welcome to VAULTLife.com, a world in which anything is possible! Its time to stop worrying about probabilities and wondering about possibilities! Begin a journey today with VAULTLife.com where you replace your to-do list with your bucket list!</p><p>We all have different dreams. Our worlds are coloured with different fantasies and we want to help you achieve yours! Based on this, we would like to tailor make your VAULTLife.com experience, so please confirm that your details are correct and that we have all we need to make your VAULTLife.com experience once in a lifetime!</p></div><div class='footer' style='padding: 15px 30px; font-family: 'Segoe UI', Calibri, Arial, Helvetica, sans-serif;'><table class='signature' cellspacing='0' cellpadding='0'><tr><td class='details' style='width: 308px; vertical-align: top; background-color: #161616;' valign='top'><img src='http://www.vaultlife.com/Content/images/email/vl-team-sig_01.jpg' alt='Vaultlife.com' style='width: 308px; height: 79px;' /><div class='content' style='padding: 0px 15px; font-size: 11px; color: white;'><h2 style='margin: 0px;font-size: 20px;'>VAULTLife TEAM</h2><div><strong>Email:</strong> &nbsp; <a href='mailto:info@vaultlife.com' style='color: #eeeeee;'>info@vaultlife.com</a></div><div><strong>Tel:</strong> &nbsp; <span>+27 86 123 6334</span></div><div><strong>Fax:</strong> &nbsp; <span>+27 86 123 6334</span></div><div><span class='small'>The Embassy, 50 Mulbarton Rd, Beverly, Johannesburg, 1296</span></div><div><a href='http://www.vaultlife.com' style='color: #eeeeee;'><strong>www.vaultlife.com</strong></a></div></div></td><td class='promo' valign='top' style='vertical-align: top; background-color: #161616; width: 220px'><img src='http://www.vaultlife.com/Content/images/email/vl-team-sig_02.jpg' alt='Vaultlife.com' style='width: 228px; height: 220px;' /></td></tr></table></div></body></html>";
            emailManager.QueueEmail();

        }
        #endregion
    }
}