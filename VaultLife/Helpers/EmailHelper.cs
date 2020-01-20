using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Configuration;
using System.Configuration;
using System.Threading;
using Vaultlife.Models;



namespace Vaultlife.Helpers
{
    public class EmailHelper
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();
        Thread emailWorker = null;
        public Email email{ get; set; }
        public EmailErrorLog errorLog {get; private set;}
        public EmailSendLog sendLog { get; private set; }
        private EmailConfig emailConfig;
        private MailMessage mailMessage;
        private SmtpClient smtpClient;
        //private Attachment attachments;
        private string _isSSL = string.Empty;
        // smtp config values
        bool smptIsSSL = false;


        #region properties and fields

        /// <summary>
        /// Message object for email
        /// </summary>
        private MailMessage EmailMessage
        {
            get
            {
                if (mailMessage == null)
                    return new MailMessage();
                else
                    return mailMessage;
            }
            set
            {
                mailMessage = value;
            }
        }
        private string fromEmailID;
        /// <summary>
        /// Email Id for from part(Mandatory)
        /// </summary>
        public string FromEmailID
        {
            get
            {
                return fromEmailID;
            }
            set
            {
                fromEmailID = value;
            }
        }

        public string isSSL
        {
            get
            {
                return _isSSL;
            }
            set
            {
                _isSSL = value;
            }
        }
        private string fromDisplayName;
        /// <summary>
        /// Display Name on from part (Optional)
        /// </summary>
        public string FromDisplayName
        {
            get
            {
                return fromDisplayName;
            }
            set
            {
                fromDisplayName = value;
            }
        }



        //private string mailSubject;
        /// <summary>
        /// Subject for mail (Mandatory)
        /// </summary>
        public string MailSubject
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new InvalidCastException("Mail subject can not be null or empty");

