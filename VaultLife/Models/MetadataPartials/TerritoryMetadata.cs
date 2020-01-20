using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
    [MetadataType(typeof(TerritoryMetadata))]
    public partial class Territory
    {
        // Note this class has nothing in it.  It's just here to add the class-level attribute.
    }

    public class TerritoryMetadata
    {
        // Name the field the same as EF named the property - "FirstName" for example.
        // Also, the type needs to match.  Basically just redeclare it.
        // Note that this is a field.  I think it can be a property too, but fields definitely should work.
        [Display(Name = "TerritoryID", ResourceType = typeof(Languaging.Resources))]
        public int TerritoryID;

        [Display(Name = "TerritoryCode", ResourceType = typeof(Languaging.Resources))]
        public string TerritoryCode;

        [Display(Name = "OwnerID", ResourceType = typeof(Languaging.Resources))]
        public int OwnerID;

        [Display(Name = "ContractID", ResourceType = typeof(Languaging.Resources))]
        public int ContractID;

        [Display(Name = "TerritoryName", ResourceType = typeof(Languaging.Resources))]
        public string TerritoryName;

        [Display(Name = "TerritoryDescription", ResourceType = typeof(Languaging.Resources))]
        public string TerritoryDescription;

        [Display(Name = "DateInserted", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateInserted;

        [Display(Name = "DateUpdated", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateUpdated;

        [Display(Name = "USR", ResourceType = typeof(Languaging.Resources))]
        public string USR;
    }
}
