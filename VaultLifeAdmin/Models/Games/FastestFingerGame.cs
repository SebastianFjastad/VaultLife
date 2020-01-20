using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using VaultLifeAdmin.Service.Rules;
using VaultLifeAdmin.Models;
using VaultLifeAdmin.Helpers;

namespace VaultLifeAdmin.Models.Games
{
    public class FastestFingerGame : GameEntity
    {
        public FastestFingerGame(VaultLifeApplicationEntities db, IScheduler scheduler, Game game)
            : base(db, scheduler, game)
        {
            if (!game.GameType.GameTypeName.Equals("FastestFinger"))
            {
                throw new InvalidOperationException();
            }
        }

        public override GameResolveStatus resolvePotentialWinners()
        {
            ICollection<ProductPlayed> gameResults = game.ProductInGames.First().ProductPlayeds;
            resolve(gameResults);
            return numWinnersLeft > 0 ? GameResolveStatus.OUTSTANDING : GameResolveStatus.RESOLVED;    
        }

        private void resolve(ICollection<ProductPlayed> gameResults)
        {
             IEnumerable<ProductPlayed> winning = gameResults.Where(play=>play.Winner==0).OrderBy(g => g.ClickInterval);  //assert highest to lowest
             for (int i = 0; i < winning.Count(); i++ )
             {
                 ProductPlayed play = winning.ElementAt(i);
                 play.Winner = 1;
                 if (i < numWinnersLeft)
                 {
                     MemberInGame me = db.MemberInGames.Find(play.MemberInGameID);
                     me.WinIndicator = true;
                     play.Winner = 2;
                     play.ProductInGame.Quantity = play.ProductInGame.Quantity - 1;
                     numWinnersLeft--;
                     this.game.NumberOfWinners = (numWinnersLeft);
                 }
             }
             db.SaveChanges();
            
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

                    IEnumerable<MemberInGame> membersingame = game.MemberInGames.Where(x => x.WinIndicator == false).ToList();
                    List<MemberInGame> newMembers = new List<MemberInGame>();

                    foreach (MemberInGame memingame in membersingame) {
                        MemberInGame memberingame = new MemberInGame();
                        memberingame.GameID = newGame.GameID;
                        memberingame.MemberID = memingame.MemberID;
                        memberingame.DateInserted = memingame.DateInserted;
                        memberingame.DateUpdated = memingame.DateUpdated;
                        memberingame.USR = memingame.USR;
                        memberingame.WinIndicator = false;
                        memberingame.PaymentIndicator = false;
                        newMembers.Add(memberingame);
                    }
                    newGame.MemberInGames = newMembers;
                    
                    db.SaveChanges();
                    FFFGame mygame = new FFFGame(db, scheduler, newGame);
                  //  mygame.makeActive();
                    mygame.makeReleased();

                }
            }
            base.makeCompleted();
        }
    }
}