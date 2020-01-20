using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using System.Net.Http.Formatting;
using Vaultlife.Models;
using System.Text.RegularExpressions;
using Vaultlife.Service;
using Vaultlife.ViewModels;
using Vaultlife.Helpers;
using Vaultlife.Dao;
using System.Globalization;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
//using System.Web.Http.Routing;


namespace Vaultlife.Controllers.Api
{
    public class AccountController : ApiController
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        [HttpPost]
        [ActionName("Authenticate")]
        [AllowAnonymous]
        public dynamic Authenticate(FormDataCollection formData)
        {

            if (formData != null)
            {
                string user = formData.Get("user"); 
                string password = formData.Get("password");

                string authToken = this.Authenticate(user, password);
                if (authToken.ToLower().Contains("required") || authToken.ToLower().Contains("invalid"))
                {
                    return new { Error = authToken };
                }

                 AccountService service = new AccountService(db, UserManager);
                 Member member = service.findMember(user);
                 if (member == null)
                 {
                     var error = new { Error = "Member not found", ErrorDetail = user };
                     return error;
                 }
                 var s = new { AuthToken = authToken, MemberID = member.MemberID };
                 return s;
            }
            return new { Error = "user and password reqiured" };
        }

        [Authorize]
        [HttpGet]
        [ActionName("ValidateToken")]
        public String ValidateToken()
        {
            var user = this.User.Identity;
            if (user != null)
            {
                return string.Format("{0} - {1}", user.Name, user.AuthenticationType);
            }
            else
            {
                return "Unable to resolve user";
            }
        }

        [Authorize]
        [HttpGet]
        [ActionName("GetPrivateData")]
        public object GetPrivateData()
        {
            return new { Message = "Secret information" };
        }

        [Authorize]
        [HttpGet]
        [ActionName("MemberTransactions")]
        public dynamic MemberTransactions()
        {
            var user = this.User.Identity;
            if (user == null)
            {
                return new { Error = "Unauthorized" };
            }

            AccountService service = new AccountService(db, UserManager);
            Member validatedMember = service.findMember(user.Name);
            if (validatedMember == null)
            {
                return new { Error = "Member not found" };
            }

            List<Transaction> memberTransactions = service.findMemberTransactions(validatedMember);
            return memberTransactions;
        }

