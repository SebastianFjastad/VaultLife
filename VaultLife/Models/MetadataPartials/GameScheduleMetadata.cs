using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
    [MetadataType(typeof(GameScheduleMetadata))]
    public partial class GameSchedule
    {
        // Note this class has nothing in it.  It's just here to add the class-level attribute.
    }

    public class GameScheduleMetadata
    {
        // Name the field the same as EF named the property - "FirstName" for example.
        // Also, the type needs to match.  Basically just redeclare it.
        // Note that this is a field.  I think it can be a property too, but fields definitely should work.

        [Display(Name = "GameScheduleID", ResourceType = typeof(Languaging.Resources))]
        public int GameScheduleID;

        [Display(Name = "GameScheduleCode", ResourceType = typeof(Languaging.Resources))]
        public string GameScheduleCode;

        [Display(Name = "ScheduledDateTime", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime ScheduledDateTime;

        [Display(Name = "GameID", ResourceType = typeof(Languaging.Resources))]
        public int GameID;

        [Display(Name = "SequenceNumber", ResourceType = typeof(Languaging.Resources))]
        public int SequenceNumber;

        [Display(Name = "DateInserted", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateInserted;

        [Display(Name = "DateUpdated", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateUpdated;

        [Display(Name = "USR", ResourceType = typeof(Languaging.Resources))]
        public string USR;

    }
}