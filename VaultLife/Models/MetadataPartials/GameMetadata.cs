using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
      [MetadataType(typeof(GameMetadata))]
      public partial class Game
      {
           // Note this class has nothing in it.  It's just here to add the class-level attribute.
      }

      public class GameMetadata
      {
          // Name the field the same as EF named the property - "FirstName" for example.
          // Also, the type needs to match.  Basically just redeclare it.
          // Note that this is a field.  I think it can be a property too, but fields definitely should work.

          [Display(Name = "GameID", ResourceType = typeof(Languaging.Resources))]
          public int GameID;

          [Display(Name = "GameCode", ResourceType = typeof(Languaging.Resources))]
          public string GameCode;

          [Display(Name = "GameTypeID", ResourceType = typeof(Languaging.Resources))]
          public int GameTypeID;

          [Display(Name = "GameName", ResourceType = typeof(Languaging.Resources))]
          public string GameName;

          [Display(Name = "GameDescription", ResourceType = typeof(Languaging.Resources))]
          public string GameDescription;

          [Display(Name = "DateInserted", ResourceType = typeof(Languaging.Resources))]
          public System.DateTime DateInserted;

          [Display(Name = "DateUpdated", ResourceType = typeof(Languaging.Resources))]
          public System.DateTime DateUpdated;

          [Display(Name = "USR", ResourceType = typeof(Languaging.Resources))]
          public string USR;
      }
}
