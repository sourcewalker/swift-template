using Models.DTO;
using $safeprojectname$.$safeprojectname$.InstantWin.Allocator.Factory;
using $safeprojectname$.$safeprojectname$.InstantWin.Generator.Factory;
using $safeprojectname$.$safeprojectname$.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace $safeprojectname$.$safeprojectname$.InstantWin.Generator
{
    public class InstantWinProvider : IInstantWinMomentProvider
    {
        public async Task<IList<DateTimeOffset>> GenerateWinningMoments(GeneratorConfig config)
        {
            var generator = GeneratorFactory.Create(ProviderConfiguration.Generator.algorithm);
            return await Task.Run(() => generator.Generate(config));
        }

        public async Task<IList<(Guid Id, string Name)>> AllocatePrizes(IList<Allocable> allocable, int instantWinNumber)
        {
            var allocator = AllocatorFactory.Create(ProviderConfiguration.Allocator.algorithm);
            return await Task.Run(() => allocator.Allocate(allocable, instantWinNumber));
        }
    }
}
