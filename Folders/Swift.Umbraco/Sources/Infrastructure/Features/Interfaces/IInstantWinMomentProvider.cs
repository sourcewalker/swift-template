using Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace $safeprojectname$.$safeprojectname$.Interfaces
{
    public interface IInstantWinMomentProvider
    {
        Task<IList<DateTimeOffset>> GenerateWinningMoments(GeneratorConfig config);

        Task<IList<(Guid Id, string Name)>> AllocatePrizes(IList<Allocable> allocable, int instantWinNumber);
    }
}
