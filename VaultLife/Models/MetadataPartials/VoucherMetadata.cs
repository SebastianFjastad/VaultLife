using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
    [MetadataType(typeof(VoucherMetadata))]
    public partial class Voucher
    {
        // Note this class has nothing in it.  It's just here to add the class-level attribute.
    }

    public class VoucherMetadata
    {
        // Name the field the same as EF named the property - "FirstName" for example.
        // Also, the type needs to match.  Basically just redeclare it.
        // Note that this is a field.  I think it can be a property too, but fields definitely should work.

        [Display(Name = "VoucherID", ResourceType = typeof(Languaging.Resources))]
        public int VoucherID;

        [Display(Name = "VoucherNumber", ResourceType = typeof(Languaging.Resources))]
        public string VoucherNumber;

        [Display(Name = "ProductLocationID", ResourceType = typeof(Languaging.Resources))]
        public int ProductLocationID;

        [Display(Name = "Used", ResourceType = typeof(Languaging.Resources))]
        public bool Used;

        [Display(Name = "DateInserted", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateInserted;

        [Display(Name = "DateUsed", ResourceType = typeof(Languaging.Resources))]
        public Nullable<System.DateTime> DateUsed;
    }
}
