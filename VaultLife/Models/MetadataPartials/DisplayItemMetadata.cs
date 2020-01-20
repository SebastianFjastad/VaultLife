using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
    [MetadataType(typeof(DisplayItemMetadata))]

  
    public class DisplayItemMetadata
    {
        // Name the field the same as EF named the property - "FirstName" for example.
        // Also, the type needs to match.  Basically just redeclare it.
        // Note that this is a field.  I think it can be a property too, but fields definitely should work.

        [Display(Name = "DisplayItemID", ResourceType = typeof(Languaging.Resources))]
        public int DisplayItemID;

        [Display(Name = "ProductID", ResourceType = typeof(Languaging.Resources))]
        public int ProductID;

        [Display(Name = "DisplayItemType", ResourceType = typeof(Languaging.Resources))]
        public string DisplayItemType;

        [Display(Name = "DisplayItemURL", ResourceType = typeof(Languaging.Resources))]
        public string DisplayItemURL;

        [Display(Name = "Active", ResourceType = typeof(Languaging.Resources))]
        public bool Active;

        [Display(Name = "DeleteOnSave", ResourceType = typeof(Languaging.Resources))]
        public bool DeleteOnSave;
    }
}

