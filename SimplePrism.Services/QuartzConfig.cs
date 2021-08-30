using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace SimplePrism.Services
{
    /// <summary>
    /// Quartz config
    /// </summary>
    public class QuartzConfig
    {
        private static readonly Lazy<ISchedulerFactory> factory =
            new Lazy<ISchedulerFactory>(() => new StdSchedulerFactory());

        private static readonly Lazy<IScheduler> scheduler =
            new Lazy<IScheduler>(() => factory.Value.GetScheduler());

        /// <summary>
        /// 
        /// </summary>
        public static ISchedulerFactory Factory => factory.Value;

        /// <summary>
        /// 
        /// </summary>
        public static IScheduler Scheduler => scheduler.Value;

    }
}
