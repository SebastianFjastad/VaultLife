using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
    [MetadataType(typeof(MemberOwnedMetadata))]
    public partial class MemberOwned
    {
        // Note this class has nothing in it.  It's just here to add the class-level attribute.
    }

    public class MemberOwnedMetadata
    {
        // Name the field the same as EF named the property - "FirstName" for example.
        // Also, the type needs to match.  Basically just redeclare it.
        // Note that this is a field.  I think it can be a property too, but fields definitely should work.

        [Display(Name = "MemberOwnedID", ResourceType = typeof(Languaging.Resources))]    
        public int MemberOwnedID;

        [Display(Name = "MemberID", ResourceType = typeof(Languaging.Resources))]    
        public int MemberID;

        [Display(Name = "OwnerID", ResourceType = typeof(Languaging.Resources))]    
        public int OwnerID;

        [Display(Name = "TerritoryID", ResourceType = typeof(Languaging.Resources))]    
        public int TerritoryID;

        [Display(Name = "MemberAcquisitionCampaignID", ResourceType = typeof(Languaging.Resources))]    
        public int MemberAcquisitionCampaignID;

        [Display(Name = "DateFrom", ResourceType = typeof(Languaging.Resources))]    
        public System.DateTime DateFrom;

        [Display(Name = "DateTo", ResourceType = typeof(Languaging.Resources))]    
        public System.DateTime DateTo;

        [Display(Name = "ExclusiveIndicator", ResourceType = typeof(Languaging.Resources))]    
        public bool ExclusiveIndicator;

        [Display(Name = "DateInserted", ResourceType = typeof(Languaging.Resources))]    
        public System.DateTime DateInserted;

        [Display(Name = "DateUpdated", ResourceType = typeof(Languaging.Resources))]    
        public System.DateTime DateUpdated;

        [Display(Name = "USR", ResourceType = typeof(Languaging.Resources))]    
        public string USR;
    

    }
}
