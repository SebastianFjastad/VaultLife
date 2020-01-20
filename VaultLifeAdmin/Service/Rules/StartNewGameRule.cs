using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaultLifeAdmin.Models;
using VaultLifeAdmin.Models.Games;

namespace VaultLifeAdmin.Service.Rules
{
    public class StartNewGameRule : IRule
    {

        public DateTimeOffset ExecuteTime { get; set; }
        public RuleType ruleType { get; set; }
        public GameEntity gameEntity { get; set; }
        public GameEntity newGame { get; set; }

      

        public StartNewGameRule(GameRule gameRule, GameEntity game, GameEntity newGame)
        {
            this.ExecuteTime = gameRule.ExcecuteTime;
            this.gameEntity = game;
            this.GameRuleId = gameRule.GameRuleID;
            this.newGame = newGame;
            this.ruleType = RuleType.START_NEW_GAME;
        }

        public override Dictionary<string, string> getRuleData()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("executeTime", ExecuteTime.ToString());
            data.Add("gameId", gameEntity.game.GameID.ToString());
            data.Add("newGameId", newGame.game.GameID.ToString());
            data.Add("ruleType", ruleType.ToString());
            return data;
        }

        public override void Execute(Quartz.IJobExecutionContext context)
        {
            IEnumerable<MemberInGame> nonWinners = gameEntity.game.MemberInGames.ToList<MemberInGame>().Where(x => x.WinIndicator == false || x.PaymentIndicator == false);
            newGame.game.MemberInGames = nonWinners.ToArray<MemberInGame>();
            newGame.makeReady();
            newGame.makeReleased();
            gameEntity.db.Games.Add(newGame.game);
            gameEntity.db.SaveChanges();
        }
    }
}