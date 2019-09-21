using $safeprojectname$.Helper;
using $safeprojectname$.Interfaces;
using $safeprojectname$.Models;
using Microsoft.Extensions.Configuration;
using SWEET.WebProjects.Brands.Milka.Kuhmunity.DTO;

namespace $safeprojectname$.Registration
{
    public class KuhmunityRegistrationModule : KuhmunityBase, IRegistrationProvider
    {
        private UserRegisterInput _kuhmunityProfileData;
        private readonly IConfiguration _configuration;

        public KuhmunityRegistrationModule(IConfiguration configuration, UserRegisterInput kuhmunityData)
        {
            _kuhmunityProfileData = kuhmunityData;
            _configuration = configuration;
        }

        public KuhmunityResponse Register()
        {
            return KuhmunityRegistrationHelper.Register(GetKuhmunityApiEndpoint(_configuration), _kuhmunityProfileData);
        }
    }
}
