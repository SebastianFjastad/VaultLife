using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaultLifeAdmin.Models;
using VaultLifeAdmin.Dao;
using VaultLifeAdmin.Helpers;
using System.Web.Mvc;

namespace VaultLifeAdmin.Service
{
    public class AccountService
    {
        private VaultLifeApplicationEntities db;
        private MemberDao memberDao;
        private MembershipSubscriptionTypeDao memberSubTypeDao;

        //TODO what does applicationUserManager do ?
        public AccountService(VaultLifeApplicationEntities db)
        {
            this.db = db;
            this.memberDao = new MemberDao(db);
            this.memberSubTypeDao = new MembershipSubscriptionTypeDao(db);

        }

        public Member findMember(String username)
        {
            return memberDao.findMember(username);
        }
    }
}