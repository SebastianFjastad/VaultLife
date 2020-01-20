using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
  
      [MetadataType(typeof(AspNetUserLoginMetadata))]
      public partial class AspNetUserLogin
      {
           // Note this class has nothing in it.  It's just here to add the class-level attribute.
      }
  
      public class AspNetUserLoginMetadata
      {
          // Name the field the same as EF named the property - "FirstName" for example.
          // Also, the type needs to match.  Basically just redeclare it.
          // Note that this is a field.  I think it can be a property too, but fields definitely should work.
  
          [Display(Name = "LoginProvider", ResourceType = typeof(Languaging.Resources))]
          public string LoginProvider;

          [Display(Name = "ProviderKey", ResourceType = typeof(Languaging.Resources))]
          public string ProviderKey;

          [Display(Name = "UserId", ResourceType = typeof(Languaging.Resources))]
          public string UserId;
      }
  }
