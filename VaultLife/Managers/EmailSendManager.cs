using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Helpers;
using Vaultlife.Models;

namespace Vaultlife.Managers
{
    public class EmailSendManager
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();
        private EmailConfig emailConfig;
        public EmailSendManager()
        {
            this.GetActiveEmailConfig();
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
                emailHelper.BodyContainsHTML = true;
                emailHelper.MailSubject = email.EmailSubject;
                emailHelper.MailBody = email.EmailBodyText;
                // send email
                emailHelper.SendMail();
            }
        }
        private void GetActiveEmailConfig()
        {
            // Get most recently inserted active config
            emailConfig = db.EmailConfigs.Where(x => x.Status == "A").OrderByDescending(x => x.DateInserted).FirstOrDefault();
        }
    }
}