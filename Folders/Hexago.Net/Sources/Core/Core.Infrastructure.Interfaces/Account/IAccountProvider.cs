using Core.Shared.Models;

namespace $safeprojectname$.Account
{
    public interface IAccountProvider
    {
        Configurations Configuration { get; set; }

        LoginResult Login(LoginInfo login);

        LoginResult Logout();

        string GetLoginCookie();

        LoginResult Register(RegisterInfo user);
    }
}
