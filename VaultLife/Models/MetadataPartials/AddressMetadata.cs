using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Languaging;


namespace Vaultlife.Models
{
    [MetadataType(typeof(AddressMetadata))]
    public partial class Address
    {
         // Note this class has nothing in it.  It's just here to add the class-level attribute.
    }

    public class AddressMetadata {
         // Name the field the same as EF named the property - "FirstName" for example.
  // Also, the type needs to match.  Basically just redeclare it.
  // Note that this is a field.  I think it can be a property too, but fields definitely should work.

        [Display(Name = "AddressID", ResourceType = typeof(Languaging.Resources))]
        public int AddressID;

        [Display(Name = "AddressType", ResourceType = typeof(Languaging.Resources))]
        public string AddressType;

        [Display(Name = "AddressLine1", ResourceType = typeof(Languaging.Resources))]
        public string AddressLine1 ;

        [Display(Name = "AddressLine2", ResourceType = typeof(Languaging.Resources))]
        public string AddressLine2 ;

        [Display(Name = "AddressLine3", ResourceType = typeof(Languaging.Resources))]
        public string AddressLine3 ;

        [Display(Name = "Country", ResourceType = typeof(Languaging.Resources))]
        public string Country ;

        [Display(Name = "StateOrProvince", ResourceType = typeof(Languaging.Resources))]
        public string StateOrProvince ;

        [Display(Name = "ZipOrPostalCode", ResourceType = typeof(Languaging.Resources))]
        public string ZipOrPostalCode ;

        [Display(Name = "DateInserted", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateInserted ;

        [Display(Name = "DateUpdated", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateUpdated ;

        [Display(Name = "USR", ResourceType = typeof(Languaging.Resources))]
        public string USR ;

    }
}