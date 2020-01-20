using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Models;
using System.Data.Entity;

namespace Vaultlife.Helpers
{    
    public static class MemberHelper
    {
        public static Member GetLoggedInMember(string UserName, VaultLifeApplicationEntities db)
        {
            var members = db.Members.Include(x=>x.MemberSubscriptionType).Where(s => s.EmailAddress == UserName);

            if (members != null && members.Count() > 0)
            {
                return members.First();
            }
            return null;
            
        }
    }
}