using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Models;

namespace Vaultlife.Dao
{
    public class MembershipSubscriptionTypeDao
    {
        private VaultLifeApplicationEntities db;
        public MembershipSubscriptionTypeDao(VaultLifeApplicationEntities dbEntities)
        {
            this.db = dbEntities;
        }

        public double findAmount(int membershipSubscriptionTypeId)
        {
            return Convert.ToDouble(db.MemberSubscriptionTypes.Where(type => type.MemberSubscriptionTypeID == membershipSubscriptionTypeId).First().amount);
        }

        public List<MemberSubscriptionType> findAll()
        {
            return db.MemberSubscriptionTypes.ToList();
        }

    }
}