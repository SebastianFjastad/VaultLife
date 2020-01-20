using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
    [MetadataType(typeof(GameTypeOwnedMetadata))]
    public partial class GameTypeOwned
    {
        // Note this class has nothing in it.  It's just here to add the class-level attribute.
    }

    public class GameTypeOwnedMetadata
    {
        // Name the field the same as EF named the property - "FirstName" for example.
        // Also, the type needs to match.  Basically just redeclare it.
        // Note that this is a field.  I think it can be a property too, but fields definitely should work.

        [Display(Name = "GameTypeOwnedID", ResourceType = typeof(Languaging.Resources))]
        public int GameTypeOwnedID;

        [Display(Name = "GameTypeOwnedCode", ResourceType = typeof(Languaging.Resources))]
        public string GameTypeOwnedCode;

        [Display(Name = "OwnerID", ResourceType = typeof(Languaging.Resources))]
        public int OwnerID;

        [Display(Name = "GameTypeID", ResourceType = typeof(Languaging.Resources))]
        public int GameTypeID;

        [Display(Name = "ContractID", ResourceType = typeof(Languaging.Resources))]
        public int ContractID;

        [Display(Name = "DateInserted", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateInserted;

        [Display(Name = "DateUpdated", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateUpdated;

        [Display(Name = "USR", ResourceType = typeof(Languaging.Resources))]
        public string USR;

    }
}
