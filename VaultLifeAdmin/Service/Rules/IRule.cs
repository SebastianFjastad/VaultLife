using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using VaultLifeAdmin.Models;

namespace VaultLifeAdmin.Service.Rules
{

    public abstract class IRule : IJob
    {
        public DateTimeOffset ExecuteTime { get; set; }
        public virtual RuleType ruleType { get; set; }
        public Game game { get; set; }
        public int GameRuleId { get; set; }
        public abstract  Dictionary<String, String> getRuleData();

        public void schedule(IScheduler scheduler)
        {

            
            IJobDetail job = JobBuilder.Create<IRule>()
                .WithIdentity(GameRuleId.ToString(),ruleType.ToString())   
                
                .Build();
            
            withData(job, getRuleData());

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(GameRuleId.ToString(), ruleType.ToString())    
                .StartAt(ExecuteTime)
                .Build();
           // scheduler.DeleteJob(job.Key);
            scheduler.UnscheduleJob(trigger.Key);
            IJobDetail jobDetail = scheduler.GetJobDetail(job.Key);
            scheduler.ScheduleJob(job, trigger);            

       
        }
  
        private void withData(IJobDetail job, Dictionary<String, String> data)
        {
            foreach (String key in data.Keys)
            {
                job.JobDataMap.Add(key, data[key]);
            }
        }

        public abstract void Execute(IJobExecutionContext context);

    }


}
