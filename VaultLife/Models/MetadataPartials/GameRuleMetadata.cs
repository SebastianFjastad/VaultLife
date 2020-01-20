using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
      [MetadataType(typeof(GameRuleMetadata))]
      public partial class GameRule 
      {
           // Note this class has nothing in it.  It's just here to add the class-level attribute.
      }

      public class GameRuleMetadata
      {
          // Name the field the same as EF named the property - "FirstName" for example.
          // Also, the type needs to match.  Basically just redeclare it.
          // Note that this is a field.  I think it can be a property too, but fields definitely should work.

          [Display(Name = "GameRuleID", ResourceType = typeof(Languaging.Resources))]
          public int GameRuleID;

          [Display(Name = "GameRuleCode", ResourceType = typeof(Languaging.Resources))]
          public string GameRuleCode;

          [Display(Name = "GameID", ResourceType = typeof(Languaging.Resources))]
          public int GameID;

          [Display(Name = "FilterCriteria", ResourceType = typeof(Languaging.Resources))]        
          public string FilterCriteria;

          [Display(Name = "Schedule", ResourceType = typeof(Languaging.Resources))]
          public int Schedule;

          [Display(Name = "ChainGameRuleID", ResourceType = typeof(Languaging.Resources))]
          public int ChainGameRuleID;

          [Display(Name = "GameRuleDetail", ResourceType = typeof(Languaging.Resources))]
          public string GameRuleDetail;

          [Display(Name = "DateInserted", ResourceType = typeof(Languaging.Resources))]
          public System.DateTime DateInserted;

          [Display(Name = "DateUpdated", ResourceType = typeof(Languaging.Resources))]
          public System.DateTime DateUpdated;

          [Display(Name = "USR", ResourceType = typeof(Languaging.Resources))]
          public string USR;
    }
}