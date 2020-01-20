using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
    [MetadataType(typeof(MemberInterestMetadata))]
    public partial class MemberInterest
    {
        // Note this class has nothing in it.  It's just here to add the class-level attribute.
    }

    public class MemberInterestMetadata
    {
        // Name the field the same as EF named the property - "FirstName" for example.
        // Also, the type needs to match.  Basically just redeclare it.
        // Note that this is a field.  I think it can be a property too, but fields definitely should work.

        [Display(Name = "MemberInterestID", ResourceType = typeof(Languaging.Resources))]
        public int MemberInterestID;

        [Display(Name = "MemberID", ResourceType = typeof(Languaging.Resources))]
        public int MemberID;

        [Display(Name = "ProductCategoryID", ResourceType = typeof(Languaging.Resources))]
        public int ProductCategoryID;

        [Display(Name = "DateInserted", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateInserted;

        [Display(Name = "DateUpdated", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateUpdated;

        [Display(Name = "USR", ResourceType = typeof(Languaging.Resources))]
        public string USR;
    

    }
}
