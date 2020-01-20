using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Models;

namespace Vaultlife.Service
{

    public class UserService
    {
        private VaultLifeApplicationEntities db;
        private ApplicationUserManager userManager;

        public UserService(VaultLifeApplicationEntities db, ApplicationUserManager userManager)
        {
            this.userManager = userManager;
           this.db = db;
        }

        public Boolean hasPassword(String username)
        {
            if (db.AspNetUsers.Where(u => u.Email == username).Count() > 0)
            {
                AspNetUser user = db.AspNetUsers.Where(u => u.Email == username).First();
                return user.PasswordHash != null;
            }
            return true;
        }
    }


}