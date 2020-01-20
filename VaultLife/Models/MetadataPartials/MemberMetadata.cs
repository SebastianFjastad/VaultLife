using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
    [MetadataType(typeof(MemberMetadata))]
    public partial class Member
    {
        // Note this class has nothing in it.  It's just here to add the class-level attribute.
    }

    public class MemberMetadata
    {
        // Name the field the same as EF named the property - "FirstName" for example.
        // Also, the type needs to match.  Basically just redeclare it.
        // Note that this is a field.  I think it can be a property too, but fields definitely should work.

        [Display(Name = "MemberID", ResourceType = typeof(Languaging.Resources))]
        public int MemberID;
        
        [Display(Name = "MemberSubscriptionTypeID", ResourceType = typeof(Languaging.Resources))]
        public int MemberSubscriptionTypeID;
        
        [Display(Name = "IdentityType", ResourceType = typeof(Languaging.Resources))]
        public string IdentityType;
        
        [Display(Name = "EmailAddress", ResourceType = typeof(Languaging.Resources))]
        public string EmailAddress;
        
        [Display(Name = "TelephoneHome", ResourceType = typeof(Languaging.Resources))]
        public string TelephoneHome;
        
        [Display(Name = "TelephoneOffice", ResourceType = typeof(Languaging.Resources))]
        public string TelephoneOffice;
        
        [Display(Name = "TelephoneMobile", ResourceType = typeof(Languaging.Resources))]
        public string TelephoneMobile;
        
        [Display(Name = "Gender", ResourceType = typeof(Languaging.Resources))]
        public string Gender;
        
        [Display(Name = "Ethnicity", ResourceType = typeof(Languaging.Resources))]
        public string Ethnicity;
        
        [Display(Name = "DateOfBirth", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateOfBirth;
        
        [Display(Name = "ActiveIndicator", ResourceType = typeof(Languaging.Resources))]
        public bool ActiveIndicator;
        
        [Display(Name = "RenewalDate", ResourceType = typeof(Languaging.Resources))]
        public Nullable<System.DateTime> RenewalDate;
        
        [Display(Name = "AddressID", ResourceType = typeof(Languaging.Resources))]
        public int AddressID;
        
        [Display(Name = "DateInserted", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateInserted;
        
        [Display(Name = "DateUpdated", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateUpdated;
        
        [Display(Name = "USR", ResourceType = typeof(Languaging.Resources))]
        public string USR;
        
        [Display(Name = "ASPUserId", ResourceType = typeof(Languaging.Resources))]
        public string ASPUserId;
        
        [Display(Name = "CountryID", ResourceType = typeof(Languaging.Resources))]
        public Nullable<int> CountryID;
        
        [Display(Name = "StateID", ResourceType = typeof(Languaging.Resources))]
        public Nullable<int> StateID;
        
        [Display(Name = "FirstName", ResourceType = typeof(Languaging.Resources))]
        public string FirstName;
        
        [Display(Name = "LastName", ResourceType = typeof(Languaging.Resources))]
        public string LastName;
    
    }
}
