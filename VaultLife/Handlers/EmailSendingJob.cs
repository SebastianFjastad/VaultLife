using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Managers;

namespace Vaultlife.Handlers
{
    public class EmailSendingJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            // send emails - this would be called by the scheduler
            EmailSendManager esm = new EmailSendManager();
            esm.SendQueuedEmail();

        }
    }
}