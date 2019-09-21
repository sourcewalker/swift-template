using $safeprojectname$.Helper;
using $safeprojectname$.Models;

namespace $safeprojectname$.Login
{
    public partial class KuhmunityLoginModule
    {
        public KuhmunityResponse Logout()
        {
            return KuhmunityLoginHelper.Logout(GetKuhmunityApiEndpoint());
        }
    }
}