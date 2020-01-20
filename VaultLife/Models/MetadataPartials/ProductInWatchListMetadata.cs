using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
    [MetadataType(typeof(ProductInWatchListMetadata))]
    public partial class ProductInWatchList
    {
        // Note this class has nothing in it.  It's just here to add the class-level attribute.
    }

    public class ProductInWatchListMetadata
    {
        // Name the field the same as EF named the property - "FirstName" for example.
        // Also, the type needs to match.  Basically just redeclare it.
        // Note that this is a field.  I think it can be a property too, but fields definitely should work.
        [Display(Name = "ProductInWatchListID", ResourceType = typeof(Languaging.Resources))]
        public int ProductInWatchListID;

        [Display(Name = "MemberID", ResourceType = typeof(Languaging.Resources))]
        public int MemberID;

        [Display(Name = "ProductID", ResourceType = typeof(Languaging.Resources))]
        public int ProductID;

        [Display(Name = "DateInserted", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateInserted;

        [Display(Name = "DateUpdated", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateUpdated;

        [Display(Name = "USR", ResourceType = typeof(Languaging.Resources))]
        public string USR;
    }
}
