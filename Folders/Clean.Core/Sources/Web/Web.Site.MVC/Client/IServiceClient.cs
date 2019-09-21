using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using $safeprojectname$.ViewModels;

namespace $safeprojectname$.Client
{
    public interface IServiceClient
    {
        Task<List<SiteViewModel>> GetAllSitesAsync();

        Task<SiteViewModel> GetSiteByCultureAsync(string culture);
    }
}
