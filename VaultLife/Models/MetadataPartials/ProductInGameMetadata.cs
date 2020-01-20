using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
    [MetadataType(typeof(ProductInGameMetadata))]
    public partial class ProductInGame
    {
        // Note this class has nothing in it.  It's just here to add the class-level attribute.
    }

    public class ProductInGameMetadata
    {
        // Name the field the same as EF named the property - "FirstName" for example.
        // Also, the type needs to match.  Basically just redeclare it.
        // Note that this is a field.  I think it can be a property too, but fields definitely should work.
        [Display(Name = "ProductInGameID", ResourceType = typeof(Languaging.Resources))]
        public int ProductInGameID;

        [Display(Name = "ProductID", ResourceType = typeof(Languaging.Resources))]
        public int ProductID;

        [Display(Name = "GameID", ResourceType = typeof(Languaging.Resources))]
        public int GameID;

        [Display(Name = "Quantity", ResourceType = typeof(Languaging.Resources))]
        public int Quantity;

        [Display(Name = "CurrencyID", ResourceType = typeof(Languaging.Resources))]
        public int CurrencyID;

        [Display(Name = "DateInserted", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateInserted;

        [Display(Name = "DateUpdated", ResourceType = typeof(Languaging.Resources))]
        public System.DateTime DateUpdated;

        [Display(Name = "USR", ResourceType = typeof(Languaging.Resources))]
        public string USR;

    }
}
