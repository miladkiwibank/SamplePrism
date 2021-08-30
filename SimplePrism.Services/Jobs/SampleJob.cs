using NLog;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePrism.Services.Jobs
{
    class SampleJob : IJob
    {
        private readonly ILogger m_logger = LogManager.GetCurrentClassLogger();

        public void Execute(IJobExecutionContext context)
        {
            //var param = context.JobDetail.JobDataMap.GetString(""); //任务启动传参
            m_logger.Debug("Sample job executed");
        }
    }
}
