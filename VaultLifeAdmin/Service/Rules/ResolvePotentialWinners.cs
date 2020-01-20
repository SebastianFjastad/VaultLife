using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaultLifeAdmin.Models;
using VaultLifeAdmin.Models.Games;

namespace VaultLifeAdmin.Service.Rules
{
    public class ResolvePotentialWinners : IRule
    {

       
        public override RuleType ruleType { get; set; }
        public GameEntity gameEntity { get; set; }
        public GameRule gamerule { get; set; }

        public ResolvePotentialWinners(GameRule gameRule, GameEntity game)
        {
            this.ExecuteTime = gameRule.ExcecuteTime;
            this.gameEntity = game;
            this.GameRuleId = gameRule.GameRuleID;
            this.ruleType = RuleType.RESOLVE_WINNERS;
            this.gamerule = gameRule;
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
            if (gameEntity.resolvePotentialWinners() == GameResolveStatus.OUTSTANDING) {
                this.ExecuteTime = ExecuteTime.AddSeconds(10);  ///TODO is this correct?
                schedule(context.Scheduler);

            }

            gamerule.ExcecuteTime =ExecuteTime.DateTime;   
           
            gameEntity.db.SaveChanges();  
        }

    }
}