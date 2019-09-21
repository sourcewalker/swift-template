using Swift.Umbraco.$safeprojectname$.Manager.Interfaces;
using Swift.Umbraco.$safeprojectname$.Service.Interfaces;
using Swift.Umbraco.Infrastructure.Features.Interfaces;
using Swift.Umbraco.Infrastructure.Features.LogoGrab.Models;
using Swift.Umbraco.Models.Enum;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Swift.Umbraco.$safeprojectname$.Validation
{
    public class ValidationService : IValidationService
    {
        private readonly ILogoValidatorProvider _logoValidator;
        private readonly IParticipationManager _participationManager;
        private readonly IParticipantManager _participantManager;
        private readonly IFormValidatorProvider _formValidator;


        public ValidationService(
            ILogoValidatorProvider logoValidator,
            IFormValidatorProvider formValidator,
            IParticipationManager participationManager,
            IParticipantManager participantManager)
        {
            _logoValidator = logoValidator;
            _participantManager = participantManager;
            _formValidator = formValidator;
            _participationManager = participationManager;
        }

        public async Task<(bool status, string errorMessage)> CanUserParticipateAsync(string email)
        {
            var participant = await Task.Run(() =>
                        _participantManager.GetByEmail(email));

            // Accept if User has not yet participated
            if (participant == null)
            {
                return (true, "USER_CAN_PARTICIPATE");
            }

            // Reject if user has participated today
            if (participant.LastParticipatedDate != default &&
                participant.LastParticipatedDate.ToUniversalTime().Date == DateTime.UtcNow.Date)
            {
                return (false, "DAILY_PARTICIPATION_LIMIT_REACHED");
            }

            // Reject if user last won is sooner than one week ago
            if (participant.LastWonDate != default &&
                participant.LastWonDate.ToUniversalTime().Date.AddDays(7) > DateTime.UtcNow.Date)
            {
                return (false, "WEEKLY_WINNING_LIMIT_REACHED");
            }

            return (true, "USER_CAN_PARTICIPATE");
        }

        public async Task<bool> CheckValidLogoAsync(string base64Image)
        {
            var settings = new LogoGrabSettings
            {
                ApiUrl = ConfigurationManager.AppSettings["LogoGrab:Url"],
                DeveloperKey = ConfigurationManager.AppSettings["LogoGrab:Key"]
            };
            return await _logoValidator.ValidateLogoAsync(base64Image, settings);
        }

        public async Task<(bool Status, Guid ParticipationId, Guid ParticipantId)> HasNotCompletedPreviousWonFlow(string email)
        {
            var participations = await Task.Run(() =>
                        _participationManager.FindAllByEmail(email));

            Guid participationId = default;
            Guid participantId = default;

            if (participations == null || !participations.Any())
            {
                return (false, participationId, participantId);
            }

            var participation = participations.FirstOrDefault(
                                    p => p.JourneyStatus == JourneyStatus.WON_CHECKED.ToString());
            var status = participation != null;
            participationId = status ? participation.Id : default;
            participantId = status ? participation.ParticipantId : default;

            return (status, participationId, participantId);
        }

        public async Task<bool> IsOpenHoursForParticipationAsync()
        {
            // Getting UK and IE Time
            var currentTime = DateTime.UtcNow.AddHours(1);

            return await Task.Run(() =>
                // Check between 8 am and 9 pm
                currentTime.Hour >= 8 && currentTime.Hour < 21);
        }

        public bool ValidateCaptcha(string token)
        {
            return _formValidator.ValidateCaptcha(token);
        }
    }
}
