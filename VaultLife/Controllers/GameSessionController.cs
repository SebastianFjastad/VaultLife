using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vaultlife.Models;

namespace Vaultlife.Controllers
{
    public class GameSessionController : Controller
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();
        //
        // GET: /GameSession/
        public ActionResult Index()
        {
            return View();
        }

        public int GameStart(int? gameID)
        {
            // Returns number of seconds till game is due to start or -1 if game not yet 'open' / > 2 min
            return 10;
            
        }


        public bool IsGameActive(int? gameID)
        {
            // Check if the game _game is complete.
            if (gameID == null)
            {
            }
            Game game = db.Games.Find(gameID);
            if (game == null)
            {
                return false;
            }
            return false;
        }

        public bool IsUserAWinner(float timeTaken)
        {
            // Tally all results and pick the winner - race conditions?
            return false;
        }
    }
}