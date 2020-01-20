using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Models;

namespace Vaultlife.Dao
{
    public class MemberDao
    {
        private VaultLifeApplicationEntities db;
        public MemberDao(VaultLifeApplicationEntities dbEntities)
        {
            this.db = dbEntities;
        }

        public Member findMember(string username)
        {
            AspNetUser user = db.AspNetUsers.Where(x => x.UserName == username).FirstOrDefault();
            Member member = user != null ? db.Members.Where(m => m.ASPUserId == user.Id).FirstOrDefault() : null;
            return member;

        }

        public MemberInGame findMemberInGame(int id, int gameId)
        {
            return db.MemberInGames.Where(mig => mig.MemberID == id && mig.GameID == gameId).First();
        }

        public bool save(Member member)
        {
            try
            {
               if (db.Members.Count(m => m.MemberID == member.MemberID) == 0)
                {
                    db.Members.Add(member);
                }

                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return false;

            }
            return true;
        }
    }
}