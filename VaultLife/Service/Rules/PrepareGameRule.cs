using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Vaultlife.Models.Games;
using Vaultlife.Models;

namespace Vaultlife.Service.Rules
{
    public class PrepareGameRule : IRule
    {
        public GameEntity gameEntity { get; set; }
       
        public RuleType ruleType { get; set; }

        

        public PrepareGameRule(GameRule gameRule, GameEntity game)
        {
            this.ExecuteTime = gameRule.ExcecuteTime;
            this.gameEntity = game;
            this.GameRuleId = gameRule.GameRuleID;
            this.ruleType = RuleType.PREPARE_GAME;
        }

        public override Dictionary<string, string> getRuleData()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("executeTime", ExecuteTime.ToString());
            data.Add("gameId", gameEntity.game.GameID.ToString());
            data.Add("ruleType", ruleType.ToString());
            return data;
        }

        public  override void Execute(IJobExecutionContext context)
        {
            gameEntity.makePrepareActive();
            gameEntity.db.SaveChanges();
        }



    }
}