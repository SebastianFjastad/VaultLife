using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
    [MetadataType(typeof(CountryStateMetadata))]
    public partial class CountryState
    {
        // Note this class has nothing in it.  It's just here to add the class-level attribute.
    }
  
    public class CountryStateMetadata
    {
        // Name the field the same as EF named the property - "FirstName" for example.
        // Also, the type needs to match.  Basically just redeclare it.
        // Note that this is a field.  I think it can be a property too, but fields definitely should work.
  
    [Display(Name = "StateID", ResourceType = typeof(Languaging.Resources))]
    public int StateID;

    [Display(Name = "CountryID", ResourceType = typeof(Languaging.Resources))]
    public Nullable<int> CountryID;

    [Display(Name = "StateName", ResourceType = typeof(Languaging.Resources))]
    public string StateName;
    }
}
