using Quartz;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Dao;
using Vaultlife.Models;
using Vaultlife.Models.Games;

namespace Vaultlife.Service.Rules 
{
    public class RestartGameRule 
    {

        public Game game { get; set; }

        public VaultLifeApplicationEntities db { get; set; }


        public RestartGameRule(Game game, VaultLifeApplicationEntities db)
        {
            this.game = game;
            this.db = db;
        }

        // TODO move login to game.. to resuse rule across games
 /*       public void restart()
        {
            if (game.restart == true)
            {
                Game newGame = new Game();
                newGame.DateInserted = new DateTime();
                newGame.DateUpdated = new DateTime();
                newGame.GameCode = game.GameCode;
                newGame.GameTypeID = game.GameTypeID;
                newGame.GameName = game.GameName;
                newGame.NumberOfWinners = game.NumberOfWinners;
                newGame.NextGameID = game.NextGameID;
                newGame.MemberSubscriptionType = game.MemberSubscriptionType;
                newGame.Global = game.Global;
                newGame.restart = false;
                GameDao gameDao = new GameDao(db);
                gameDao.save(newGame);

                ICollection<GameRule> newGameRules = add3Mins(game.GameRules.ToList(), newGame.GameID);
                GameRuleDao gameRuleDao = new GameRuleDao(db);
                gameRuleDao.save(newGameRules);

                newGame.MemberInGames = gameDao.findLosers(game.GameID);
                game.NextGameID = newGame.GameID;
                db.SaveChanges();

                reschedule5MinDeal(gameDao, newGame.NextGameID);
            }
        }
        */

        private void reschedule5MinDeal(GameDao gameDao, int? gameID)
        {
            if (gameID != null)
            {
                Game fiveMinGame = gameDao.findGame(gameID);
                foreach (GameRule gameRule in fiveMinGame.GameRules)
                {
                    gameRule.ExcecuteTime.AddMinutes(3);
                }
                gameDao.save();
            } 
        }


        private ICollection<GameRule> add3Mins(ICollection<GameRule> gameRules, int newGameID)
        {
            ICollection<GameRule> newGameRules = gameRules.Select(gr => new GameRule
            {
                GameRuleCode = gr.GameRuleCode,
                GameID = newGameID,
                Schedule = gr.Schedule,
                GameRuleDetail = gr.GameRuleDetail,
                ExcecuteTime = gr.ExcecuteTime.AddMinutes(3),
                DateInserted = new DateTime(),
                DateUpdated = new DateTime(),
                USR = gr.USR,
                GameTemplateID = gr.GameTemplateID,
                ExecuteHhMmSs = gr.ExecuteHhMmSs
            }).ToList();

            return newGameRules;
        }

    }


}