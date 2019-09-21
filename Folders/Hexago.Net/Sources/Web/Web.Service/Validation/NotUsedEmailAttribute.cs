using Core.Service.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Web.Http;

namespace $safeprojectname$.Validation
{
    public class NotUsedEmail : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            var dependencyResolver = GlobalConfiguration.Configuration.DependencyResolver;
            var validationService = (IValidationService)dependencyResolver.GetService(typeof(IValidationService));

            return !validationService.HasAlreadyParticipated((string)value) ?
                        ValidationResult.Success :
                        new ValidationResult(ErrorMessage);

        }
    }
}