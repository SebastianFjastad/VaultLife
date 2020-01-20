using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Languaging;

namespace VaultLifeAdmin.Models
{
    public class MembersInGamesModel
    {

        [Display(Name = "AgeGroup", ResourceType = typeof(Languaging.Resources))]
        public string AgeGroup { get; set; }
        
        [Display(Name = "Gender", ResourceType = typeof(Languaging.Resources))]
        public string Gender {get; set;}


        [Display(Name = "Ethnicity", ResourceType = typeof(Languaging.Resources))]
        public string Ethnicity { get; set; }

        [Display(Name = "CountryID", ResourceType = typeof(Languaging.Resources))]
        public int CountryID { get; set; }

        [Display(Name = "StateID", ResourceType = typeof(Languaging.Resources))]
        public int StateID { get; set; }

        [Display(Name = "MemberSubscriptionTypeID", ResourceType = typeof(Languaging.Resources))]
        public int MemberSubscriptionTypeID { get; set; }
        public int MembersCount { get; set; }
        public int GameID { get; set; }
       
    }
}