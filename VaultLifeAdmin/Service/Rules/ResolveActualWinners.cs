using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaultLifeAdmin.Models;
using VaultLifeAdmin.Models.Games;
using Quartz;
using Quartz.Impl;

namespace VaultLifeAdmin.Service.Rules
{
    public class ResolveActualWinners : IRule
    {

       
        public RuleType ruleType { get; set; }
        public GameEntity gameEntity { get; set; }
       

        public ResolveActualWinners(GameRule gameRule, GameEntity game)
        {
            this.ExecuteTime = gameRule.ExcecuteTime;
            this.gameEntity = game;
            this.GameRuleId = gameRule.GameRuleID;
            this.ruleType = RuleType.RESOLVE_ACTUAL_WINNERS;
        }

        public override Dictionary<string, string> getRuleData()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("executeTime", ExecuteTime.ToString());
            data.Add("gameId", gameEntity.game.GameID.ToString());
            data.Add("ruleType", ruleType.ToString());
            return data;
        }

        public override void Execute(Quartz.IJobExecutionContext context)
        {
            
       
            gameEntity.makeCompleted();
            
            gameEntity.db.SaveChanges();
        }

        
        private void restartGame(GameEntity gameEntity, int numWinners)
        {
            gameEntity.numWinnersLeft = numWinners;
            gameEntity.makePrepareActive();
            //TODO reschedule Rules 
            //TODO find what about timings

        }

        private int getFailedPayments(GameEntity gameEntity)
        {
            ICollection<MemberInGame> players = gameEntity.game.MemberInGames;
            return players.Count(p => p.WinIndicator == true && p.PaymentIndicator == false);
        }

    }
}