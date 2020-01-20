using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Models;
using System.Data.Entity;
using Vaultlife.ViewModels;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Vaultlife.ViewModels;
namespace Vaultlife.Dao
{
    public class GameDao
    {
        private VaultLifeApplicationEntities db;

        public GameDao(VaultLifeApplicationEntities db)
        {
            this.db = db;
        }

        public IEnumerable<Game> findGlobalGames(int? MemberId)
        {
            if (MemberId == null || MemberId == 0)
            {
                return db.Games.Where(g => g.Global == true && (g.GameState.ToLower() == "prepare" || g.GameState.ToLower() == "released" || g.GameState.ToLower() == "active"));   // MAtt - Put back membershipID match TODO refactor membersubtype checking

            }
            else { 
            Member member = db.Members.Where(m => m.MemberID == MemberId).First();

            List<int?> GameIDs = db.GetMissingGames(member.MemberID).ToList();
            
            return db.Games.Where(g => GameIDs.Contains(g.GameID));
            }
            
        }

        public void removeGameFromWatchList(int memberId, int gameId) {
           if (db.ProductInWatchLists.Where(piw => piw.GameID == gameId && piw.MemberID == memberId).Count() > 0) { 
                ProductInWatchList pinWatchlist = db.ProductInWatchLists.Where(piw => piw.GameID == gameId && piw.MemberID == memberId).First();
                db.ProductInWatchLists.Remove(pinWatchlist);
            }
        }

        public MemberInGame findMemberInGame(int gameId, int memberId)
        {
            //TODO weird.. does a massive select for everything
            return db.MemberInGames.Where(mig => mig.GameID == gameId && mig.MemberID == memberId).First();
        }

        public Dictionary<int , int> findMemberInGame(int gameId)
        {
            Dictionary<int, int> mapping = new Dictionary<int,int>();
            var members = db.getMembersInGame(gameId);
            return members.ToDictionary(m => m.Memberid, m => m.memberinGameID);
        }

        public ProductInGame findProductInGame(int gameId)
        {
            return db.ProductInGames.Where(pig => pig.GameID == gameId).First();
        }

        public void AddMemberToGame(ApplicationUser User) {
            Member member = db.Members.Where(m=>m.ASPUserId.Equals(User.Id)).First();
            List<Game> games = findGlobalGames(member.MemberID).ToList();
                
            foreach( Game game in games ) {
                MemberInGame mig = new MemberInGame();
                mig.GameID = game.GameID;
                mig.MemberID = member.MemberID;
                mig.DateInserted = DateTime.Now;
                mig.DateUpdated = DateTime.Now;
                mig.WinIndicator = false;
                mig.PaymentIndicator = false;
                mig.USR = member.EmailAddress;
                db.MemberInGames.Add(mig);
                db.SaveChanges();
            }

        }


        public List<GameListViewModel> getOtherGames(int MemberId, int? membershipsubscriptionTypeID)
        {

            List<Game> games = new List<Game>();
            if (MemberId == 0)
            {
                games = db.Games.Where(x => x.MemberSubscriptionType == membershipsubscriptionTypeID && (x.GameState.ToLower() == "prepare" || x.GameState.ToLower() == "released" || x.GameState.ToLower() == "active")).Take(10).ToList();
            }
            else {

                games = db.Games.Where(x => x.MemberSubscriptionType == membershipsubscriptionTypeID && x.MemberInGames.Count(y => y.MemberID == MemberId) > 0 && (x.GameState.ToLower() == "prepare" || x.GameState.ToLower() == "released" || x.GameState.ToLower() == "active")).Take(10).ToList(); 
                
            }
            return games.Select(g => new GameListViewModel(g.GameID, db.ProductInfoes.Where(p => p.GameID == g.GameID).First().ImageID)).ToList();            
        }



        public IEnumerable<Game> findComingSoonGames(int? MemberId)
        {


            List<int?> ComingSoonGameID = db.FindComingSoonGames(MemberId).ToList();
            List<Game> ListofGames = new List<Game>(); ;
            foreach (int i in ComingSoonGameID)
            {
                Game game = db.Games.Find(i);
                ListofGames.Add(game);

            }
           
            //IEnumerable<Game> comingSoonGames = db.Games.Include(x => x.ProductInGames).Where(x => (x.GameState.ToLower() == "prepare" || x.GameState.ToLower() == "released" || x.GameState.ToLower() == "active") && x.MemberInGames.Count(y => y.MemberID == MemberId) > 0);
            return ListofGames;
        }


        public IEnumerable<Game> findComingSoonGames()
        {


            List<int?> ComingSoonGameID = db.FindComingSoonGames(0).ToList();
            List<Game> ListofGames = new List<Game>(); ;
            foreach (int i in ComingSoonGameID)
            {
                Game game = db.Games.Find(i);
                ListofGames.Add(game);

            }

            //IEnumerable<Game> comingSoonGames = db.Games.Include(x => x.ProductInGames).Where(x => (x.GameState.ToLower() == "prepare" || x.GameState.ToLower() == "released" || x.GameState.ToLower() == "active") && x.MemberInGames.Count(y => y.MemberID == MemberId) > 0);
            return ListofGames;
        }

        public List<dynamic> findRecentWinners()
        {
            var RecentGames = db.getRecentSales;
            return RecentGames.ToList<dynamic>();
        }

        public int? isWinner(int GameID, int MemberID)
        {
            return  db.IsUserWinner(GameID, MemberID).First();
        }

           public int getMaxTransactionTime(int GameId)
        {
            return 0;// Convert.ToInt32(db.GetMaxTransactionTime(GameId));
        }
        

        public IEnumerable<ProductPlayed> findProductPlayeds(int productInGameID)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            List<ProductPlayed> prodPlays = new List<ProductPlayed>();

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("findProductPlayeds", con))
                {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@productInGameID", SqlDbType.Int).Value = productInGameID;
 
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ProductPlayed prodPlay = new ProductPlayed();
                                prodPlay.ProductPlayedID = reader.GetInt32(0);
                                prodPlay.Winner = reader.GetInt32(1);
                                prodPlays.Add(prodPlay);
                            }

                        }
                        reader.Close();

                }
                con.Close();
            }
            return prodPlays;                                                                  

        }

        public void save(Game newGame)
        {
            db.Games.Add(newGame);
            db.SaveChanges();
        }

        public ICollection<MemberInGame> findLosers(int gameId)
        {
            return db.MemberInGames.Where(mig => mig.GameID == gameId && mig.ProductPlayeds.First().Winner == 1).ToList(); 
        }

        public Game findGame(int? gameID)
        {
            return db.Games.Find(gameID);
        }

        public void save()
        {
            db.SaveChanges();
        }

        public IEnumerable<MembersWhoPlayed> findMembersWhoPlayed(int gameID)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            
            List<MembersWhoPlayed> played = new List<MembersWhoPlayed>();
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select * from memberswhoplayed where gameId = " + gameID + " and winindicator != 2", con))
                {

                        cmd.CommandType = CommandType.Text;
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MembersWhoPlayed membersWhoPlayed = new MembersWhoPlayed();
                                membersWhoPlayed.MemberID = reader.GetInt32(0);
                                membersWhoPlayed.GameID = reader.GetInt32(1);
                                membersWhoPlayed.WinIndicator = reader.GetInt32(2);
                                membersWhoPlayed.ClickInterval = reader.GetInt32(3);
                                membersWhoPlayed.PaymentIndicator = reader.GetBoolean(4);
                                played.Add(membersWhoPlayed);
                            }

                        }
                        reader.Close();
                }
                con.Close();
               
            }
            return played;
        }

    }
}