using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
    [MetadataType(typeof(NextGameMetadata))]
    public partial class NextGame
    {
        // Note this class has nothing in it.  It's just here to add the class-level attribute.
    }

    public class NextGameMetadata
    {
        // Name the field the same as EF named the property - "FirstName" for example.
        // Also, the type needs to match.  Basically just redeclare it.
        // Note that this is a field.  I think it can be a property too, but fields definitely should work.

        [Display(Name = "NextGameID", ResourceType = typeof(Languaging.Resources))]
        public int NextGameID;

        [Display(Name = "GameID", ResourceType = typeof(Languaging.Resources))]
        public int GameID;

        [Display(Name = "DateInserted", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateInserted;

        [Display(Name = "DateUpdated", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateUpdated;

        [Display(Name = "USR", ResourceType = typeof(Languaging.Resources))]
        public string USR;

    }
}
