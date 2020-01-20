using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
    [MetadataType(typeof(ProductPlayedMetadata))]
    public partial class ProductPlayed
    {
        // Note this class has nothing in it.  It's just here to add the class-level attribute.
    }

    public class ProductPlayedMetadata
    {
        // Name the field the same as EF named the property - "FirstName" for example.
        // Also, the type needs to match.  Basically just redeclare it.
        // Note that this is a field.  I think it can be a property too, but fields definitely should work.
        [Display(Name = "ProductPlayedID", ResourceType = typeof(Languaging.Resources))]
        public int ProductPlayedID;

        [Display(Name = "ProductInGameID", ResourceType = typeof(Languaging.Resources))]
        public Nullable<int> ProductInGameID;

        [Display(Name = "MemberInGameID", ResourceType = typeof(Languaging.Resources))]
        public Nullable<int> MemberInGameID;

        [Display(Name = "VoucherID", ResourceType = typeof(Languaging.Resources))]
        public Nullable<int> VoucherID;

        [Display(Name = "SerialNumberID", ResourceType = typeof(Languaging.Resources))]
        public Nullable<int> SerialNumberID;

        [Display(Name = "DeliveryAgentName", ResourceType = typeof(Languaging.Resources))]
        public string DeliveryAgentName;

        [Display(Name = "DeliveryAgentReferenceNumber", ResourceType = typeof(Languaging.Resources))]
        public string DeliveryAgentReferenceNumber;

    }

}