using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
    [MetadataType(typeof(InteractionTypeMetadata))]
    public partial class InteractionType
    {
        // Note this class has nothing in it.  It's just here to add the class-level attribute.
    }

    public class InteractionTypeMetadata
    {
        // Name the field the same as EF named the property - "FirstName" for example.
        // Also, the type needs to match.  Basically just redeclare it.
        // Note that this is a field.  I think it can be a property too, but fields definitely should work.

        [Display(Name = "InteractionTypeID", ResourceType = typeof(Languaging.Resources))]
        public int InteractionTypeID;

        [Display(Name = "InteractionTypeCode", ResourceType = typeof(Languaging.Resources))]
        public string InteractionTypeCode;

        [Display(Name = "InteractionTypeName", ResourceType = typeof(Languaging.Resources))]
        public string InteractionTypeName;

        [Display(Name = "InteractionTypeDescription", ResourceType = typeof(Languaging.Resources))]
        public string InteractionTypeDescription;

        [Display(Name = "DateInserted", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateInserted;

        [Display(Name = "DateUpdated", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateUpdated;

        [Display(Name = "USR", ResourceType = typeof(Languaging.Resources))]
        public string USR;

    }
}
