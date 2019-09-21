using $safeprojectname$.$safeprojectname$.Interfaces;
using $safeprojectname$.$safeprojectname$.ProCampaign.Constants;
using $safeprojectname$.$safeprojectname$.ProCampaign.Helper;
using $safeprojectname$.$safeprojectname$.ProCampaign.Models;
using Swift.Umbraco.Models.DTO;
using System;
using System.Threading.Tasks;

namespace $safeprojectname$.$safeprojectname$.ProCampaign.Provider
{
    public class ConsumerProvider : ICrmConsumerProvider
    {
        public CrmConfiguration Configuration { get; set; }

        public ConsumerProvider()
        {
            Configuration = new CrmConfiguration();

            // Consumer endpoint configuration
            Configuration.Settings.ConsumerBaseUrl = ProCampaignConstants.ConsumerAPI.BaseUrl;
            Configuration.Settings.ParticipationPath = ProCampaignConstants.ConsumerAPI.ParticipationPath;

            // API endpoint configuration
            Configuration.Settings.DocumentBaseUrl = ProCampaignConstants.DocumentAPI.BaseUrl;
            Configuration.Settings.DocumentPath = $"{ProCampaignConstants.DocumentAPI.DocumentPath}/" +
                                                  $"{ProCampaignConstants.DocumentAPI.DefaultListName}/" +
                                                  $"{ProCampaignConstants.DocumentAPI.DefaultDocumentName}";
            Configuration.Settings.InternationalApiKey = $"{ProCampaignConstants.DocumentAPI.InternationalApiKey}";
        }

        public async Task<CrmData> CreateParticipationAsync(
            CrmData data,
            CrmConfiguration requestWideSettings,
            bool requestConsumerId = false)
        {
            data.AddSetting("SourceName", requestWideSettings.Settings.SourceName);
            data.AddSetting("TransactionName", requestWideSettings.Settings.TransactionName);

            var settings = new ProCampaignSettings
            {
                ConsumerBaseUrl = new Uri(Configuration.Settings.ConsumerBaseUrl),
                ParticipationPath = Configuration.Settings.ParticipationPath,
                ApiKey = requestWideSettings.Settings.ApiKey,
                ApiSecret = requestWideSettings.Settings.ApiSecret
            };
            var ApiData = ProCampaignData.FormatParticipationData(data);

            var response = await ApiHelper.PostParticipationAsync(ApiData, settings, requestConsumerId);

            var returnData = new CrmData();
            returnData.AddSetting("Success", response.IsSuccessful);
            returnData.AddSetting("ApiStatus", response.StatusCode);
            returnData.AddSetting("ApiMessage", response.StatusMessage);
            returnData.AddSetting("HttpStatus", response.HttpStatusCode);
            returnData.AddSetting("HttpMessage", response.HttpStatusMessage);
            returnData.AddSetting("Data", response.Data);

            return returnData;
        }

        public async Task<CrmData> ReadTextDocumentAsync()
        {
            var settings = new ProCampaignSettings
            {
                ApiBaseUrl = new Uri(Configuration.Settings.DocumentBaseUrl),
                DocumentPath = Configuration.Settings.DocumentPath,
                InternationalApiKey = Configuration.Settings.InternationalApiKey
            };

            var response = await ApiHelper.GetPermissionTextAsync(settings);

            var returnData = new CrmData();
            returnData.AddSetting("Success", response.IsSuccessful);
            returnData.AddSetting("ApiStatus", response.StatusCode);
            returnData.AddSetting("ApiMessage", response.StatusMessage);
            returnData.AddSetting("HttpStatus", response.HttpStatusCode);
            returnData.AddSetting("HttpMessage", response.HttpStatusMessage);
            returnData.AddSetting("Data", response.Data);

            return returnData;
        }
    }
}
