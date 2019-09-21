﻿using Core.Shared.Models;
using System.Globalization;

namespace $safeprojectname$.Configuration
{
    public interface IConfigurationProvider
    {
        CultureInfo GetCultureByUrl(string Url);

        string GetSitenameByCulture(CultureInfo culture);

        string GetSharedConfig(string key);

        string GetSharedCultureConfigByEnvironment(string key, Environments environment = Environments.Local);

        string GetSharedEnvironmentConfigByCulture(string key, CultureInfo culture);

        string GetConfigByCultureAndEnvironment(string key, CultureInfo culture, Environments environment = Environments.Local);
    }
}
