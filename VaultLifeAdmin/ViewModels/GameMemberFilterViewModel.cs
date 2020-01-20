
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using VaultLifeAdmin.Models;

namespace VaultLifeAdmin.ViewModels
{
    public class GameMemberFilterViewModel
    {

        public GameMemberFilterViewModel(GameMemberFilter gmf)
        {
            this.GameMemberFilter = gmf;

        }

        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();

        public Models.GameMemberFilter GameMemberFilter;

        public CountryCity CountryCity { get; set; }//TODO: Add this when we have cities





        public MemberSubscriptionType MemberSubscriptionType
        {
            get
            {
                if (db.MemberSubscriptionTypes.Where(x => x.MemberSubscriptionTypeID == this.GameMemberFilter.MemberSubscriptionTypeID).Count()>0)
                {
                var mst = db.MemberSubscriptionTypes.Where(x => x.MemberSubscriptionTypeID == this.GameMemberFilter.MemberSubscriptionTypeID).First();

                return mst;
                }
            else
            {
                return null;
            }
            }


        }

        public Territory Territory
        {
            get
            {
                if (db.Territories.Where(x => x.TerritoryID == this.GameMemberFilter.Territory).Count() > 0)
                {
                var mst = db.Territories.Where(x => x.TerritoryID == this.GameMemberFilter.Territory).First();

                return mst;
                 }
                else
                {
                    return null;
                }
            }


        }
        public CountryState State
        {
            get
            {
                if (db.CountryStates.Where(x => x.StateID == this.GameMemberFilter.StateID).Count() > 0)
                {
                    var mst = db.CountryStates.Where(x => x.StateID == this.GameMemberFilter.StateID).First();

                    return mst;
                }
                else
                {
                    return null;
                }
            }


        }
        public Country Country
        {
            get
            {
                if (db.Countries.Where(x => x.CountryID == this.GameMemberFilter.CountryID).Count() >0)
                {
                    var mst = db.Countries.Where(x => x.CountryID == this.GameMemberFilter.CountryID).First();

                    return mst;
                }
                else
                {
                    return null;
                }
            }


        }
        public Game Game
        {
            get
            {
                if (db.Games.Where(x => x.GameID == this.GameMemberFilter.GameID).Count()>0)
                {
                    var mst = db.Games.Where(x => x.GameID == this.GameMemberFilter.GameID).First();

                    return mst;
                }
                else
                {
                    return null;
                }
            }


        }

        public string AgeBand
        {
            get
            {
                if (this.GameMemberFilter.AgeBandID != null)
                {
                    var mst = GetAgeGroups().Where(x => x.Value == this.GameMemberFilter.AgeBandID.ToString()).First();
                    return mst.Text;
                }
                else
                {
                    return null;
                }
            }

        }

        public string Gender
        {
            get
            {
                if (this.GameMemberFilter.GenderID != null)
                {
                    var mst = GetGenders().Where(x => x.Value == this.GameMemberFilter.GenderID.ToString()).First();
                    return mst.Text;
                }
                else
                {
                    return null;
                }
            }

        }


        private List<SelectListItem> GetAgeGroups()
        {
            var AgeGroups = new List<SelectListItem>();

            AgeGroups.Add(new SelectListItem
            {
                Text = "Any",
                Value = "0"
            });
            AgeGroups.Add(new SelectListItem
            {
                Text = "18-25",
                Value = "1"
            });
            AgeGroups.Add(new SelectListItem
            {
                Text = "25-35",
                Value = "2",
                Selected = true
            });
            AgeGroups.Add(new SelectListItem
            {
                Text = "35-45",
                Value = "3"
            });
            AgeGroups.Add(new SelectListItem
            {
                Text = "45-55",
                Value = "4"
            });
            AgeGroups.Add(new SelectListItem
            {
                Text = " Older Than 55",
                Value = "5"
            });
            return AgeGroups;
        }
        private List<SelectListItem> GetGenders()
        {
            var Genders = new List<SelectListItem>();
            Genders.Add(new SelectListItem
            {
                Text = "Any",
                Value = "0"

            });
            Genders.Add(new SelectListItem
            {
                Text = "Male",
                Value = "1"
            });
            Genders.Add(new SelectListItem
            {
                Text = "Female",
                Value = "2",

            });
            return Genders;
        }

    }
}