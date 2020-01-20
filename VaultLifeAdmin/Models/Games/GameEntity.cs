using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaultLifeAdmin.Service.Rules;
using Quartz;
using Quartz.Impl;
using VaultLifeAdmin.Models;

namespace VaultLifeAdmin.Models.Games
{
    public abstract class GameEntity
    {
        private List<IRule> rules;
        protected IScheduler scheduler;
        public VaultLifeApplicationEntities db { get; set; }
        public int numWinnersLeft {get; set;}
        public GameState state { get; set; }
        public Game game { get; set; }

        public GameEntity(VaultLifeApplicationEntities db, IScheduler scheduler, int numWinners) {
            state = GameState.CREATED;
            this.db = db;
            this.scheduler = scheduler; // StdSchedulerFactory.GetDefaultScheduler();
            this.rules = new List<IRule>();
            this.numWinnersLeft = numWinners;
            this.game = new Game();
            switch (game.GameState) {
                case "ACTIVE" :     this.state = GameState.ACTIVE;
                                    break;
                case "READY" :      this.state = GameState.READY;
                                    break;
                case "CREATED" :    this.state = GameState.CREATED;
                                    break;
                case "COMPLETED" :  this.state = GameState.COMPLETED;
                                    break;
                case "RELEASED" :   this.state = GameState.RELEASED;
                                    break;
                case "PREPARE" :    this.state = GameState.PREPARE_ACTIVE;
                                    break;

            }
        }

        public GameEntity(VaultLifeApplicationEntities db, IScheduler scheduler, Game game) {
            this.scheduler = scheduler;
            this.db = db;
            this.rules = new List<IRule>();
            this.game = game;
          //  ProductInGame prod = game.ProductInGames.First();
            this.numWinnersLeft = game.NumberOfWinners;  //prod.Quantity;
           

            switch (game.GameState)
            {
                case "ACTIVE": this.state = GameState.ACTIVE;
                    break;
                case "READY": this.state = GameState.READY;
                    break;
                case "CREATED": this.state = GameState.CREATED;
                    break;
                case "COMPLETED": this.state = GameState.COMPLETED;
                    break;
                case "RELEASED": this.state = GameState.RELEASED;
                    break;
                case "PREPARE": this.state = GameState.PREPARE_ACTIVE;
                    break;

            }

        }
        public static GameEntity toGameEntity(VaultLifeApplicationEntities db, IScheduler scheduler, Game game)
        {
            switch (game.GameType.GameTypeName)
            {
                case "FastestFinger": return new FFFGame(db, scheduler, game);
                case "FCF": return new FCFGame(db, scheduler, game);
            }
            throw new NotImplementedException();
        }

        public abstract GameResolveStatus resolvePotentialWinners();

        public virtual void makeReleased()
        {
            state = GameState.RELEASED;
            game.GameState = "RELEASED";
            db.SaveChanges();
        }

        public virtual void makeReady()
        {
            state = GameState.READY;
            game.GameState = "READY";
            db.SaveChanges();
        }

        public virtual void makeActive()
        {
            state = GameState.ACTIVE;
            game.GameState = "ACTIVE";
            db.SaveChanges();
        }

        public virtual void makePrepareActive()
        {
            state = GameState.PREPARE_ACTIVE;
            game.GameState = "PREPARE";
            db.SaveChanges();
        }

        public virtual void makeCompleted()
        {
            state = GameState.COMPLETED;
            game.GameState = "COMPLETED";
            db.SaveChanges();
        }

    }
}
