using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
      [MetadataType(typeof(EventMetadata))]
      public partial class Event
      {
           // Note this class has nothing in it.  It's just here to add the class-level attribute.
      }

      public class EventMetadata
      {
          // Name the field the same as EF named the property - "FirstName" for example.
          // Also, the type needs to match.  Basically just redeclare it.
          // Note that this is a field.  I think it can be a property too, but fields definitely should work.

          [Display(Name = "", ResourceType = typeof(Languaging.Resources))]
          public int EventID;

          [Display(Name = "", ResourceType = typeof(Languaging.Resources))]
          public string EventCode;

          [Display(Name = "", ResourceType = typeof(Languaging.Resources))]
          public string EventName;

          [Display(Name = "", ResourceType = typeof(Languaging.Resources))]
          public string EventDescription;

          [Display(Name = "", ResourceType = typeof(Languaging.Resources))]
          public System.DateTime EventDate;

          [Display(Name = "", ResourceType = typeof(Languaging.Resources))]
          public System.DateTime DateInserted;

          [Display(Name = "", ResourceType = typeof(Languaging.Resources))]
          public System.DateTime DateUpdated;

          [Display(Name = "", ResourceType = typeof(Languaging.Resources))]
          public string USR;
      }
}
