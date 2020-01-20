using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
  
      [MetadataType(typeof(AspNetUserMetadata))]
      public partial class AspNetUser
      {
           // Note this class has nothing in it.  It's just here to add the class-level attribute.
      }
  
      public class AspNetUserMetadata
      {
          // Name the field the same as EF named the property - "FirstName" for example.
          // Also, the type needs to match.  Basically just redeclare it.
          // Note that this is a field.  I think it can be a property too, but fields definitely should work.
  
          [Display(Name = "Id", ResourceType = typeof(Languaging.Resources))]
          public string Id;
          
          [Display(Name = "Email", ResourceType = typeof(Languaging.Resources))]
          public string Email;
          
          [Display(Name = "EmailConfirmed", ResourceType = typeof(Languaging.Resources))]
          public bool EmailConfirmed;
          
          [Display(Name = "PasswordHash", ResourceType = typeof(Languaging.Resources))]
          public string PasswordHash;
          
          [Display(Name = "SecurityStamp", ResourceType = typeof(Languaging.Resources))]
          public string SecurityStamp;
          
          [Display(Name = "PhoneNumber", ResourceType = typeof(Languaging.Resources))]
          public string PhoneNumber;
          
          [Display(Name = "PhoneNumberConfirmed", ResourceType = typeof(Languaging.Resources))]
          public bool PhoneNumberConfirmed;
          
          [Display(Name = "TwoFactorEnabled", ResourceType = typeof(Languaging.Resources))]
          public bool TwoFactorEnabled;
          
          [Display(Name = "LockoutEndDateUtc", ResourceType = typeof(Languaging.Resources))]
          public Nullable<System.DateTime> LockoutEndDateUtc;
          
          [Display(Name = "LockoutEnabled", ResourceType = typeof(Languaging.Resources))]
          public bool LockoutEnabled;
          
          [Display(Name = "AccessFailedCount", ResourceType = typeof(Languaging.Resources))]
          public int AccessFailedCount;
          
          [Display(Name = "UserName", ResourceType = typeof(Languaging.Resources))]
          public string UserName;
         
      }
  }    
    