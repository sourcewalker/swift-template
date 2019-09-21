using System;
using System.Collections.Generic;

namespace $safeprojectname$.InstantWin
{
    public interface IInstantWinMomentProvider
    {
        IList<DateTimeOffset> GenerateWinningMoments(GeneratorConfig config);

        IList<(Guid Id, string Name)> AllocatePrizes(IList<Allocable> allocable, int instantWinNumber);
    }
}
