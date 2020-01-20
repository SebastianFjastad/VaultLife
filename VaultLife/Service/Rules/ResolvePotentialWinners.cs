using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Models;
using Vaultlife.Models.Games;
using Quartz;

namespace Vaultlife.Service.Rules
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
            try
            {

                if (gameEntity.resolvePotentialWinners() == GameResolveStatus.OUTSTANDING) {
                    this.ExecuteTime = DateTime.Now.AddSeconds(10);  ///TODO is this correct?
                    schedule(context.Scheduler);

                }

                gamerule.ExcecuteTime =ExecuteTime.DateTime;    
                gameEntity.db.SaveChanges();
            }
            catch (Exception e)
            {
                JobExecutionException je = new JobExecutionException(e);
                je.RefireImmediately = true;  //do something with the exception
                throw je;  //throw JobExecutionException
            } 
        }

    }
}