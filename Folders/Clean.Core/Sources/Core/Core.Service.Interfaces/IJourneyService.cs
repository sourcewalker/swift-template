using Core.Shared.DTO;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    public interface IJourneyService
    {
        Task<(bool, string)> ParticipateAsync(ParticipationDto participation, string culture, string country = "GB");
    }
}