                EmailMessage.Subject = value;
            }
        }

        //private string mailBody;
        /// <summary>
        /// Body for mail (Mandatory)
        /// </summary>
        public string MailBody
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new InvalidCastException("Mail body can not be null or empty");

                EmailMessage.Body = value;
            }
        }

        //private bool bodyContainsHTML = true;
        /// <summary>
        /// Body for mail contains HTML tags (Optional default true)
        /// </summary>
        public bool BodyContainsHTML
        {
            set
            {
                EmailMessage.IsBodyHtml = value;
            }
        }

        #endregion
        /// <summary>
        /// Parameterless Constructor 
        /// </summary>
        public EmailHelper()
        {
            EmailMessage = new MailMessage();
            errorLog = new EmailErrorLog();
            sendLog = new EmailSendLog();
            email = new Email();
          
        }

        public EmailHelper(EmailConfig _emailConfig)
        {
            EmailMessage = new MailMessage();
            errorLog = new EmailErrorLog();
            sendLog = new EmailSendLog();
            email = new Email();
            emailConfig = _emailConfig;

        }
        

        /// <summary>
        /// add email address to To List.
        /// </summary>
        /// <param name="toAddress">address of user to whom mail needed to be sent</param>
        public void AddToAddress(string toAddress)
        {
            if (!string.IsNullOrEmpty(toAddress))
                EmailMessage.To.Add(toAddress);
            else
                throw new ArgumentNullException("To address can't be null or empty");

        }

        /// <summary>
        /// add multiple email address to To List.
        /// </summary>
        /// <param name="toAddress"> array of email address of users to whom mail needed to be sent</param>
        public void AddToAddress(string[] toAddress)
        {
            string addreses = string.Join(",", toAddress);

            if (!string.IsNullOrEmpty(addreses))
                EmailMessage.To.Add(addreses);
        }

        /// <summary>
        /// add email address to CC List(Optional).
        /// </summary>
        public void AddCCAddress(string ccAddress)
        {
            if (!string.IsNullOrEmpty(ccAddress))
                EmailMessage.CC.Add(ccAddress);

        }

        /// <summary>
        /// add multiple email address to CC List.
        /// </summary>
        public void AddCCAddress(string[] ccAddress)
        {
            string addreses = string.Join(",", ccAddress);

            if (!string.IsNullOrEmpty(addreses))
                EmailMessage.CC.Add(addreses);
        }

        /// <summary>
        /// add email address to BCC List(Optional).
        /// </summary>
        public void AddBCCAddress(string bccAddress)
        {
            if (!string.IsNullOrEmpty(bccAddress))
                EmailMessage.Bcc.Add(bccAddress);

        }

        /// <summary>
        /// add multiple email address to BCC List.
        /// </summary>
        public void AddBCCAddress(string[] bccAddress)
        {
            string addreses = string.Join(",", bccAddress);

            if (!string.IsNullOrEmpty(addreses))
                EmailMessage.CC.Add(addreses);
        }


        /// <summary>
        /// add file attachment to email(Optional).
        /// </summary>
        public void AddFileAttachment(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
                EmailMessage.Attachments.Add(new Attachment(filePath));
            else
                throw new ArgumentNullException("File path is null or empty");
        }

        /// <summary>
        /// add file attachment to email(Optional).
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="mediaType">media type of the file attached this can be null</param>
        public void AddFileAttachment(string filePath, string mediaType)
        {
            if (!string.IsNullOrEmpty(filePath))
                EmailMessage.Attachments.Add(new Attachment(filePath, mediaType));
            else
                throw new ArgumentNullException("File path is null or empty");
        }

        /// <summary>
        /// add file attachment to email(Optional).
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="contentType">content type that describes the data in string </param>
        public void AddFileAttachment(string filePath, ContentType contentType)
        {
            if (!string.IsNullOrEmpty(filePath))
                EmailMessage.Attachments.Add(new Attachment(filePath, contentType));
            else
                throw new ArgumentNullException("File path is null or empty");
        }
        ///<summary> 
        /// Method for sending mail through SMTP server
        ///</summary>
        ///<parameters>mail details</parameters>
        ///<Returns>bool</Returns>
        public void SendMail()
        {
            try
            {
                #region Please do not delete finding a work around for it.
                //if (emailWorker == null)
                //    emailWorker = new BackgroundWorker();

                //emailWorker.WorkerSupportsCancellation = true;
                //emailWorker.DoWork += delegate(object sender, DoWorkEventArgs e)
                //{
                //    Send();

                //};
                //emailWorker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
                //{
                //    if (e.Error == null)
                //        Logger.Log("Mail Sent successfully", Logger.LogType.Info);
                //    else
                //    {
                //        Logger.Log("Mail sending failed ", Logger.LogType.Fatal);
                //        Logger.Log(e.Error, Logger.LogType.Error);
                //    }
                //};
                ////we have not reported progress for it as we dont need it.
                //emailWorker.RunWorkerAsync();
                //    //System.Threading.Thread.CurrentThread.Join();

                #endregion
                if (emailWorker == null)
                    emailWorker = new Thread(new ThreadStart(Send));

                emailWorker.IsBackground = true;
                emailWorker.Name = "EmailSenderBackgroundWorker";
                emailWorker.Start();
                Logger.Log("Email sender thread started", Logger.LogType.Info);
            }
            catch (Exception ex)
            {
                errorLog.EmailID = email.EmailID;
                errorLog.DateInserted = DateTime.Now;
                errorLog.ErrorCode = "Service";
                errorLog.ErrorDescription = "[WARN] Unable to start email thread";
                errorLog.MemberID = email.MemberID;
                errorLog.RecipientEmailAddress = email.RecipientEmailAddress;
                errorLog.SendAttempt = email.Attempts + 1;

                Logger.Log("Unable to start email thread", Logger.LogType.Warn);
                Logger.Log(ex, Logger.LogType.Error);
                if (emailWorker != null)
                {
                    if (emailWorker.IsAlive)
                        emailWorker.Abort();
                    emailWorker = null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Send()
        {
            try
            {
                if (EmailMessage != null)
                {
                    if (EmailMessage.To.Count > 0)
                    {
                        EmailMessage.From = new MailAddress(email.FromAddress, email.FromName);
                    }
                    else
                    {
                        errorLog.EmailID = email.EmailID;
                        errorLog.DateInserted = DateTime.Now;
                        errorLog.ErrorCode = "Address";
                        errorLog.ErrorDescription = "[WARN] Required From and To email addresses are not provided";
                        errorLog.MemberID = email.MemberID;
                        errorLog.RecipientEmailAddress = email.RecipientEmailAddress;
                        errorLog.SendAttempt = email.Attempts + 1;
                        throw new ArgumentException("Required From and To email addresses are not provided");
                    }

                    //after setting To and From Address instantiate Smtp client
                    smtpClient = new SmtpClient();

                    smtpClient = SetDefaultConfig(smtpClient);
                    smtpClient.Send(EmailMessage);

                    sendLog.EmailID = email.EmailID;
                    sendLog.SendDateTime = DateTime.Now;
                    sendLog.SendStatus = "Sent";

                }
                else
                {
                    sendLog.EmailID = email.EmailID;
                    sendLog.SendDateTime = DateTime.Now;
                    sendLog.SendStatus = "Error";

                    errorLog.EmailID = email.EmailID;
                    errorLog.DateInserted = DateTime.Now;
                    errorLog.ErrorCode = "Message";
                    errorLog.ErrorDescription = "[ERROR] EmailMessage parameters not set properly";
                    errorLog.MemberID = email.MemberID;
                    errorLog.RecipientEmailAddress = email.RecipientEmailAddress;
                    errorLog.SendAttempt = email.Attempts + 1;
                    throw new InvalidOperationException("EmailMessage parameters not set properly");
                }
            }
            catch (SmtpFailedRecipientException se)
            {
                sendLog.EmailID = email.EmailID;
                sendLog.SendDateTime = DateTime.Now;
                sendLog.SendStatus = "Error";

                errorLog.EmailID = email.EmailID;
                errorLog.DateInserted = DateTime.Now;
                errorLog.ErrorCode = "Not Reachable";
                errorLog.ErrorDescription = "[ERROR] Recipient not reachable";
                errorLog.MemberID = email.MemberID;
                errorLog.RecipientEmailAddress = email.RecipientEmailAddress;
                errorLog.SendAttempt = email.Attempts + 1;
                Logger.Log("Recipient not reachable", Logger.LogType.Error);
                Logger.Log(se, Logger.LogType.Error);
                // DO nothing as it recipeint not exists exception doesn't need to be thrown.
            }
            catch (Exception Ex)
            {
                sendLog.EmailID = email.EmailID;
                sendLog.SendDateTime = DateTime.Now;
                sendLog.SendStatus = "Error";

                errorLog.EmailID = email.EmailID;
                errorLog.DateInserted = DateTime.Now;
                errorLog.ErrorCode = "Exception";
                errorLog.ErrorDescription = "[ERROR] " + Ex.ToString();
                errorLog.MemberID = email.MemberID;
                errorLog.RecipientEmailAddress = email.RecipientEmailAddress;
                errorLog.SendAttempt = email.Attempts + 1;
                Logger.Log(Ex, Logger.LogType.Error);
            }
            finally
            {
                if (mailMessage.Attachments != null)
                {
                    for (int i = 0; i < mailMessage.Attachments.Count; i++)
                    {
                        if (mailMessage.Attachments.Contains(mailMessage.Attachments[i]))
                            mailMessage.Attachments.RemoveAt(i);
                    }
                    mailMessage.Attachments.Clear();
                    mailMessage.Dispose();
                }
                if (smtpClient != null)
                {
                    smtpClient = null;
                }
                if (emailWorker != null)
                {
                    //emailWorker.Abort();
                    emailWorker = null;
                }
                GC.Collect();
                Logger.Log("Released all resources", Logger.LogType.Info);
                // update send attempt
                db.EmailSendLogs.Add(sendLog);
                if (sendLog.SendStatus != "Sent")
                {
                    db.EmailErrorLogs.Add(errorLog);
                }

                Email updateEmail = db.Emails.Find(email.EmailID);
                updateEmail.Attempts += 1;
                updateEmail.Status = sendLog.SendStatus;

                if (emailConfig.DefaultMaxRetries > 0)
                {
                    updateEmail.FailedPermanently = (updateEmail.Attempts >= emailConfig.DefaultMaxRetries) ? true : false;
                }


                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Logger.Log("Entity of type " + eve.Entry.Entity.GetType().Name + " in state " +  eve.Entry.State + " has the following validation errors:", Logger.LogType.Info);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Logger.Log("- Property: " + ve.PropertyName + ", Error: " + ve.ErrorMessage, Logger.LogType.Info);
                        }
                    }
                }

            }
        }

        /// <summary>
        /// Sets the credentials defined in config table/file by user.
        /// </summary>
        private SmtpClient SetDefaultConfig(SmtpClient smtpClient)
        {
            if (smtpClient != null) 
            {
                if (emailConfig != null)
                {
                    // try set from address/name from table, if not, get from config file.
                    email.FromAddress = (email.FromAddress == "") ? emailConfig.DefaultFromAddress : email.FromAddress;
                    email.FromAddress = (email.FromAddress == "") ? EmailMessage.From.Address : email.FromAddress;
                    email.FromName = (email.FromName == "") ? emailConfig.DefaultFromName : email.FromName;
                    email.FromName = (email.FromName == "") ? Convert.ToString(ConfigurationManager.AppSettings["FromEmailID"]) : email.FromName;

                    smtpClient.EnableSsl = (emailConfig.smtpEnableSSL != null) ? (bool)emailConfig.smtpEnableSSL : false;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(emailConfig.smtpUsername, emailConfig.smtpPassword);
                    smtpClient.Host = emailConfig.smtpServerURI;
                    //smtpClient.Port = emailConfig.smtpPort;
                }
                else
                {
                    SmtpSection mailSettings = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                    if (mailSettings != null)
                    {
                        email.FromName = (email.FromName == "" ) ? Convert.ToString(ConfigurationManager.AppSettings["FromEmailID"]) : email.FromName;
                        email.FromAddress = (email.FromAddress == "") ? EmailMessage.From.Address : email.FromAddress;
                        smtpClient.EnableSsl = (Convert.ToString(ConfigurationManager.AppSettings["EnableSSL"]) != "") ? Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSSL"].ToString()) : false;
                        smtpClient.UseDefaultCredentials = mailSettings.Network.DefaultCredentials;
                        smtpClient.Credentials = new NetworkCredential(mailSettings.Network.UserName, mailSettings.Network.Password);
                        //smtpClient.Host = smtpHost;
                        smtpClient.Host = mailSettings.Network.Host;
                        smtpClient.Port = mailSettings.Network.Port;

                    }
                    else
                    {
                        throw new ConfigurationErrorsException("smtp configuration not specified, add <System.net><mailsettings><smtp> tags to congfig file");
                    }
                }
            }
            return smtpClient;
        }


        public string GetSMTPSetting()
        {
            return "";
            //return ConfigurationManager.AppSettings["SMTPServer"].ToString();
        }
    }
}
