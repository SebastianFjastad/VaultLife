using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



namespace Vaultlife.Models
{
    [MetadataType(typeof(AspNetRoleMetadata))]
    public partial class AspNetRole
    {
         // Note this class has nothing in it.  It's just here to add the class-level attribute.
    }

    public class AspNetRoleMetadata
    {
        // Name the field the same as EF named the property - "FirstName" for example.
        // Also, the type needs to match.  Basically just redeclare it.
        // Note that this is a field.  I think it can be a property too, but fields definitely should work.

        [Display(Name = "Id", ResourceType = typeof(Languaging.Resources))]
        public string Id;

        [Display(Name = "Name", ResourceType = typeof(Languaging.Resources))]
        public string Name;

    }
}
