using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Vaultlife.Service.Rules;
using Vaultlife.Models;
using Vaultlife.Service;
using System.Collections.Concurrent;
using System.Collections;
using Vaultlife.Dao;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace Vaultlife.Models.Games
{
    public class FFFGame : GameEntity
    {
        public FFFGame(VaultLifeApplicationEntities db, IScheduler scheduler, Game game)
            : base(db, scheduler, game)
        {
     
//            if (!game.GameType.GameTypeName.Equals("FastestFinger"))
 //           {
 //               throw new InvalidOperationException();
  //          }
            
        }

        public override GameResolveStatus resolvePotentialWinners()
        {
            GameDao gameDao = new GameDao(db);
            IEnumerable<ProductPlayed> gameResults = gameDao.findProductPlayeds(game.ProductInGames.First().ProductInGameID);  //winner = 0 and orderd highest to lowest
            resolve(gameResults);
            return game.GameState.ToUpper() != "COMPLETED" ? GameResolveStatus.OUTSTANDING : GameResolveStatus.RESOLVED;   
        }

        private void resolve(IEnumerable<ProductPlayed> gameResults)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction(); 
                using (SqlCommand cmd = new SqlCommand("UpdateWinIndictor", con, transaction))
                {
                    SqlParameter prodParam = cmd.Parameters.Add("@productPlayedID", SqlDbType.VarChar);
                    SqlParameter winParam = cmd.Parameters.Add("@Winner", SqlDbType.VarChar);
                    foreach (ProductPlayed play in gameResults)
                    {
                        play.Winner = 1;
                        if (numWinnersLeft > 0)
                        {
                            play.Winner = 2;
                            // play.ProductInGame.Quantity = play.ProductInGame.Quantity - 1;
                            numWinnersLeft--;
                        }

                        cmd.CommandType = CommandType.StoredProcedure;
                        prodParam.Value = play.ProductPlayedID;
                        winParam.Value = play.Winner;
                        cmd.ExecuteNonQuery();
                    }
                    
                }
                transaction.Commit();
                con.Close();
               
                this.game.NumberOfWinners = numWinnersLeft;
                db.SaveChanges();
            }
            
        }

        public override void makeReleased()
        {
            ICollection<GameRule> rules = game.GameRules;

           /* GameRule notifyRule = rules.FirstOrDefault(r => r.GameRuleCode.Equals("NotifiyGameParticipants"));
            GameRule startRule = rules.FirstOrDefault(r => r.GameRuleCode.Equals("StartGame"));
            GameRule prepStartTime = notifyRule == null ? startRule : notifyRule; */
            GameRule prepStartTime = rules.FirstOrDefault(r => r.GameRuleCode.Equals("PrepareGameRule"));
            PrepareGameRule prepareRule = new PrepareGameRule(prepStartTime, this); 
            prepareRule.schedule(scheduler);
            
            base.makeReleased();
        }

        public override void makeReady()
        {
            base.makeReady();
        }

        public override void makeActive()
        {
            ICollection<GameRule> rules = game.GameRules;
            GameRule potentialGameRule = rules.First(r => r.GameRuleCode.Equals("ResolvePotentialWinners")); 
            ResolvePotentialWinners potentialRule = new ResolvePotentialWinners(potentialGameRule, this);
            potentialRule.schedule(scheduler);

            //TODO potential + 1 min?
            GameRule actualGameRule = rules.First(r => r.GameRuleCode.Equals("ResolveActualWinners"));
            ResolveActualWinners actualRule = new ResolveActualWinners(actualGameRule, this);  
            actualRule.schedule(scheduler);
            base.makeActive();
        }

        public override void makePrepareActive()
        {
            ICollection<GameRule> rules = game.GameRules;
            GameRule notifyRule = rules.FirstOrDefault(r => r.GameRuleCode.Equals("NotifiyGameParticipants")); 
            if (notifyRule != null)
            {
                NotifyGameParticipantsRule rule = new NotifyGameParticipantsRule(notifyRule, this); 
                rule.schedule(scheduler);
            }
            GameRule startGameRule = rules.First(r => r.GameRuleCode.Equals("StartGame"));
            StartGameRule startRule = new StartGameRule(startGameRule, this); 
            startRule.schedule(scheduler);
            base.makePrepareActive();
        }

        public override void makeCompleted()
        {    
            if (game.NextGameID != null)
            {
                Game newGame = db.Games.Find(game.NextGameID);
                if (newGame != null)
                {
                    GameDao gameDao = new GameDao(db);
                    IEnumerable<MembersWhoPlayed> nonWinnersQ = gameDao.findMembersWhoPlayed(game.GameID); 

                    List<MemberInGame> nonWinners = new List<MemberInGame>();

                    foreach (MembersWhoPlayed nonWinner in nonWinnersQ)
                    {
                        MemberInGame memberingame = new MemberInGame();
                        memberingame.GameID = newGame.GameID;
                        memberingame.MemberID = nonWinner.MemberID;
                        memberingame.DateInserted = DateTime.Now;
                        memberingame.DateUpdated = DateTime.Now;
                        memberingame.USR = "Computed";
                     //   memberingame.WinIndicator = false;
                        memberingame.PaymentIndicator = false;
                        nonWinners.Add(memberingame);
                    }
                    newGame.MemberInGames = nonWinners;
                    
                    db.SaveChanges();
                    FFFGame mygame = new FFFGame(db, scheduler, newGame);
                    
                     //  mygame.makeActive();
                    mygame.makeActive();

                }
            }
            base.makeCompleted();
        }
    }
}