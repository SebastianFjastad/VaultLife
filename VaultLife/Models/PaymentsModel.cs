using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Vaultlife.Models
{
    public class PaymentsModel
    {
        [Display(Name = "CardType", ResourceType = typeof(Languaging.Resources))]
        [Required]
        public string CardType { get; set; }

        [Display(Name = "CardNumber", ResourceType = typeof(Languaging.Resources))]
        [Required]
        public string CardNumber { get; set; }

        [Display(Name = "NameOnCard", ResourceType = typeof(Languaging.Resources))]
        [Required]
        public string NameOnCard { get; set; }

        [Display(Name = "ExpiryDateM", ResourceType = typeof(Languaging.Resources))]
        [Required]
        public string ExpiryDateM { get; set; }
        [Display(Name = "ExpiryDateY", ResourceType = typeof(Languaging.Resources))]
        [Required]
        public string ExpiryDateY { get; set; }

        [Display(Name = "CVCNumber", ResourceType = typeof(Languaging.Resources))]
        [Required]
        public String CVCNumber { get; set; }

    }
}