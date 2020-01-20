using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Models;
using Vaultlife.Dao;
using Vaultlife.Service.Rules;



namespace Vaultlife.Service
{
    public class GameService
    {

        private VaultLifeApplicationEntities db;

        public GameService(VaultLifeApplicationEntities db)
        {
            this.db = db;
        }

        public IEnumerable<Game> findGlobalGames(int? MemberId)
        {
            GameDao gameDao = new GameDao(db);
            return gameDao.findGlobalGames(MemberId);
        }

        public IEnumerable<Game> FindComingSoonGames(int? MemberId)
        {
            GameDao gameDao = new GameDao(db);
            if (MemberId > 0)
            {
                var comingSoonGames = gameDao.findComingSoonGames(MemberId);
                return comingSoonGames;
            }
            var comingSoonAllGames = gameDao.findComingSoonGames();
            return comingSoonAllGames;
        }

        public int getMaxTransactionTime(int GameId)
        {
            GameDao gameDao = new GameDao(db);
            return gameDao.getMaxTransactionTime(GameId);
        }

        public void paymentFailed(int GameID)
        {
            GameDao gameDao = new GameDao(db);
            Game game = gameDao.findGame(GameID);
            game.NumberOfWinners++;
            gameDao.save();
            if (getMaxTransactionTime(game.GameID) == 0)   //is this the last payment failure
            {
                RestartGameRule restartRule = new RestartGameRule(game, db);
            //    restartRule.restart();
            }
        }

    }
}