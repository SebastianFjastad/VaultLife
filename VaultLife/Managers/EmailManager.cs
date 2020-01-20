using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Helpers;
using Vaultlife.Models;

namespace Vaultlife.Managers
{
    public class EmailManager
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();
        private EmailConfig emailConfig;
        #region properties and fields

        /// <summary>
        /// Message object for email
        /// </summary>
        private Email newMail;
        private int priority;
        private DateTime sendAfter;
        private string fromName;
        private string fromAddress;
        private int memberID;
        private string recipientEmailAddress;
        private string recipientName;
        private string emailSubject;
        private string emailBodyText;

        public string FromName
        {
            get
            {
                return fromName;
            }
            set
            {
                fromName = value;
            }
        }

        public string FromAddress
        {
            get
            {
                return fromAddress;
            }
            set
            {
                fromAddress = value;
            }
        }

        public string RecipientEmailAddress
        {
            get
            {
                return recipientEmailAddress;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new InvalidCastException("Recipient Email Address can not be null or empty");

                recipientEmailAddress = value;
            }
        }

        public string RecipientName
        {
            get
            {
                return recipientName;
            }
            set
            {
                recipientName = value;
            }
        }

        public string EmailSubject
        {
            get
            {
                return emailSubject;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new InvalidCastException("Email Subject can not be null or empty");

                emailSubject = value;
            }
        }

        public string EmailBodyText
        {
            get
            {
                return emailBodyText;
            }
            set
            {
                emailBodyText = value;
            }
        }

        public int Priority
        {
            get
            {
                return priority;
            }
            set
            {
                priority = value;
            }
        }
        public DateTime SendAfter
        {
            get
            {
                return sendAfter;
            }
            set
            {
                sendAfter = value;
            }
        }
        public int MemberID
        {
            get
            {
                return memberID;
            }
            set
            {
                memberID = value;
            }
        }

        #endregion
        public EmailManager()
        {
            this.GetActiveEmailConfig();
        }

        public void NewEmail()
        {
            newMail = new Email();
            this.SetNewMailDefaults();
        }

            
        public string QueueEmail()
        {
            DateTime baseDateTime = DateTime.MinValue;
            if (priority != null)
            {
                newMail.Priority = ((int)priority > 0) ? (int)priority : newMail.Priority;
            }

            if (sendAfter != null && sendAfter > DateTime.MinValue)
            {
                newMail.SendAfter = sendAfter;
            }

            if (fromName != "" && fromName != null)
            {
                newMail.FromName = fromName;
            }

            if (fromAddress != "" && fromAddress != null)
            {
                newMail.FromAddress = fromAddress;
            }

            newMail.MemberID = memberID;
            newMail.RecipientEmailAddress = recipientEmailAddress;
            newMail.RecipientName = recipientName;
            newMail.EmailSubject = emailSubject;
            newMail.EmailBodyText = emailBodyText;

            db.Emails.Add(newMail);
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
                return "Error";
            }
            return "Success";
        }

        public void SendQueuedEmail()
        {
            // get list of emails, sort by priority
            IQueryable<Email> queuedEmails = db.Emails.Where(x => x.Status != "Sent" && x.FailedPermanently == false).OrderBy(x => x.Priority);
            // loop through collection
            foreach (Email email in queuedEmails)
            {
                EmailHelper emailHelper = new EmailHelper();
                if (emailConfig != null)
                {
                    // setup smtp properties if in config
                    if (emailConfig.smtpPassword != null && emailConfig.smtpUsername != null && emailConfig.smtpUsername != null)
                    {
                        emailHelper = new EmailHelper(emailConfig);
                    }
                }

                emailHelper.email = email;
                emailHelper.AddToAddress(email.RecipientEmailAddress);
                emailHelper.FromEmailID = email.FromAddress;
                emailHelper.FromDisplayName = email.FromName;
                emailHelper.MailSubject = email.EmailSubject;
                emailHelper.MailBody = email.EmailBodyText;
                // send email
                emailHelper.SendMail();
            }
        }

        private void SetNewMailDefaults()
        {
            newMail.Status = "New";
            newMail.Priority = 3; // 5 is high priority, 3 - default normal priority , 1 - low priority
            newMail.FromName = ""; // default to configured default email senders name
            newMail.FromAddress = ""; //default to configured default email senders email
            newMail.SendAfter = DateTime.Now;
            newMail.DateInserted = DateTime.Now;
            newMail.DateUpdated = DateTime.Now;
            newMail.Attempts = 0;
            newMail.USR = "";
            newMail.FailedPermanently = false;

            if (emailConfig != null)
            {
                newMail.FromName = emailConfig.DefaultFromName;
                newMail.FromAddress = emailConfig.DefaultFromAddress;
            }
        }

        private void GetActiveEmailConfig()
        {
            // Get most recently inserted active config
            emailConfig = db.EmailConfigs.Where(x => x.Status == "A").OrderByDescending(x => x.DateInserted).FirstOrDefault();
        }

    }
}