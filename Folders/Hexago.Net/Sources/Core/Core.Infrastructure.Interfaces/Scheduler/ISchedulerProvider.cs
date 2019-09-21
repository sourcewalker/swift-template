using $safeprojectname$.Crm;
using Core.Shared.Models;
using System;
using System.Threading.Tasks;

namespace $safeprojectname$.Scheduler
{
    public interface ISchedulerProvider
    {
        Task RetryParticipationSyncRecurrently(
            CrmData data,
            Configurations requestWideSettings,
            CronEnum occurence,
            bool requestConsumerId = false);

        Task<object> RetryParticipationSyncImmediately(
            CrmData data,
            Configurations requestWideSettings,
            bool requestConsumerId = false);

        Task DelayedParticipationRetrySync(
            CrmData data,
            Configurations requestWideSettings,
            TimeSpan delay,
            bool requestConsumerId = false);
    }
}
