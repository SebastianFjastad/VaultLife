using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;

namespace VaultLifeAdmin.Models.Games
{
    public class FCFGame : GameEntity
    {
        public FCFGame(VaultLifeApplicationEntities db, IScheduler scheduler, Game game) : base(db, scheduler, game)
        {
            
            if (!game.GameType.GameTypeName.Equals("FCF")) {
                throw new InvalidOperationException();
            }
        }

        public override GameResolveStatus resolvePotentialWinners()
        {
            return GameResolveStatus.OUTSTANDING;
        }

        public override void makeReleased()
        {
            base.makeReleased();
        }

        public override void makeReady()
        {
            base.makeReady();
        }

        public override void makeActive()
        {
            base.makeActive();
        }

        public override void makePrepareActive()
        {
            base.makePrepareActive();
        }

        public override void makeCompleted()
        {
            base.makeCompleted();
        }


    }
}