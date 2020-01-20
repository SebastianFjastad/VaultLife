using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
    [MetadataType(typeof(ProductMetadata))]
    public partial class Product
    {
        // Note this class has nothing in it.  It's just here to add the class-level attribute.
    }

    public class ProductMetadata
    {
        // Name the field the same as EF named the property - "FirstName" for example.
        // Also, the type needs to match.  Basically just redeclare it.
        // Note that this is a field.  I think it can be a property too, but fields definitely should work.
        [Display(Name = "ProductID", ResourceType = typeof(Languaging.Resources))]
        public int ProductID;

        [StringLength(50, ErrorMessage = "ProductSKUCode cannot be longer than 50 characters.")]
        [Display(Name = "ProductSKUCode", ResourceType = typeof(Languaging.Resources))]
        public string ProductSKUCode;

        [Display(Name = "OwnerID", ResourceType = typeof(Languaging.Resources))]
        public int OwnerID;

        [Display(Name = "ContractID", ResourceType = typeof(Languaging.Resources))]
        public int ContractID;

        [StringLength(600, ErrorMessage = "ProductName cannot be longer than 600 characters.")]
        [Display(Name = "ProductName", ResourceType = typeof(Languaging.Resources))]
        public string ProductName;


        [StringLength(600, ErrorMessage = "ProductDescription cannot be longer than 600 characters.")]
        [Display(Name = "ProductDescription", ResourceType = typeof(Languaging.Resources))]
        [DataType(DataType.MultilineText)]
        public string ProductDescription;

        [Display(Name = "DateInserted", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateInserted;

        [Display(Name = "DateUpdated", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateUpdated;

        [Display(Name = "USR", ResourceType = typeof(Languaging.Resources))]
        public string USR;

        [Display(Name = "IsExclusive", ResourceType = typeof(Languaging.Resources))]
        public bool IsExclusive;

        [Display(Name = "SOH", ResourceType = typeof(Languaging.Resources))]
        public Nullable<int> SOH;

        [Display(Name = "AvailableSOH", ResourceType = typeof(Languaging.Resources))]
        public Nullable<int> AvailableSOH;
    }
}
