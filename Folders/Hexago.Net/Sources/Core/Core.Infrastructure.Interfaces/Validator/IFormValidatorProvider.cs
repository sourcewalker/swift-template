namespace $safeprojectname$.Validator
{
    public interface IFormValidatorProvider
    {
        bool Validate(string response);
    }
}
