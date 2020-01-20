using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
    [MetadataType(typeof(GameTypeMetadata))]
    public partial class GameType
    {
        // Note this class has nothing in it.  It's just here to add the class-level attribute.
    }

    public class GameTypeMetadata
    {
        // Name the field the same as EF named the property - "FirstName" for example.
        // Also, the type needs to match.  Basically just redeclare it.
        // Note that this is a field.  I think it can be a property too, but fields definitely should work.

        [Display(Name = "GameTypeID", ResourceType = typeof(Languaging.Resources))]
        public int GameTypeID;

        [StringLength(20, ErrorMessage = "GameTypeCode cannot be longer than 20 characters.")]
        [Display(Name = "GameTypeCode", ResourceType = typeof(Languaging.Resources))]
        public string GameTypeCode;

        [StringLength(255, ErrorMessage = "GameTypeName cannot be longer than 255 characters.")]
        [Display(Name = "GameTypeName", ResourceType = typeof(Languaging.Resources))]
        public string GameTypeName;

        [StringLength(255, ErrorMessage = "GameTypeDescription cannot be longer than 255 characters.")]
        [Display(Name = "GameTypeDescription", ResourceType = typeof(Languaging.Resources))]
        public string GameTypeDescription;

        [Display(Name = "DateInserted", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateInserted;

        [Display(Name = "DateUpdated", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateUpdated;

        [Display(Name = "USR", ResourceType = typeof(Languaging.Resources))]
        public string USR;
    }
}