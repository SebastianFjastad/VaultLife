using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using VaultLifeAdmin.Models.Games;
using VaultLifeAdmin.Models;

namespace VaultLifeAdmin.Service.Rules
{
    public class StartGameRule : IRule
    {
        public GameEntity gameEntity { get; set; }
     

        public StartGameRule(GameRule gameRule, GameEntity game)
        {

            this.ExecuteTime = gameRule.ExcecuteTime;
            this.gameEntity = game;
            this.GameRuleId = gameRule.GameRuleID;
            this.ruleType = RuleType.START_GAME;
        }

        public override Dictionary<string, string> getRuleData()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("executeTime", ExecuteTime.ToString());
            data.Add("gameId", gameEntity.game.GameID.ToString());
            data.Add("ruleType", ruleType.ToString());
            return data;
        }

        public override void Execute(IJobExecutionContext context)
        {
            gameEntity.makeActive();
            gameEntity.db.SaveChanges();
        }



    }
}