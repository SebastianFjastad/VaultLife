using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Models;

namespace Vaultlife.Dao
{
    public class TransactionDao
    {
        private VaultLifeApplicationEntities db;
        public TransactionDao(VaultLifeApplicationEntities db)
        {
            this.db = db;
        }

        public List<Transaction> findWonAndPaidTransactions(int memberId)
        {
            var transac = from m in db.MemberInGames
                          join g in db.Games on m.GameID equals g.GameID
                          where (m.WinIndicator == true && m.PaymentIndicator == true) && m.MemberID == memberId
                          select new { DateUpdated = m.DateUpdated,
                              Usr = m.USR,
                              GameName = g.GameName,
                              GameDescription = g.GameDescription};
            IEnumerable<Transaction> trans =  transac.ToList().Select(t => new Transaction { Usr = t.Usr, GameName = t.GameName, GameDescription = t.GameDescription, DateUpdated = Convert.ToDateTime(t.DateUpdated).ToString("d") });
            return trans.ToList();
        }
    }
}