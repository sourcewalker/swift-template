using $safeprojectname$.$safeprojectname$.LogoGrab.Models;
using System.Threading.Tasks;

namespace $safeprojectname$.$safeprojectname$.Interfaces
{
    public interface ILogoValidatorProvider
    {
        Task<bool> ValidateLogoAsync(string base64Image, LogoGrabSettings settings);
    }
}