        [HttpPost]
        [ActionName("RegisterFull")]
        [AllowAnonymous]
        public List<dynamic> RegisterFull(FormDataCollection formData)
        {
            List<string> errors = new List<string>();
            List<string> response = new List<string>();

            if (formData != null)
            {
                string name = formData.Get("name");
                string surname = formData.Get("surname");
                string password = formData.Get("password");
                string confirmPassword = formData.Get("confirmPassword");
                string email = formData.Get("email");
                string country = formData.Get("country");
                string state = formData.Get("state");
                string city = formData.Get("city");
                string mobile = formData.Get("mobile");
                string promo = formData.Get("promo");
                string membershipType = formData.Get("membershipType");
                string tnc = formData.Get("TermsAndConditions");

                // Validation section - cascade errors
                if (string.IsNullOrEmpty(name))
                    errors.Add("Name is required.");

                if (string.IsNullOrEmpty(surname))
                    errors.Add("Surname is required.");

                if (string.IsNullOrEmpty(password))
                    errors.Add("Password is required.");

                if (string.IsNullOrEmpty(confirmPassword))
                {
                    errors.Add("Confirm Password is required.");
                }
                else
                {
                    if (password.Trim() != confirmPassword.Trim())
                        errors.Add("Passwords don't match");
                }

                if (string.IsNullOrEmpty(email))
                {
                    errors.Add("Email is required.");
                }
                else
                {
                    // check for existing.

                }

                if (string.IsNullOrEmpty(country))
                {
                    errors.Add("Country is required.");
                }
                else
                {
                    // validate country ID

                }

                if (string.IsNullOrEmpty(membershipType))
                { 
                    errors.Add("Membership Type is required.");
                }
                else
                {
                    // validate membership type

                }

                if (string.IsNullOrEmpty(tnc))
                {
                    errors.Add("Terms and Conditions required.");
                }
                else
                {
                    if (tnc.Trim() != "1")
                        errors.Add("Terms and Conditions required.");
                }

                promo = (string.IsNullOrEmpty(promo)) ? null : promo;
                city = (string.IsNullOrEmpty(city)) ? "" : city;
                state = (string.IsNullOrEmpty(state)) ? "" : state;
                mobile = (string.IsNullOrEmpty(mobile)) ? "" : mobile;

                if (errors.Count > 0)
                {
                    return errors.ToList<dynamic>();
                }

                // No apparent errors, continue creation.
                int countryID = Convert.ToInt32(country.Trim());
                int? cityID = null;
                int? stateID = null; 
                
                if (!(string.IsNullOrEmpty(city)))
                {
                    cityID = Convert.ToInt32(city.Trim());
                }

                if (!(string.IsNullOrEmpty(state)))
                {
                    stateID = Convert.ToInt32(state.Trim());
                }

                int membershipTypeID = Convert.ToInt32(membershipType.Trim());
                int? OwnerID = getOwner(countryID, stateID, cityID, promo);
                string MobileNumber = "";
                if (!string.IsNullOrEmpty(mobile))
                    MobileNumber = Regex.Replace(mobile, @"\s+", "");

                AccountHelper accountHelper = new AccountHelper();
                AccountService service = new AccountService(db, UserManager);
                ApplicationUser user;
                AccountViewModel avm = new AccountViewModel();
                avm.Email = email.Trim();
                avm.CountryID = countryID;
                avm.CityID = cityID;
                avm.FirstName = name.Trim();
                avm.LastName = surname.Trim();
                avm.MemberSubscriptionType = membershipTypeID;
                avm.ownerID = OwnerID;
                avm.Password = password.Trim();
                avm.ConfirmPassword = confirmPassword.Trim();
                avm.MobileNumber = MobileNumber;
                avm.PromoCode = (string.IsNullOrEmpty(promo)) ? "" : promo.Trim();
                avm.StateID = stateID;
                avm.TermsAndConditions = Convert.ToBoolean(Convert.ToInt32(tnc.Trim()));
                IEnumerable<string> Ierrors;
                System.Web.Http.Routing.UrlHelper urlHelper = new System.Web.Http.Routing.UrlHelper(Request);

                if (service.createUser(accountHelper.ToMember(avm), avm.Password, out user, out Ierrors, urlHelper))
                {
                    GameDao gd = new GameDao(db);
                    gd.AddMemberToGame(user);
                    // Do user sign-in
                    string AuthToken = this.Authenticate(avm.Email, avm.Password);
                    // Get Member ID

                    Member member = service.findMember(avm.Email);
                    if (member == null)
                    {
                        errors.Add("Member not generated");
                        return errors.ToList<dynamic>();
                    }

                    response.Add("AuthorizationToken::" + AuthToken);
                    response.Add("MemberID::" + member.MemberID);
                }
                if (Ierrors.Count() > 0)
                    return Ierrors.ToList<dynamic>();
            }
            return response.ToList<dynamic>();
        }

        //[Authorize]

