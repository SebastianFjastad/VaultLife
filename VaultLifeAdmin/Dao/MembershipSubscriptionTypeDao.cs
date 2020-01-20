using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaultLifeAdmin.Models;

namespace VaultLifeAdmin.Dao
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