using Swift.Umbraco.$safeprojectname$.Helper;
using Swift.Umbraco.$safeprojectname$.Manager.Interfaces;
using Swift.Umbraco.$safeprojectname$.Service.Interfaces;
using Swift.Umbraco.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Swift.Umbraco.$safeprojectname$.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IPublishedContentManager _contentManager;

        public ConfigurationService(IPublishedContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public CampaignConfiguration GetCampaignConfiguration()
        {
            var configContent = _contentManager.GetTypedContentSingleAtXPath("//configurationContainer/activations");
            return new CampaignConfiguration
            {
                StartDate = configContent.GetPropertyValue<DateTime>("campaignStartData"),
                EndDate = configContent.GetPropertyValue<DateTime>("campaignEndDate")
            };
        }

        public string GetStaticPagePathByName(string name)
        {
            var staticPage = _contentManager.GetTypedContentAtXPath("//specificStaticPage")
                                            .First(c => c.Name == name);
            return staticPage.GetPath();
        }

        public string GetPageUrlByName(string name)
        {
            var staticPage = _contentManager.GetTypedContentAtXPath("//homepage")
                                            .First()
                                            .Descendants()
                                            .First(c => c.Name == name);
            return staticPage.GetPath();
        }

        public IEnumerable<IPublishedContent> GetNotRedirectedPages()
        {
            return _contentManager.GetTypedContentSingleAtXPath("//configurationContainer/activations")
                                                   .GetPropertyValue<IEnumerable<IPublishedContent>>("notRedirectedPages");
        }

        public int GetCongratulationPageId()
        {
            var congratsPage = _contentManager.GetTypedContentAtXPath("//congratulations")
                                            .First(c => c.Name == "Congratulations");
            return congratsPage.Id;
        }

        public int GetLosePageId()
        {
            var lostPage = _contentManager.GetTypedContentAtXPath("//lost")
                                            .First(c => c.Name == "Lose");
            return lostPage.Id;
        }
    }
}