        [HttpPost]
        [ActionName("MemberAccount")]
        [Authorize]
        public Dictionary<string,string> MemberAccount(FormDataCollection formData)
        {
            Dictionary<string,string> result = new Dictionary<string,string>();

            AccountService service = new AccountService(db, UserManager);

            var user = this.User.Identity;
            if (user == null)
            {
                result.Add("Error", "Unathorized");
                return result;
            }

            Member validateMember = service.findMember(user.Name);
            if (validateMember == null)
            {
                result.Add("Error", "Member not found");
                return result;
            }

            bool dataUpdate = false;
            if (formData != null)
            {
                string memberID = formData.Get("memberID");
                if (memberID == null)
                {
                    result.Add("Error","MemberID required.");
                    return result;
                }
                
                int MemberID = Convert.ToInt32(memberID);

                if (MemberID != validateMember.MemberID)
                {
                    result.Add("Error", "MemberID does not match authenticated user");
                    return result;
                }
                //  At this point, the authenticated user and memberID parameter have been successfully verified.

                // get member, Billing & postal (delivery) address and interests
                Member member = db.Members.Find(MemberID);
                Address billAddress = db.Addresses.Where(x => x.MemberID == member.MemberID && x.AddressType.ToLower().Equals("billing")).FirstOrDefault();
                Address delAddress = db.Addresses.Where(x => x.MemberID == member.MemberID && x.AddressType.ToLower().Equals("postal")).FirstOrDefault();
                MemberInterest memberInterest = member.MemberInterests.Where(x => x.MemberID == member.MemberID).FirstOrDefault();

                if (formData["name"] != null || formData["surname"] != null || formData["email"] != null || formData["gender"] != null || formData["dateOfBirth"] != null || formData["mobile"] != null | formData["ethnicGroup"] != null || formData["maritalStatus"] != null || formData["children"] != null || formData["industry"] != null || formData["designation"] != null)
                {
                    dataUpdate = true;
                    member.DateUpdated = DateTime.Now;
                    // Perform member details updates
                    if (formData["name"] != null)
                    {
                        member.FirstName = formData.Get("name").Trim();
                    }

                    if (formData["surname"] != null)
                    {
                        member.LastName = formData.Get("surname").Trim();
                    }

                    if (formData["email"] != null) 
                    {
                        member.EmailAddress = formData.Get("email").Trim();
                    }

                    if (formData["gender"] != null) 
                    {
                        member.Gender = formData.Get("gender").Trim();
                    }

                    if (formData["dateOfBirth"] != null) 
                    {
                        member.DateOfBirth = Convert.ToDateTime(formData.Get("dateOfBirth").Trim());
                    }
                    
                    if (formData["mobile"] != null) 
                    {
                        member.TelephoneMobile = formData.Get("mobile").Trim();
                    }
                    
                    if (formData["ethnicGroup"] != null) 
                    {
                        member.Ethnicity = formData.Get("ethnicGroup").Trim();
                    }
                    
                    if (formData["maritalStatus"] != null)
                    {
                        member.MaritalStatus = formData.Get("maritalStatus").Trim();
                    }
                    
                    if (formData["children"] != null) 
                    {
                        member.Children = formData.Get("children").Trim();
                    }
                    
                    if (formData["industry"] != null) 
                    {
                        member.Industry = formData.Get("industry").Trim();
                    }
                    
                    if (formData["designation"] != null)
                    {
                        member.Designation = formData.Get("designation").Trim();
                    }
                }
                bool addNewDelAddress = false;
                if (formData["countryDelivery"] != null || formData["stateDelivery"] != null || formData["cityDelivery"] != null || formData["postalDelivery"] != null || formData["addressLine1Delivery"] != null || formData["addressLine2Delivery"] != null) 
                {
                    dataUpdate = true;
                    if (delAddress == null)
                    {
                        addNewDelAddress = true;
                        delAddress = new Address();
                        delAddress.AddressType = "Postal";
                        delAddress.DateInserted = DateTime.Now;
                        delAddress.MemberID = member.MemberID;
                        delAddress.USR = "mobile";
                    }

                    delAddress.DateUpdated = DateTime.Now;
                    // perform delivery address information updates
                    if (formData["countryDelivery"] != null)
                    {
                        delAddress.Country = formData.Get("countryDelivery").Trim();
                    } 
                    
                    if (formData["stateDelivery"] != null)
                    {
                        delAddress.StateOrProvince = formData.Get("stateDelivery").Trim();
                    }
                    
                    if (formData["cityDelivery"] != null)
                    {
                        delAddress.CityName = formData.Get("cityDelivery").Trim();
                    }
                    
                    if (formData["postalDelivery"] != null)
                    {
                        delAddress.ZipOrPostalCode = formData.Get("postalDelivery").Trim();
                    }
                    
                    if (formData["addressLine1Delivery"] != null) 
                    {
                        delAddress.AddressLine1 = formData.Get("addressLine1Delivery").Trim();
                    }
                    
                    if (formData["addressLine2Delivery"] != null) 
                    {
                        delAddress.AddressLine2 = formData.Get("addressLine2Delivery").Trim();
                    }
                }

                bool addNewBillAddress = false;
                if (formData["countryBill"] != null || formData["stateBill"] != null || formData["cityBill"] != null || formData["postalBill"] != null || formData["addressLine1Bill"] != null || formData["addressLine2Bill"] != null)
                {
                    dataUpdate = true;
                    if (billAddress == null)
                    {
                        addNewBillAddress = true;
                        billAddress = new Address();
                        billAddress.AddressType = "Billing";
                        billAddress.MemberID = member.MemberID;
                        billAddress.USR = "mobile";
                        billAddress.DateInserted = DateTime.Now;
                    }

                    billAddress.DateUpdated = DateTime.Now;
                    // perform billing address information updates
                    if (formData["countryBill"] != null)
                    {
                        billAddress.Country = formData.Get("countryBill").Trim();
                    }
    
                    if (formData["stateBill"] != null)
                    {
                        billAddress.StateOrProvince = formData.Get("stateBill").Trim();
                    }
                    
                    if (formData["cityBill"] != null)
                    {
                        billAddress.CityName = formData.Get("cityBill").Trim();
                    }
                    
                    if (formData["postalBill"] != null)
                    {
                        billAddress.ZipOrPostalCode = formData.Get("postalBill").Trim();
                    }
                    
                    if (formData["addressLine1Bill"] != null)
                    {
                        billAddress.AddressLine1 = formData.Get("addressLine1Bill").Trim();
                    }

                    if (formData["addressLine2Bill"] != null)
                    {
                        billAddress.AddressLine2 = formData.Get("addressLine2Bill").Trim();
                    }
                }

                bool addNewMemberInterest = false;
                if (formData["interestAutomotive"] != null || formData["interestEntertainment"] != null || formData["interestFashion"] != null || formData["interestArt"] != null || formData["interestDecor"] != null || formData["interestExperiences"] != null || formData["interestAppliances"] != null || formData["interestBeauty"] != null || formData["interestTechnology"] != null || formData["interestToys"] != null || formData["interestDining"] != null || formData["interestTravel"] != null || formData["interestOther"] != null)
                {

                    dataUpdate = true;
                    if (memberInterest == null)
                    {
                        addNewMemberInterest = true;
                        memberInterest = new MemberInterest();
                        memberInterest.MemberID = member.MemberID;
                        memberInterest.DateInserted = DateTime.Now;
                        memberInterest.USR = "mobile";
                        // I don't know what this productCategory table is supposed to represent here.... making it a 1 for now.
                        memberInterest.ProductCategoryID = 1;
                    }

                    memberInterest.DateUpdated = DateTime.Now;
                    // perform interests update
                    if (formData["interestAutomotive"] != null)
                    {
                        memberInterest.MemberInterestAutomotive = Convert.ToBoolean(formData.Get("interestAutomotive").Trim());
                    }

                    if (formData["interestEntertainment"] != null)
                    {
                        memberInterest.MemberInterestEntertainment = Convert.ToBoolean(formData.Get("interestEntertainment").Trim());
                    }
                    
                    if (formData["interestFashion"] != null)
                    {
                        memberInterest.MemberInterestFashionAccessories = Convert.ToBoolean(formData.Get("interestFashion").Trim());
                    }
                    
                    if (formData["interestArt"] != null)
                    {
                        memberInterest.MemberInterestArtCollectibles = Convert.ToBoolean(formData.Get("interestArt").Trim());
                    }
                    
                    if (formData["interestDecor"] != null)
                    {
                        memberInterest.MemberInterestDecorDesign = Convert.ToBoolean(formData.Get("interestDecor").Trim());
                    }
                    
                    if (formData["interestExperiences"] != null)
                    {
                        memberInterest.MemberInterestExperiences = Convert.ToBoolean(formData.Get("interestExperiences").Trim());
                    }
                    
                    if (formData["interestAppliances"] != null)
                    {
                        memberInterest.MemberInterestHomeAppliances = Convert.ToBoolean(formData.Get("interestAppliances").Trim());
                    }
                    
                    if (formData["interestBeauty"] != null)
                    {
                        memberInterest.MemberInterestHealthBeauty = Convert.ToBoolean(formData.Get("interestBeauty").Trim());
                    }
                    
                    if (formData["interestTechnology"] != null)
                    {
                        memberInterest.MemberInterestTechnology= Convert.ToBoolean(formData.Get("interestTechnology").Trim());
                    }
                    
                    if (formData["interestToys"] != null)
                    {
                        memberInterest.MemberInterestToys = Convert.ToBoolean(formData.Get("interestToys").Trim());
                    }
                    
                    if (formData["interestDining"] != null)
                    {
                        memberInterest.MemberInterestWiningDining = Convert.ToBoolean(formData.Get("interestDining").Trim());
                    }
                    
                    if (formData["interestTravel"] != null)
                    {
                        memberInterest.MemberInterestTravel = Convert.ToBoolean(formData.Get("interestTravel").Trim());
                    }

                    if (formData["interestOther"] != null)
                    {
                        memberInterest.MemberInterestOther = formData.Get("interestOther").Trim();
                    }
                }

                if (!dataUpdate)
                {
                    // No updates to process, just return account detail.
                    Dictionary<string, string> accountDetails = new Dictionary<string, string>();

                    accountDetails.Add("name", member.FirstName.Trim());
                    accountDetails.Add("surname", member.LastName.Trim());

                    string email = (member.EmailAddress != null) ? member.EmailAddress.Trim() : "";
                    string gender = (member.Gender != null) ? member.Gender.Trim() : "";
                    string dob =  (member.DateOfBirth != null) ? ((DateTime) member.DateOfBirth).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) : "";
                    string mobile = (member.TelephoneMobile != null) ? member.TelephoneMobile.Trim() : "";
                    string ethnicGroup = (member.Ethnicity != null) ? member.Ethnicity.Trim() : "";
                    string maritalStatus = (member.MaritalStatus != null) ? member.MaritalStatus.Trim() : "";
                    string children = (member.Children != null) ? member.Children.Trim() : "";
                    string industry = (member.Industry != null) ? member.Industry.Trim() : "";
                    string designation = (member.Designation != null) ? member.Designation.Trim() : "";
					
					accountDetails.Add("email", email);
					accountDetails.Add("gender", gender);
                    accountDetails.Add("dateOfBirth", dob);
                    accountDetails.Add("mobile", mobile);
                    accountDetails.Add("ethnicGroup", ethnicGroup);
                    accountDetails.Add("maritalStatus", maritalStatus);
                    accountDetails.Add("children", children);
                    accountDetails.Add("industry", industry);
                    accountDetails.Add("designation", designation);

                    if (billAddress != null)
                    {
						string countryBill = (billAddress.Country != null) ? billAddress.Country.Trim() : "";
						string stateBill = (billAddress.StateOrProvince != null) ? billAddress.StateOrProvince.Trim() : "";
						string cityBill = (billAddress.CityName != null) ? billAddress.CityName.Trim() : "";
						string postalBill = (billAddress.ZipOrPostalCode != null) ? billAddress.ZipOrPostalCode.Trim() : "";
						string addressLine1Bill = (billAddress.AddressLine1 != null) ? billAddress.AddressLine1.Trim() : "";
						string addressLine2Bill = (billAddress.AddressLine2 != null) ? billAddress.AddressLine2.Trim() : "";

                        accountDetails.Add("countryBill",countryBill);
                        accountDetails.Add("stateBill",stateBill);
                        accountDetails.Add("cityBill", cityBill);
                        accountDetails.Add("postalBill",postalBill);
                        accountDetails.Add("addressLine1Bill",addressLine1Bill);
                        accountDetails.Add("addressLine2Bill", addressLine2Bill);
                    }

                    if (delAddress != null)
                    {
						string countryDelivery = (delAddress.Country != null) ? delAddress.Country.Trim() : "";
						string stateDelivery = (delAddress.StateOrProvince != null) ? delAddress.StateOrProvince.Trim() : "";
						string cityDelivery = (delAddress.CityName != null) ? delAddress.CityName.Trim() : "";
						string postalDelivery = (delAddress.ZipOrPostalCode != null) ? delAddress.ZipOrPostalCode.Trim() : "";
						string addressLine1Delivery = (delAddress.AddressLine1 != null) ? delAddress.AddressLine1.Trim() : "";
						string addressLine2Delivery = (delAddress.AddressLine2 != null) ? delAddress.AddressLine2.Trim() : "";

                        accountDetails.Add("countryDelivery",countryDelivery);
                        accountDetails.Add("stateDelivery", stateDelivery);
                        accountDetails.Add("cityDelivery",cityDelivery);
                        accountDetails.Add("postalDelivery",postalDelivery);
                        accountDetails.Add("addressLine1Delivery",addressLine1Delivery);
                        accountDetails.Add("addressLine2Delivery",addressLine2Delivery);
                    }

                    if (memberInterest != null)
                    {
						string interestOther = (memberInterest.MemberInterestOther != null) ? memberInterest.MemberInterestOther.Trim() : "";

                        accountDetails.Add("interestAutomotive",memberInterest.MemberInterestAutomotive.ToString());
                        accountDetails.Add("interestEntertainment",memberInterest.MemberInterestEntertainment.ToString());
                        accountDetails.Add("interestFashion", memberInterest.MemberInterestFashionAccessories.ToString());
                        accountDetails.Add("interestArt",memberInterest.MemberInterestArtCollectibles.ToString());
                        accountDetails.Add("interestDecor",memberInterest.MemberInterestDecorDesign.ToString());
                        accountDetails.Add("interestExperiences",memberInterest.MemberInterestExperiences.ToString() );
                        accountDetails.Add("interestAppliances", memberInterest.MemberInterestHomeAppliances.ToString());
                        accountDetails.Add("interestBeauty",memberInterest.MemberInterestHealthBeauty.ToString());
                        accountDetails.Add("interestTechnology",memberInterest.MemberInterestTechnology.ToString());
                        accountDetails.Add("interestToys", memberInterest.MemberInterestToys.ToString());
                        accountDetails.Add("interestDining",memberInterest.MemberInterestWiningDining.ToString());
                        accountDetails.Add("interestTravel",memberInterest.MemberInterestTravel.ToString());
                        accountDetails.Add("interestOther",interestOther);
                    }

                    return accountDetails;
                }
                else
                {
                    try
                    {
                        if (addNewBillAddress)
                        {
                            db.Addresses.Add(billAddress);
                        }
                        
                        if (addNewDelAddress)
                        {
                            db.Addresses.Add(delAddress);
                        }

                        if (addNewMemberInterest)
                        {
                            db.MemberInterests.Add(memberInterest);
                        }

                        db.SaveChanges();
                        result.Add("Success", "Successfully saved changes");
                        return result; 
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
                                result.Add("Property: " + ve.PropertyName, " Error: " + ve.ErrorMessage);
                            }
                        }
                        result.Add("Error","During Save");
                        return result;
                    }
                }
            }
            else
            {
                result.Add("Input Error","No Valid Data");
            }
            return result;
        }

        [HttpPost]
        [ActionName("ChangePassword")]
        [Authorize]
        public dynamic ChangePassword(FormDataCollection formData)
        {
            List<string> errors = new List<string>();
            List<string> response = new List<string>();
            
            var user = this.User.Identity;
            if (user == null)
            {
                return new { Error = "Unauthorized" };
            }

            AccountService service = new AccountService(db, UserManager);
            Member validateMember = service.findMember(user.Name);
            if (validateMember == null)
            {
                return new { Error = "Member not found" };
            }

            if (formData != null)
            {
                if (formData["oldPassword"] == null)
                {
                    errors.Add("Old Password is required");
                }

                if (formData["newPassword"] == null)
                {
                    errors.Add("New Password is required");
                }

                if (formData["confirmPassword"] == null)
                {
                    errors.Add("Confirm Password is required");
                }

                if (errors.Count > 0)
                {
                    var e = Enumerable.Range(0, errors.Count).ToDictionary(x => "Error" + x, x => errors[x]);
                    return e;
                 }

                string oldPassword = formData.Get("oldPassword");
                string newPassword = formData.Get("newPassword");
                string confirmPassword = formData.Get("confirmPassword");

                // Validation section - cascade errors

                if (string.IsNullOrEmpty(oldPassword))
                {
                    errors.Add("Old Password is required.");
                }

                if (string.IsNullOrEmpty(newPassword))
                {
                    errors.Add("Password is required.");
                }

                if (string.IsNullOrEmpty(confirmPassword))
                {
                    errors.Add("Confirm Password is required.");
                }

                if (newPassword.Trim() != confirmPassword.Trim())
                {
                        errors.Add("Passwords don't match");
                }

                if (errors.Count > 0)
                {
                    var e = Enumerable.Range(0, errors.Count).ToDictionary(x => "Error" + x, x => errors[x]);
                    return e;
                }

                string AuthToken = this.UpdatePassword(oldPassword, newPassword);
                if (AuthToken.ToLower().Contains("error"))
                {
                    errors.Add(AuthToken);
                    var e = Enumerable.Range(0, errors.Count).ToDictionary(x => "Error" + x, x => errors[x]);
                    return e;
                }
                var s = new { AuthToken = AuthToken, Status = "Password changed successfully" };
                return s;
            }
            return new { Error = "No data available (oldPassword, newPassword, confirmPassword expected.)" };
        }


        private string UpdatePassword(string oldPassword, string newPassword)
        {
            IdentityResult result = UserManager.ChangePassword(User.Identity.GetUserId(), oldPassword, newPassword);
            if (result.Succeeded)
            {
                var user = UserManager.FindById(User.Identity.GetUserId());
                string authToken = this.Authenticate(user.Email, newPassword);
                if (authToken.ToLower().Contains("required") || authToken.ToLower().Contains("invalid"))
                {
                    return "Error changing password:" + authToken.ToString();
                }
                return authToken.ToString();
            }
            return "Error changing password";
        }

        
        private string Authenticate(string user, string password)
        {
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            {
                return "Username and Password required";
            }

            var userIdentity = UserManager.FindAsync(user, password).Result;
            if (userIdentity != null)
            {
                var identity = new ClaimsIdentity(Startup.OAuthBearerOptions.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, user));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userIdentity.Id));
                AuthenticationTicket ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
                var currentUtc = new SystemClock().UtcNow;
                ticket.Properties.IssuedUtc = currentUtc;
                ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromMinutes(30));
                string AccessToken = Startup.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);
                return AccessToken;
            }
            return "Invalid username or password";
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
                if (codes.Count() > 0)
                {
                    retvar = codes.First().OwnerID;
                }
            }

            return retvar;
        }


    }

}