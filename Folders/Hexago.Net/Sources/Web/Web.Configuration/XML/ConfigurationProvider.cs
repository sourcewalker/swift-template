using Core.Infrastructure.Interfaces.Configuration;
using Core.Shared.Models;
using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using $safeprojectname$.Models.Elements;
using $safeprojectname$.Sections;

namespace $safeprojectname$.XML
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private readonly EnvironmentContextSection _envContextSection;

        public ConfigurationProvider()
        {
            _envContextSection = ConfigurationManager.GetSection("EnvironmentContext") as EnvironmentContextSection;
        }

        public string GetConfigByCultureAndEnvironment(string key, CultureInfo culture, Environments environment = Environments.Local)
        {
            var currentEnvironment = (from EnvironmentElement env in _envContextSection.Environments
                                      where env.Name.ToLower() == environment.ToString().ToLower()
                                      select env)?.FirstOrDefault();
            var currentSite = (from SiteElement site in currentEnvironment?.Sites
                               where !string.IsNullOrEmpty(site.Culture) && site.Culture == culture.Name
                               select site)?.FirstOrDefault();
            var config = (from AddElement add in currentSite?.Adds
                          where add.Key == key
                          select add)?.FirstOrDefault();
            return config?.Value;
        }

        public CultureInfo GetCultureByUrl(string Url)
        {
            var currentEnvironment = (from EnvironmentElement env in _envContextSection.Environments
                                      where env.Name == "Shared"
                                      select env)?.FirstOrDefault();
            var currentSite = (from SiteElement site in currentEnvironment?.Sites
                               where !string.IsNullOrEmpty(site.Domain) && Url.Contains(site.Domain)
                               select site)?.FirstOrDefault();
            return currentSite != null ?
                        CultureInfo.GetCultureInfo(currentSite.Culture) :
                        throw new NullReferenceException("No Configured Site for the current Url");
        }

        public string GetSharedCultureConfigByEnvironment(string key, Environments environment = Environments.Local)
        {
            var currentEnvironment = (from EnvironmentElement env in _envContextSection.Environments
                                      where env.Name == environment.ToString()
                                      select env)?.FirstOrDefault();
            var currentSite = (from SiteElement site in currentEnvironment?.Sites
                               where site.Name == "Shared"
                               select site)?.FirstOrDefault();
            var config = (from AddElement add in currentSite?.Adds
                          where add.Key == key
                          select add)?.FirstOrDefault();
            return config?.Value;
        }

        public string GetSharedEnvironmentConfigByCulture(string key, CultureInfo culture)
        {
            var currentEnvironment = (from EnvironmentElement env in _envContextSection.Environments
                                      where env.Name == "Shared"
                                      select env)?.FirstOrDefault();
            var currentSite = (from SiteElement site in currentEnvironment?.Sites
                               where !string.IsNullOrEmpty(site.Culture) && site.Culture == culture.Name
                               select site)?.FirstOrDefault();
            var config = (from AddElement add in currentSite?.Adds
                          where add.Key == key
                          select add)?.FirstOrDefault();
            return config?.Value;
        }

        public string GetSitenameByCulture(CultureInfo culture)
        {
            var currentEnvironment = (from EnvironmentElement env in _envContextSection.Environments
                                      where env.Name == "Shared"
                                      select env)?.FirstOrDefault();
            var currentSite = (from SiteElement site in currentEnvironment?.Sites
                               where !string.IsNullOrEmpty(site.Culture) && site.Culture == culture.Name
                               select site)?.FirstOrDefault();
            return currentSite != null ?
                        currentSite.Name :
                        throw new NullReferenceException("No Configured Site for the current culture");
        }

        public string GetSharedConfig(string key)
        {
            var currentEnvironment = (from EnvironmentElement env in _envContextSection.Environments
                                      where env.Name == "Shared"
                                      select env)?.FirstOrDefault();
            var currentSite = (from SiteElement site in currentEnvironment?.Sites
                               where site.Name == "Shared"
                               select site)?.FirstOrDefault();
            var config = (from AddElement add in currentSite?.Adds
                          where add.Key == key
                          select add)?.FirstOrDefault();
            return config?.Value;
        }
    }
}
