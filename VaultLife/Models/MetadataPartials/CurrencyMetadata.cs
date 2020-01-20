using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
    [MetadataType(typeof(CurrencyMetadata))]
    public partial class Currency
    {
        // Note this class has nothing in it.  It's just here to add the class-level attribute.
    }
  
    public class CurrencyMetadata
    {
        // Name the field the same as EF named the property - "FirstName" for example.
        // Also, the type needs to match.  Basically just redeclare it.
        // Note that this is a field.  I think it can be a property too, but fields definitely should work.

        [Display(Name = "CurrencyID", ResourceType = typeof(Languaging.Resources))]
        public int CurrencyID;

        [Display(Name = "CurrencyCode", ResourceType = typeof(Languaging.Resources))]
        public string CurrencyCode;

        [Display(Name = "CurrencySymbol", ResourceType = typeof(Languaging.Resources))]
        public string CurrencySymbol;

        [Display(Name = "CountryID", ResourceType = typeof(Languaging.Resources))]
        public Nullable<int> CountryID;
    }
}

