using Core.Infrastructure.Interfaces.Validator;
using $safeprojectname$.Helper;
using Microsoft.Extensions.Configuration;

namespace $safeprojectname$.Provider
{
    public class CaptchaProvider : IFormValidatorProvider
    {
        private readonly IConfiguration _config;

        public CaptchaProvider(IConfiguration config)
        {
            _config = config;
        }

        public bool Validate(string response)
        {
            return GoogleRecaptchaHelper.ValidateReCaptchaV2(_config, response);
        }
    }
}
