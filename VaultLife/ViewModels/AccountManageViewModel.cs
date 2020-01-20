using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PaymentService;
using Vaultlife.Models;
using System.Web.Mvc;




namespace Vaultlife.ViewModels
{
    public class AccountManageViewModel
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();
        public Models.Member Member { get; set; }

        public Models.Address Address { get; set; }

        public Models.MemberInterest MemberInterest { get; set; }

        public AccountManageViewModel()
        {
            this.DateOfBirth = DateTime.Now;
        }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "Email")]
        //The regular expression below implements the official RFC 2822 standard for email addresses. Using this regular expression in actual applications is NOT recommended. It is shown to illustrate that with regular expressions there's always a trade-off between what's exact and what's practical.
        //[RegularExpression("^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$", ErrorMessage = "Invalid e-mail.")]
        [RegularExpression(@"^([\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+\.)*[\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$", ErrorMessage = "Please enter correct email address")]
        [Required(ErrorMessage = "E-mail must be entered")]
        [DataType(DataType.EmailAddress)]
        public virtual string Email { get; set; }

        [Display(Name = "Confirm email")]
        [Required(ErrorMessage = "Repeat e-mail must be entered")]
        [DataType(DataType.EmailAddress)]
        [System.ComponentModel.DataAnnotations.Compare("Email", ErrorMessage = "E-mail and Repeat e-mail must be identically entered.")]
        public virtual string ConfirmEmail { get; set; }


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

        [Required]
        [Display(Name = "Gender", ResourceType = typeof(Languaging.Resources))]
        public string Gender { get; set; }

        [Required]
        [ValidateAge(18)]
        [Display(Name = "DateOfBirth", ResourceType = typeof(Languaging.Resources))]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Ethnicity", ResourceType = typeof(Languaging.Resources))]
        public string Ethnicity { get; set; }

        [Display(Name = "Children")]
        public string Children { get; set; }

        [Display(Name = "MaritalStatus")]
        public string MaritalStatus { get; set; }

        [Display(Name = "Industry")]
        public string Industry { get; set; }

        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [Display(Name = "CountryID", ResourceType = typeof(Languaging.Resources))]
        public int? CountryID { get; set; }

        [Display(Name = "StateID", ResourceType = typeof(Languaging.Resources))]
        public int? StateID { get; set; }


        [Display(Name = "CityID")]
        public int? CityID { get; set; }


        [Display(Name = "CountryBillID")]
        public int? CountryBillID { get; set; }


        [Display(Name = "StateBillID")]
        public int? StateBillID { get; set; }

        [Display(Name = "CityBillID")]
        public int? CityBillID { get; set; }

        ////[Required]
        //[Display(Name = "IDNumber")]
        //public string IDNumber { get; set; }


        [Required]
        [Display(Name = "AddressLineOne")]
        public string AddressLineOne { get; set; }

        [Required]
        [Display(Name = "AddressLineTwo")]
        public string AddressLineTwo { get; set; }

        [Required]
        [Display(Name = "AddressZip")]
        public string AddressZip { get; set; }

        [Required]
        [Display(Name = "AddressBillLineOne")]
        public string AddressBillLineOne { get; set; }

        [Required]
        [Display(Name = "AddressBillLineTwo")]
        public string AddressBillLineTwo { get; set; }

        [Required]
        [Display(Name = "AddressBillZip")]
        public string AddressBillZip { get; set; }

        [Display(Name = "MemberInterestsAutomotive")]
        public bool MemberInterestsAutomotive { get; set; }

        [Display(Name = "MemberInterestsArtCollectibles")]
        public bool MemberInterestsArtCollectibles { get; set; }

        [Display(Name = "MemberInterestsHomeAppliances")]
        public bool MemberInterestsHomeAppliances { get; set; }

        [Display(Name = "MemberInterestsToys")]
        public bool MemberInterestsToys { get; set; }

        [Display(Name = "MemberInterestsEntertainment")]
        public bool MemberInterestsEntertainment { get; set; }

        [Display(Name = "MemberInterestsDecoreDesign")]
        public bool MemberInterestsDecoreDesign { get; set; }

        [Display(Name = "MemberInterestsHealthBeauty")]
        public bool MemberInterestsHealthBeauty { get; set; }

        [Display(Name = "MemberInterestsTravel")]
        public bool MemberInterestsTravel { get; set; }

        [Display(Name = "MemberInterestsFashionAccessories")]
        public bool MemberInterestsFashionAccessories { get; set; }

        [Display(Name = "MemberInterestsExperiences")]
        public bool MemberInterestsExperiences { get; set; }

        [Display(Name = "MemberInterestsTechnology")]
        public bool MemberInterestsTechnology { get; set; }

        [Display(Name = "MemberInterestsWiningDining")]
        public bool MemberInterestsWiningDining { get; set; }

        [Display(Name = "MemberInterestsOther")]
        public string MemberInterestsOther { get; set; }

        [Display(Name = "Transactions")]
        public List<Vaultlife.Models.Transaction> Transactions { get; set; }

        public List<SelectListItem> allIndustry()
        {
            List<SelectListItem> Industry = new List<SelectListItem>();
            Industry.Add(new SelectListItem { Text = "Admin", Value = "Admin" });
            Industry.Add(new SelectListItem { Text = "Agriculture", Value = "Agriculture" });
            Industry.Add(new SelectListItem { Text = "Arts & Entertainment", Value = "Arts & Entertainment" });
            Industry.Add(new SelectListItem { Text = "Beauty", Value = "Beauty" });
            Industry.Add(new SelectListItem { Text = "Botanical", Value = "Botanical" });
            Industry.Add(new SelectListItem { Text = "Building & Construction", Value = "Building & Construction" });
            Industry.Add(new SelectListItem { Text = "Business & Management", Value = "Business & Management" });
            Industry.Add(new SelectListItem { Text = "Commercial Services", Value = "Commercial Services" });
            Industry.Add(new SelectListItem { Text = "Design", Value = "Design" });
            Industry.Add(new SelectListItem { Text = "Distribution", Value = "Distribution" });
            Industry.Add(new SelectListItem { Text = "Education", Value = "Education" });
            Industry.Add(new SelectListItem { Text = "Engineering", Value = "Engineering" });
            Industry.Add(new SelectListItem { Text = "Financial", Value = "Financial" });
            Industry.Add(new SelectListItem { Text = "FMCG, Retail & Wholesale", Value = "FMCG, Retail & Wholesale" });
            Industry.Add(new SelectListItem { Text = "Government", Value = "Government" });
            Industry.Add(new SelectListItem { Text = "Hospitality & Restaurant", Value = "Hospitality & Restaurant" });
            Industry.Add(new SelectListItem { Text = "Human Resources", Value = "Human Resources" });
            Industry.Add(new SelectListItem { Text = "Information Technology", Value = "Information Technology" });
            Industry.Add(new SelectListItem { Text = "Legal", Value = "Legal" });
            Industry.Add(new SelectListItem { Text = "Manufacturing", Value = "Manufacturing" });
            Industry.Add(new SelectListItem { Text = "Maritime", Value = "Maritime" });
            Industry.Add(new SelectListItem { Text = "Marketing", Value = "Marketing" });
            Industry.Add(new SelectListItem { Text = "Media", Value = "Media" });
            Industry.Add(new SelectListItem { Text = "Medical", Value = "Medical" });
            Industry.Add(new SelectListItem { Text = "Mining", Value = "Mining" });
            Industry.Add(new SelectListItem { Text = "Motor", Value = "Motor" });
            Industry.Add(new SelectListItem { Text = "Petrochemical", Value = "Petrochemical" });
            Industry.Add(new SelectListItem { Text = "Property", Value = "Property" });
            Industry.Add(new SelectListItem { Text = "Safety & Security", Value = "Safety & Security" });
            Industry.Add(new SelectListItem { Text = "Sales", Value = "Sales" });
            Industry.Add(new SelectListItem { Text = "Science & Technology", Value = "Science & Technology" });
            Industry.Add(new SelectListItem { Text = "Social & Community", Value = "Social & Community" });
            Industry.Add(new SelectListItem { Text = "Sport & Fitness", Value = "Sport & Fitness" });
            Industry.Add(new SelectListItem { Text = "Telecommunication", Value = "Telecommunication" });
            Industry.Add(new SelectListItem { Text = "Transport & Aviation", Value = "Transport & Aviation" });
            Industry.Add(new SelectListItem { Text = "Travel & Tourism", Value = "Travel & Tourism" });
            return Industry;
        }


        public List<SelectListItem> allDesignations()
        {
            List<SelectListItem> Designation = new List<SelectListItem>();
            Designation.Add(new SelectListItem { Text = "Professional", Value = "Professional" });
            Designation.Add(new SelectListItem { Text = "Manager", Value = "Manager" });
            Designation.Add(new SelectListItem { Text = "Director", Value = "Director" });
            Designation.Add(new SelectListItem { Text = "Entrepreneur", Value = "Entrepreneur" });
            return Designation;
        }

        public List<SelectListItem> allMaritalStatus()
        {
            List<SelectListItem> MaritalStatus = new List<SelectListItem>();
            MaritalStatus.Add(new SelectListItem { Text = "Single", Value = "Single" });
            MaritalStatus.Add(new SelectListItem { Text = "Engaged", Value = "Engaged" });
            MaritalStatus.Add(new SelectListItem { Text = "Married", Value = "Married" });
            MaritalStatus.Add(new SelectListItem { Text = "Divorced", Value = "Divorced" });
            return MaritalStatus;
        }

        public List<SelectListItem> allChildren()
        {
            List<SelectListItem> Children = new List<SelectListItem>();
            Children.Add(new SelectListItem { Text = "None", Value = "None" });
            Children.Add(new SelectListItem { Text = "1", Value = "1" });
            Children.Add(new SelectListItem { Text = "2", Value = "2" });
            Children.Add(new SelectListItem { Text = "3", Value = "3", Selected = true });
            Children.Add(new SelectListItem { Text = "4", Value = "4" });
            Children.Add(new SelectListItem { Text = "5", Value = "5" });
            Children.Add(new SelectListItem { Text = "More than 5", Value = "More than 5" });
            return Children;
        }

        public List<SelectListItem> allEthnics()
        {
            List<SelectListItem> Ethnics = new List<SelectListItem>();
            Ethnics.Add(new SelectListItem { Text = "Any", Value = null });
            Ethnics.Add(new SelectListItem { Text = "African", Value = "African" });
            Ethnics.Add(new SelectListItem { Text = "Coloured", Value = "Coloured" });
            Ethnics.Add(new SelectListItem { Text = "Indian", Value = "Indian" });
            Ethnics.Add(new SelectListItem { Text = "White", Value = "White" });
            return Ethnics;
        }
    }
}