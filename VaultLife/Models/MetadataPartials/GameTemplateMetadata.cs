using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
    [MetadataType(typeof(GameTemplateMetadata))]
    public partial class GameTemplate
    {
        // Note this class has nothing in it.  It's just here to add the class-level attribute.
    }

    public class GameTemplateMetadata
    {
        // Name the field the same as EF named the property - "FirstName" for example.
        // Also, the type needs to match.  Basically just redeclare it.
        // Note that this is a field.  I think it can be a property too, but fields definitely should work.

        [Display(Name = "GameTemplateID", ResourceType = typeof(Languaging.Resources))]
        public int GameTemplateID;

        [Display(Name = "GameTemplateCode", ResourceType = typeof(Languaging.Resources))]
        public string GameTemplateCode;

        [Display(Name = "GameTypeID", ResourceType = typeof(Languaging.Resources))]
        public int GameTypeID;

        [Display(Name = "GameRuleCode", ResourceType = typeof(Languaging.Resources))]
        public string GameRuleCode;

        [Display(Name = "GameRuleDetail", ResourceType = typeof(Languaging.Resources))]
        public string GameRuleDetail;

        [Display(Name = "OrderInGame", ResourceType = typeof(Languaging.Resources))]
        public int OrderInGame;

        [Display(Name = "DateInserted", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateInserted;

        [Display(Name = "DateUpdated", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateUpdated;

        [Display(Name = "USR", ResourceType = typeof(Languaging.Resources))]
        public string USR;

    }
}