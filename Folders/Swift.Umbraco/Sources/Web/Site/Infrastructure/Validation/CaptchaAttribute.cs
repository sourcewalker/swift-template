using Swift.Umbraco.Business.Service.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Web.Http;

namespace $safeprojectname$.Infrastructure.Validation
{
    public class Captcha : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            var dependencyResolver = GlobalConfiguration.Configuration.DependencyResolver;
            var validationService = (IValidationService)dependencyResolver.GetService(typeof(IValidationService));

            return validationService.ValidateCaptcha((string)value) ?
                        ValidationResult.Success :
                        new ValidationResult(ErrorMessage);
        }
    }
}