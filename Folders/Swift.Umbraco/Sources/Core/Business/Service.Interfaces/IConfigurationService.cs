using System.Collections.Generic;
using Swift.Umbraco.Models.DTO;
using Umbraco.Core.Models;

namespace Swift.Umbraco.$safeprojectname$.Service.Interfaces
{
    public interface IConfigurationService
    {
        CampaignConfiguration GetCampaignConfiguration();

        string GetStaticPagePathByName(string name);

        string GetPageUrlByName(string name);

        IEnumerable<IPublishedContent> GetNotRedirectedPages();

        int GetCongratulationPageId();

        int GetLosePageId();

    }
}
