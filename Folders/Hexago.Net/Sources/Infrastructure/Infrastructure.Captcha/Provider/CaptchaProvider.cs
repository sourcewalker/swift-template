using Core.Infrastructure.Interfaces.Validator;
using $safeprojectname$.Helper;

namespace $safeprojectname$.Provider
{
    public class CaptchaProvider : IFormValidatorProvider
    {
        public bool Validate(string response)
        {
            return GoogleRecaptchaHelper.ValidateReCaptchaV2(response);
        }
    }
}
