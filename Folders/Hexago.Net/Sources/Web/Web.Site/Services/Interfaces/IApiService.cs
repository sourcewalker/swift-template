using System.Threading.Tasks;

namespace $safeprojectname$.Services.Interfaces
{
    public interface IApiService
    {
        Task<string> GetPrivacyAsync();
    }
}