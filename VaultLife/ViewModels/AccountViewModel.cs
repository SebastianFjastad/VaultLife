using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vaultlife.Models;


namespace Vaultlife.ViewModels
{
    public class AccountViewModel
    {
        public int? OwnerID;

       
        //private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities(); 
        public Models.AspNetUser AspNetUser { get; set; }

        public Models.Member Member { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression(@"^([\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+\.)*[\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$", ErrorMessage = "Please enter correct email address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [RegularExpression(@"[a-zA-Z\.\'\-_\s]{1,40}", ErrorMessage = "Your first name contains unrecognized characters")]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"[a-zA-Z\.\'\-_\s]{1,40}", ErrorMessage = "Your last name contains unrecognized characters")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        /*[Required] */
        [RegularExpression(@"^[+0][0-9_ ]+$", ErrorMessage = "Please enter a valid mobile number")]
        [Display(Name = "MobileNumber")]
        public string MobileNumber { get; set; }

       
        [Display(Name = "PromoCode")]
        public string PromoCode { get; set; }

        [Display(Name = "CountryID", ResourceType = typeof(Languaging.Resources))]
        public int CountryID { get; set; }

        [Display(Name = "StateID", ResourceType = typeof(Languaging.Resources))]
        public int? StateID { get; set; }

        [Display(Name = "CityID", ResourceType = typeof(Languaging.Resources))]
        public int? CityID { get; set; }

        [Display(Name = "MemberSubscriptionTypeID", ResourceType = typeof(Languaging.Resources))]
        public int MemberSubscriptionType { get; set; }

        [Display(Name = "IdentityTypeID")]
        public string IdentityTypeID { get; set; }

        [MustBeTrue(ErrorMessage = "Please accept the terms of use and privacy policy")]
        [Display(Name = "TermsAndConditions")]
        public bool TermsAndConditions { get; set; }


        public int? ownerID { get; set; }
    }




}