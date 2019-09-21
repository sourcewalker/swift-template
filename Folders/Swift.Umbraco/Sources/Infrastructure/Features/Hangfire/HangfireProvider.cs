using Hangfire;
using Hangfire.Storage;
using Hangfire.Storage.Monitoring;
using $safeprojectname$.$safeprojectname$.Interfaces;
using $safeprojectname$.$safeprojectname$.ProCampaign.Models;
using Swift.Umbraco.Models.DTO;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace $safeprojectname$.$safeprojectname$.Hangfire
{
    public class HangfireProvider : ISchedulerProvider
    {
        public async Task DelayedParticipationRetrySync(
           CrmData data,
           CrmConfiguration requestWideSettings,
           TimeSpan delay,
           bool requestConsumerId = false)
        {
            await Task.Run(() =>
                    BackgroundJob.Schedule<ICrmConsumerProvider>(
                        iCrmProvider =>
                             iCrmProvider.CreateParticipationAsync(
                                 data,
                                 requestWideSettings,
                                 requestConsumerId),
                             delay));
        }

        public async Task<object> RetryParticipationSyncImmediately(
            CrmData data,
            CrmConfiguration requestWideSettings,
            bool requestConsumerId = false)
        {
            var jobId = await Task.Run(() => BackgroundJob.Enqueue<ICrmConsumerProvider>(
                            iCrmProvider =>
                                iCrmProvider.CreateParticipationAsync(
                                    data,
                                    requestWideSettings,
                                    requestConsumerId)));

            var result = new object();
            IMonitoringApi monitoringApi = JobStorage.Current.GetMonitoringApi();
            JobDetailsDto jobDetails = monitoringApi.JobDetails(jobId);
            SucceededJobDto jobDto = monitoringApi.SucceededJobs(0, int.MaxValue)
                                                    .First()
                                                    //.FirstOrDefault(job => job.Key == "Key")
                                                    .Value;
            if (jobDto != null)
            {
                result = jobDto.Result;
                return JsonConvert.DeserializeObject<CrmResponse>(result.ToString());
            }

            return null;
        }

        public async Task RetryParticipationSyncRecurrently(
            CrmData data,
            CrmConfiguration requestWideSettings,
            CronEnum occurence,
            bool requestConsumerId = false)
        {
            var frequency = MapjobFrequency(occurence);

            await Task.Run(() => RecurringJob.AddOrUpdate<ICrmConsumerProvider>(
                                    iCrmProvider =>
                                        iCrmProvider.CreateParticipationAsync(
                                            data,
                                            requestWideSettings,
                                            requestConsumerId),
                                        frequency));
        }

        private string MapjobFrequency(CronEnum occurence)
        {
            var frequency = Cron.Daily();

            switch (occurence)
            {
                case CronEnum.Daily:
                    frequency = Cron.Daily();
                    break;
                case CronEnum.Minutely:
                    frequency = Cron.Minutely();
                    break;
                case CronEnum.Hourly:
                    frequency = Cron.Hourly();
                    break;
                case CronEnum.Weekly:
                    frequency = Cron.Weekly();
                    break;
                case CronEnum.Monthly:
                    frequency = Cron.Monthly();
                    break;
                case CronEnum.Yearly:
                    frequency = Cron.Yearly();
                    break;
                default:
                    break;
            }

            return frequency;
        }
    }
}
