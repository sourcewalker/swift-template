using Swift.Umbraco.Models.DTO;
using Swift.Umbraco.Models.Enum;
using System;
using System.Threading.Tasks;

namespace Swift.Umbraco.$safeprojectname$.Service.Interfaces
{
    public interface IParticipationService
    {
        Task<(bool creationStatus, Guid participationId, Guid participantId)> GetOrCreateEmailValidatedParticipationAsync(string email);

        Task<bool> UpdateLogoValidatedParticipationAsync(ParticipationDto participation);

        Task<(bool winStatus, PrizeDto prize)> UpdateInstantWinStatusAsync(ParticipationDto participationDto);

        string GetWonPrize(Guid participationId);

        string GetEmail(Guid participationId);

        bool CheckStatus(Guid participationId, JourneyStatus status);

        Task<(bool success, string consumerId)> UpdateUserInformationAsync(UserInfoDto userInfo);
    }
}
