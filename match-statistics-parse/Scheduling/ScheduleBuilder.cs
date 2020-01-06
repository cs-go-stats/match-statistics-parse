using System;
using System.Threading.Tasks;
using CSGOStats.Services.Core.Scheduling;
using Microsoft.Extensions.Configuration;
using Quartz;

namespace CSGOStats.Services.MatchStatisticsParse.Scheduling
{
    public static class ScheduleExtensions
    {
        public static async Task ConfigureJobs(
            IScheduler scheduler, 
            IServiceProvider serviceProvider,
            IConfigurationRoot configuration)
        {
            await scheduler.ScheduleJob(
                jobDetail: serviceProvider.CreateJobTemplate<GamePageStatisticsParseJob>(),
                trigger: SchedulerExtensions.CreateCronScheduledTriggerFromConfiguration(configuration, "Jobs:GamePageParse:CronExpression"));
            await scheduler.ScheduleJob(
                jobDetail: serviceProvider.CreateJobTemplate<MapPageStatisticsParseJob>(),
                trigger: SchedulerExtensions.CreateCronScheduledTriggerFromConfiguration(configuration, "Jobs:MapPageParse:CronExpression"));
        }
    }
}