using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
    [MetadataType(typeof(TerritoryDefinitionMetadata))]
    public partial class TerritoryDefinition
    {
        // Note this class has nothing in it.  It's just here to add the class-level attribute.
    }

    public class TerritoryDefinitionMetadata
    {
        // Name the field the same as EF named the property - "FirstName" for example.
        // Also, the type needs to match.  Basically just redeclare it.
        // Note that this is a field.  I think it can be a property too, but fields definitely should work.
        [Display(Name = "TerritoryDefinitionID", ResourceType = typeof(Languaging.Resources))]
        public int TerritoryDefinitionID;

        [Display(Name = "TerritoryDefinitionCode", ResourceType = typeof(Languaging.Resources))]
        public string TerritoryDefinitionCode;

        [Display(Name = "TerritoryID", ResourceType = typeof(Languaging.Resources))]
        public int TerritoryID;

        [Display(Name = "ZipOrPostalCode", ResourceType = typeof(Languaging.Resources))]
        public string ZipOrPostalCode;

        [Display(Name = "IPAddress", ResourceType = typeof(Languaging.Resources))]
        public string IPAddress;

        [Display(Name = "PhysicalCoordinates", ResourceType = typeof(Languaging.Resources))]
        public string PhysicalCoordinates;

        [Display(Name = "DateInserted", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateInserted;

        [Display(Name = "DateUpdated", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateUpdated;

        [Display(Name = "USR", ResourceType = typeof(Languaging.Resources))]
        public string USR;

    }
}
