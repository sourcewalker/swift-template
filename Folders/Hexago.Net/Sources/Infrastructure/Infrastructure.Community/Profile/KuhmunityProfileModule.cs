using $safeprojectname$.Helper;
using $safeprojectname$.Interfaces;
using $safeprojectname$.Models;
using SWEET.WebProjects.Brands.Milka.Kuhmunity.DTO;

namespace $safeprojectname$.Profile
{
    public class KuhmunityProfileModule : KuhmunityBase, IProfileProvider
    {
        UserOperationInput _userOperationData;

        public KuhmunityProfileModule(UserOperationInput operationData)
        {
            _userOperationData = operationData;
            operationData.UserId = GetUserIdFromSessionTicket(operationData.SessionTicket);
        }

        public KuhmunityResponse GetProfile()
        {
            return KuhmunityProfileHelper.GetProfile(GetKuhmunityApiEndpoint(), _userOperationData);
        }
    }
}