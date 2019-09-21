using $safeprojectname$.Models;

namespace $safeprojectname$.Interfaces
{
    public interface ILoginProvider
    {
        KuhmunityResponse Login();

        string GetLoginCookie();

        KuhmunityResponse Logout();
    }
}
