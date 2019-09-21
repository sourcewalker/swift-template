
using System;

namespace $safeprojectname$
{
    public interface IValidationService
    {
        bool ValidateCaptcha(string response);

        bool HasAlreadyParticipated(string email);
    }
}
