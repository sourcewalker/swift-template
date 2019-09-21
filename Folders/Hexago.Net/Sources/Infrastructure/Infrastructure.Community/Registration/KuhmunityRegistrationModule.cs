using $safeprojectname$.Helper;
using $safeprojectname$.Interfaces;
using $safeprojectname$.Models;
using SWEET.WebProjects.Brands.Milka.Kuhmunity.DTO;

namespace $safeprojectname$.Registration
{
    public class KuhmunityRegistrationModule : KuhmunityBase, IRegistrationProvider
    {
        UserRegisterInput _kuhmunityProfileData;

        public KuhmunityRegistrationModule(UserRegisterInput kuhmunityData)
        {
            _kuhmunityProfileData = kuhmunityData;
        }

        public KuhmunityResponse Register()
        {
            return KuhmunityRegistrationHelper.Register(GetKuhmunityApiEndpoint(), _kuhmunityProfileData);
        }
    }
}