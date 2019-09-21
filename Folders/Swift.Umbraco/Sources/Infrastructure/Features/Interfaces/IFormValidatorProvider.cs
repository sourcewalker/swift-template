namespace $safeprojectname$.$safeprojectname$.Interfaces
{
    public interface IFormValidatorProvider
    {
        bool ValidateCaptcha(string captchaResponse);
    }
}
