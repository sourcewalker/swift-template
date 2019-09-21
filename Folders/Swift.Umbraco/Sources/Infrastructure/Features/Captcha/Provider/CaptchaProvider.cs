using $safeprojectname$.$safeprojectname$.Captcha.Helper;
using $safeprojectname$.$safeprojectname$.Interfaces;

namespace $safeprojectname$.$safeprojectname$.Captcha.Provider
{
    public class CaptchaProvider : IFormValidatorProvider
    {
        public bool ValidateCaptcha(string captchaResponse)
        {
            return GoogleRecaptchaHelper.ValidateReCaptchaV2(captchaResponse);
        }
    }
}
