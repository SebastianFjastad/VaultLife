using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
    [MetadataType(typeof(MemberAcquisitionCampaignMetadata))]
    public partial class MemberAcquisitionCampaign
    {
        // Note this class has nothing in it.  It's just here to add the class-level attribute.
    }

    public class MemberAcquisitionCampaignMetadata
    {
        // Name the field the same as EF named the property - "FirstName" for example.
        // Also, the type needs to match.  Basically just redeclare it.
        // Note that this is a field.  I think it can be a property too, but fields definitely should work.
        [Display(Name = "MemberAcquisitionCampaignCode", ResourceType = typeof(Languaging.Resources))]
        public string MemberAcquisitionCampaignCode;

        [Display(Name = "OwnerID", ResourceType = typeof(Languaging.Resources))]
        public int OwnerID;

        [Display(Name = "ContractID", ResourceType = typeof(Languaging.Resources))]
        public int ContractID;

        [Display(Name = "MemberAcquisitionCampaignName", ResourceType = typeof(Languaging.Resources))]
        public string MemberAcquisitionCampaignName;

        [Display(Name = "MemberAcquisitionCampaignDescription", ResourceType = typeof(Languaging.Resources))]
        public string MemberAcquisitionCampaignDescription;

        [Display(Name = "DateInserted", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateInserted;

        [Display(Name = "DateUpdated", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateUpdated;

        [Display(Name = "USR", ResourceType = typeof(Languaging.Resources))]
        public string USR;

    
    }
}
