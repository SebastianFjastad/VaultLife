using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Models;
using System.Net;

namespace Vaultlife.Managers
{
    public class TrackingTransactionManager
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();

        public int InitiatePaymentTrackingTransaction(int GameID, int MemberID)
        {
          

            MemberInGame memberInGame = db.MemberInGames.Where(x => x.GameID == GameID && x.MemberID == MemberID).FirstOrDefault();
            if (memberInGame == null)
            {
                return -1;
            }

            int TrackingTransactionID = this.GetTrackingTransaction(memberInGame.MemberInGameID);
            if (TrackingTransactionID > 0)
            {
                bool pauseResult = this.PauseTrackingTransaction(TrackingTransactionID, DateTime.Now);
                if (pauseResult)
                {
                    bool updateResult = this.UpdateRemainingDuration(TrackingTransactionID);
                    if (updateResult)
                    {
                        bool resumeResult = this.ResumeTrackingTransaction(TrackingTransactionID);
                        if (resumeResult)
                        {
                            return this.GetRemainingDuration(TrackingTransactionID);
                        }
                    }

                }
                return -1;
            }

            if (this.InitiateTrackingTransaction(memberInGame.MemberInGameID))
            {
                return 90;
            }
            return -1;
        }

        public bool PausePaymentTrackingTransaction(int GameID, int MemberID)
        {
          

            MemberInGame memberInGame = db.MemberInGames.Where(x => x.GameID == GameID && x.MemberID == MemberID).FirstOrDefault();
            if (memberInGame == null)
            {
                return false;
            }

            int TrackingTransactionID = this.GetTrackingTransaction(memberInGame.MemberInGameID);
            if (TrackingTransactionID > 0)
            {
               return this.PauseTrackingTransaction(TrackingTransactionID, DateTime.Now);
            }
            return false;
        }

        public bool ResumePaymentTrackingTransaction(int GameID, int MemberID)
        {
          

            MemberInGame memberInGame = db.MemberInGames.Where(x => x.GameID == GameID && x.MemberID == MemberID).FirstOrDefault();
            if (memberInGame == null)
            {
                return false;
            }

            int TrackingTransactionID = this.GetTrackingTransaction(memberInGame.MemberInGameID);
            if (TrackingTransactionID > 0)
            {
                bool resumeResult = this.UpdateRemainingDuration(TrackingTransactionID);
                if (resumeResult)
                {
                   resumeResult = this.ResumeTrackingTransaction(TrackingTransactionID);
                }
                return resumeResult;

            }
            return false;
        }

        public int GetTimeRemaining(int GameID, int MemberID)
        {
           
            MemberInGame memberInGame = db.MemberInGames.Where(x => x.GameID == GameID && x.MemberID == MemberID).FirstOrDefault();
            if (memberInGame == null)
            {
                return -1;
            }

            int TrackingTransactionID = this.GetTrackingTransaction(memberInGame.MemberInGameID);
            if (TrackingTransactionID > 0)
            {
                return this.GetRemainingDuration(TrackingTransactionID);
            }
            return -1;
        }

        private bool InitiateTrackingTransaction(int MemberInGameID)
        {
            TrackingTransaction trackingTransaction = new TrackingTransaction();
            trackingTransaction.MemberInGameID = MemberInGameID;
            trackingTransaction.TimeInitiated = DateTime.Now;
            trackingTransaction.DurationRemaining = 90;
            trackingTransaction.DateInserted = DateTime.Now;
            trackingTransaction.DateModified = DateTime.Now;

            db.TrackingTransactions.Add(trackingTransaction);

            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return false;
            }
            return true;
        }

        private int GetTrackingTransaction(int MemberInGameID)
        {
            TrackingTransaction trackingTransaction = new TrackingTransaction();
            var trackingTransactions = from tt in db.TrackingTransactions where tt.MemberInGameID == MemberInGameID select tt;
            foreach (var trackingTx in trackingTransactions)
            {
                trackingTransaction = (TrackingTransaction)trackingTx;
                break;
            }

            // -1 - no tracking transaction found.
            if (trackingTransaction.MemberInGameID == MemberInGameID)
            {
                return trackingTransaction.TrackingTransactionID;
            }
            return -1;
        }

        private int GetRemainingDuration(int TrackingTransactionID)
        {
            TrackingTransaction trackingTransaction = db.TrackingTransactions.Find(TrackingTransactionID);
            return (int)trackingTransaction.DurationRemaining;
        }

        private bool PauseTrackingTransaction(int TrackingTransactionID, DateTime TransactionInitiateTime)
        {
            TrackingTransaction trackingTransaction = db.TrackingTransactions.Find(TrackingTransactionID);
            trackingTransaction.TimePaused = TransactionInitiateTime;

            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return false;
            }
            return true;

        }

        private bool ResumeTrackingTransaction(int TrackingTransactionID)
        {
            TrackingTransaction trackingTransaction = db.TrackingTransactions.Find(TrackingTransactionID);
            trackingTransaction.TimeResumed = DateTime.Now;

            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return false;
            }
            return true;

        }

        private bool CompleteTrackingTransaction(int TrackingTransactionID)
        {
            TrackingTransaction trackingTransaction = db.TrackingTransactions.Find(TrackingTransactionID);
            trackingTransaction.TimeCompleted = DateTime.Now;

            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return false;
            }
            return true;

        }


        public bool CompletePaymentTrackingTransaction(int GameID, int MemberID)
        {
            MemberInGame memberInGame = db.MemberInGames.Where(x => x.GameID == GameID && x.MemberID == MemberID).FirstOrDefault();
            if (memberInGame == null)
            {
                return false;
            }

            int TrackingTransactionID = this.GetTrackingTransaction(memberInGame.MemberInGameID);
            if (TrackingTransactionID > 0)
            {
                return this.CompleteTrackingTransaction(TrackingTransactionID);
            }
            return false;
        }

        private bool UpdateRemainingDuration(int TrackingTransactionID)
        {
            TrackingTransaction trackingTransaction = db.TrackingTransactions.Find(TrackingTransactionID);

            if (trackingTransaction.TimeResumed != null)
            {
                DateTime TimePaused = (DateTime)trackingTransaction.TimePaused;
                DateTime TimeResumed = (DateTime)trackingTransaction.TimeResumed;
                float difference = (float)TimePaused.Subtract(TimeResumed).TotalSeconds ;
                difference = (difference < 0) ? (float)trackingTransaction.DurationRemaining : difference;
                trackingTransaction.DurationRemaining = trackingTransaction.DurationRemaining - difference;
            }
            else
            {
                DateTime TimePaused = (DateTime)trackingTransaction.TimePaused;
                DateTime TimeInitiated = (DateTime)trackingTransaction.TimeInitiated;
                float difference = (float)TimePaused.Subtract(TimeInitiated).TotalSeconds;
                trackingTransaction.DurationRemaining = trackingTransaction.DurationRemaining - difference;

            }

            if (trackingTransaction.DurationRemaining < 0)
            {
                trackingTransaction.DurationRemaining = 0;
            }

            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return false;
            }
            return true;

        }

    }
}