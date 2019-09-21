using Core.Shared.Models;

namespace $safeprojectname$.UserData
{
    public interface IUserDataProvider
    {
        Configurations Configuration { get; set; }

        User GetUserDetails<T>(T id);
    }
}
