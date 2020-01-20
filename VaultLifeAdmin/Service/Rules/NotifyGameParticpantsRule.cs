using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using VaultLifeAdmin.Models;
using VaultLifeAdmin.Models.Games;

namespace VaultLifeAdmin.Service.Rules
{
    public class NotifyGameParticipantsRule : IRule
    {
        public GameEntity gameEntity { get; set; }
       
        public RuleType ruleType { get; set; }
      

        public NotifyGameParticipantsRule(GameRule gameRule, GameEntity game)
        {
            this.ExecuteTime = gameRule.ExcecuteTime;
            this.gameEntity = game;
            this.GameRuleId = gameRule.GameRuleID;
            this.ruleType = RuleType.NOTIFY_PARTICIPANTS;
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
            try
            {
                ///TODO add some kind of email/sms handler and send notifications
            }
            catch (Exception e)
            {
               ///// TODO do something
            }
        }

       
    }

}