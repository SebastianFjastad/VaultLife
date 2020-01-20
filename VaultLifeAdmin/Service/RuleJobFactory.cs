using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz.Spi;
using Quartz;
using VaultLifeAdmin.Service;
using VaultLifeAdmin.Service.Rules;
using VaultLifeAdmin.Models;
using VaultLifeAdmin.Models.Games;

namespace VaultLifeAdmin.Service
{
    public class RuleJobFactory : IJobFactory
    {

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();
            JobDataMap map = bundle.JobDetail.JobDataMap;
            Game game = db.Games.Find(Convert.ToInt32(map["gameId"]));
            DateTimeOffset executeTime = DateTimeOffset.Parse((String)map["executeTime"]);
            GameRule gameRule;
            switch (map.GetString("ruleType"))
            {
                case "NOTIFY_PARTICIPANTS" :
                     gameRule = game.GameRules.First(g => g.GameRuleCode.Equals("NotifiyGameParticipants"));
                    return new NotifyGameParticipantsRule(gameRule, GameEntity.toGameEntity(db, scheduler, game));
                case "START_GAME":
                     gameRule = game.GameRules.First(g => g.GameRuleCode.Equals("StartGame"));
                    return new StartGameRule(gameRule, GameEntity.toGameEntity(db, scheduler, game));
                case "PREPARE_GAME":
                    gameRule = game.GameRules.First(g => g.GameRuleCode.Equals("PrepareGameRule"));
                    return new PrepareGameRule(gameRule, GameEntity.toGameEntity(db, scheduler, game));
                case "RESOLVE_WINNERS":
                    gameRule = game.GameRules.First(g => g.GameRuleCode.Equals("ResolvePotentialWinners"));
                    return new ResolvePotentialWinners(gameRule, GameEntity.toGameEntity(db, scheduler, game));
                case "RESOLVE_ACTUAL_WINNERS":
                    gameRule = game.GameRules.First(g => g.GameRuleCode.Equals("ResolveActualWinners"));
                    return new ResolveActualWinners(gameRule, GameEntity.toGameEntity(db, scheduler, game));
                case "START_NEW_GAME":
                    gameRule = game.GameRules.First(g => g.GameRuleCode.Equals("StartNewGame"));
                    return new StartNewGameRule(gameRule, GameEntity.toGameEntity(db, scheduler, game), GameEntity.toGameEntity(db, scheduler, db.Games.Find(map["newGameId"])));
            }
            throw new NotImplementedException();
        }

        public void ReturnJob(IJob job)
        {

        }
    }
}