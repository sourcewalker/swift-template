using Swift.Umbraco.$safeprojectname$.Service.Interfaces;
using Swift.Umbraco.Infrastructure.Features.Interfaces;
using Swift.Umbraco.Infrastructure.Features.ProCampaign.Models;
using Swift.Umbraco.Models.DTO;
using System.Configuration;
using System.Threading.Tasks;

namespace Swift.Umbraco.$safeprojectname$.Journey
{
    public class SynchronizationService : ISynchronizationService
    {
        private readonly ICrmConsumerProvider _crmProvider;

        public SynchronizationService(ICrmConsumerProvider crmProvider)
        {
            _crmProvider = crmProvider;
            ConfigureProvider();
        }

        private void ConfigureProvider()
        {
            // Document request configuration
            _crmProvider.Configuration.AddSetting("ApiBaseUrl",
                ConfigurationManager.AppSettings["CSX:Api:BaseUrl"]);
            _crmProvider.Configuration.AddSetting("PrivacyPath",
                $"{ConfigurationManager.AppSettings["CSX:Api:DocumentPath"]}/" +
                $"{ConfigurationManager.AppSettings["CSX:Api:ListName"]}/" +
                $"{ConfigurationManager.AppSettings["CSX:Api:DocumentName"]}");
            _crmProvider.Configuration.AddSetting("InternationalApiKey",
                ConfigurationManager.AppSettings["CSX:Api:ApiKey"]);

            // Participation Transaction configuration
            _crmProvider.Configuration.AddSetting("SourceName",
                ConfigurationManager.AppSettings["CSX:Consumer:SourceName"]);
            _crmProvider.Configuration.AddSetting("TransactionName",
                ConfigurationManager.AppSettings["CSX:Consumer:TransactionName"]);
        }

        public async Task<string> GetPrivacyPolicyTextAsync()
        {
            var privacy = await _crmProvider.ReadTextDocumentAsync();

            return privacy.Data.Data.Html;
        }

        public async Task<CrmResponse> SendDataToSynchronizeAsync(CrmDto data)
        {
            _crmProvider.Configuration.AddSetting("ApiKey",
                ConfigurationManager.AppSettings[$"CSX:Consumer:ApiKey:{data.Country.ToString()}"]);
            _crmProvider.Configuration.AddSetting("ApiSecret",
                ConfigurationManager.AppSettings[$"CSX:Consumer:ApiSecret:{data.Country.ToString()}"]);

            var legalDocument = await _crmProvider.ReadTextDocumentAsync();

            var crmData = GatherParticipationDataToSync(data, legalDocument);

            var consumer = await _crmProvider.CreateParticipationAsync(crmData, _crmProvider.Configuration, true);

            return new CrmResponse
            {
                Success = consumer.GetSetting<bool>("Success"),
                ConsumerId = consumer.GetSetting<bool>("Success") ?
                                (string)consumer.Data.Data.ConsumerId : string.Empty,
                ApiStatus = consumer.GetSetting<int>("ApiStatus").ToString(),
                ApiMessage = consumer.GetSetting<string>("ApiMessage")
            };
        }

        private CrmData GatherParticipationDataToSync(CrmDto data, CrmData legalDocument)
        {
            var crmData = new CrmData();

            crmData.Data.Firstname = data.Firstname;
            crmData.Data.Lastname = data.Lastname;
            crmData.Data.Email = data.Email;
            crmData.Data.MobilePrivate = data.PhoneNumber;
            crmData.Data.Street1 = data.Address;
            crmData.Data.Street2 = data.AdditionalAddress;
            crmData.Data.City = data.City;
            crmData.Data.ZipCode = data.ZipCode;
            crmData.Data.Country = data.Country;
            crmData.Data.AccountNumber = data.AccountNumber;
            crmData.Data.SortCode = data.SortCode;
            crmData.Data.IBAN = data.IBAN;
            crmData.Data.BIC = data.BIC;

            crmData.Data.PrivacyPolicyTextName = legalDocument.Data.Data.Versions[0].LegalTextName;
            crmData.Data.PrivacyPolicyVersion = legalDocument.Data.Data.Versions[0].Version;
            crmData.Data.PrivacyPolicyCreation = legalDocument.Data.Data.Versions[0].Created.ToString();

            crmData.Data.Payment = data.PaymentType;
            crmData.Data.PrizeValue = data.PrizeValue;
            crmData.Data.ParticipationId = data.ParticipationId;

            return crmData;
        }
    }
}
