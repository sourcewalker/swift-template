using Core.Shared.Models;
using System.Threading.Tasks;

namespace $safeprojectname$.CRM
{
    public interface ICrmConsumerProvider
    {
        Configurations Configuration { get; set; }

        Task<CrmData> CreateParticipationAsync(
            CrmData data,
            Configurations requestWideSettings,
            bool requestConsumerId = false);

        Task<CrmData> ReadTextDocumentAsync();
    }
}
