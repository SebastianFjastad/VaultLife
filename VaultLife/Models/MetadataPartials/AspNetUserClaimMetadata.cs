using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
  
      [MetadataType(typeof(AspNetUserClaimMetadata))]
      public partial class AspNetUserClaim
      {
           // Note this class has nothing in it.  It's just here to add the class-level attribute.
      }
  
      public class AspNetUserClaimMetadata
      {
          // Name the field the same as EF named the property - "FirstName" for example.
          // Also, the type needs to match.  Basically just redeclare it.
          // Note that this is a field.  I think it can be a property too, but fields definitely should work.
  
          [Display(Name = "Id", ResourceType = typeof(Languaging.Resources))]
          public int Id;
          
          [Display(Name = "UserId", ResourceType = typeof(Languaging.Resources))]
          public string UserId;
          
          [Display(Name = "ClaimType", ResourceType = typeof(Languaging.Resources))]
          public string ClaimType;
          
          [Display(Name = "ClaimValue", ResourceType = typeof(Languaging.Resources))]
          public string ClaimValue;
	      
      }
  }    
    