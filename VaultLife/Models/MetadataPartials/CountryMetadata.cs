using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
    [MetadataType(typeof(CountryMetadata))]
    public partial class Country
    {
        // Note this class has nothing in it.  It's just here to add the class-level attribute.
    }
  
    public class CountryMetadata
    {
        // Name the field the same as EF named the property - "FirstName" for example.
        // Also, the type needs to match.  Basically just redeclare it.
        // Note that this is a field.  I think it can be a property too, but fields definitely should work.
  
        [Display(Name = "CountryID", ResourceType = typeof(Languaging.Resources))]
        public int CountryID;
        [Display(Name = "CountryName", ResourceType = typeof(Languaging.Resources))]
        public string CountryName;
    }
}
