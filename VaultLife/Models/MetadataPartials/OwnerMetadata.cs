using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
    [MetadataType(typeof(OwnerMetadata))]
    public partial class Owner
    {
        // Note this class has nothing in it.  It's just here to add the class-level attribute.
    }

    public class OwnerMetadata
    {
        // Name the field the same as EF named the property - "FirstName" for example.
        // Also, the type needs to match.  Basically just redeclare it.
        // Note that this is a field.  I think it can be a property too, but fields definitely should work.

        [Display(Name = "OwnerID", ResourceType = typeof(Languaging.Resources))]
        public int OwnerID;

        [Display(Name = "OwnerCode", ResourceType = typeof(Languaging.Resources))]
        public string OwnerCode;

        [Display(Name = "OwnerName", ResourceType = typeof(Languaging.Resources))]
        public string OwnerName;

        [Display(Name = "OwnerType", ResourceType = typeof(Languaging.Resources))]
        public string OwnerType;

        [Display(Name = "BankingDetailBank", ResourceType = typeof(Languaging.Resources))]
        public string BankingDetailBank;

        [Display(Name = "BankingDetailAccountNumber", ResourceType = typeof(Languaging.Resources))]
        public string BankingDetailAccountNumber;

        [Display(Name = "BankingDetailAccountType", ResourceType = typeof(Languaging.Resources))]
        public string BankingDetailAccountType;

        [Display(Name = "BankingDetailBranchCode", ResourceType = typeof(Languaging.Resources))]
        public string BankingDetailBranchCode;

        [Display(Name = "BankingDetailBranchName", ResourceType = typeof(Languaging.Resources))]
        public string BankingDetailBranchName;

        [Display(Name = "BankingDetailDefaultReference", ResourceType = typeof(Languaging.Resources))]
        public string BankingDetailDefaultReference;

        [Display(Name = "EmailAddress", ResourceType = typeof(Languaging.Resources))]
        public string EmailAddress;

        [Display(Name = "ContactPerson", ResourceType = typeof(Languaging.Resources))]
        public string ContactPerson;

        [Display(Name = "TelephoneOffice", ResourceType = typeof(Languaging.Resources))]
        public string TelephoneOffice;

        [Display(Name = "TelephoneMobile", ResourceType = typeof(Languaging.Resources))]
        public string TelephoneMobile;

        [Display(Name = "AddressID", ResourceType = typeof(Languaging.Resources))]
        public int AddressID;

        [Display(Name = "DateInserted", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateInserted;

        [Display(Name = "DateUpdated", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateUpdated;

        [Display(Name = "USR", ResourceType = typeof(Languaging.Resources))]
        public string USR;

    }
}
