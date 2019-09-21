using Models.DTO;
using Swift.Umbraco.$safeprojectname$.Manager.Interfaces;
using Swift.Umbraco.$safeprojectname$.Service.Interfaces;
using Swift.Umbraco.Infrastructure.Features.Interfaces;
using Swift.Umbraco.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swift.Umbraco.$safeprojectname$.Journey
{
    public class InstantWinService : IInstantWinService
    {
        private static readonly object _instantWinLock = new object();
        private readonly IInstantWinMomentManager _instantWinManager;
        private readonly IPrizeManager _prizeManager;
        private readonly IInstantWinMomentProvider _instantWinProvider;

        public InstantWinService(
            IInstantWinMomentManager instantWinManager,
            IInstantWinMomentProvider instantWinProvider,
            IPrizeManager prizeManager)
        {
            _instantWinManager = instantWinManager;
            _instantWinProvider = instantWinProvider;
            _prizeManager = prizeManager;
        }

        public (bool isWinner, PrizeDto prize, InstantWinMomentDto instantWin) WinCheck()
        {
            PrizeDto prizeDto = null;
            InstantWinMomentDto momentDto = null;
            var isWinner = false;

            lock (_instantWinLock)
            {
                var winMoment = _instantWinManager.CheckAvailableWinningMoment();
                isWinner = winMoment != null;

                if (isWinner)
                {

                    _prizeManager.UpdateRemainingNumber(winMoment.Id, out prizeDto);
                    _instantWinManager.MarkAsWon(winMoment);
                    momentDto = winMoment;
                }
            }

            return (isWinner, prizeDto, momentDto);
        }

        public async Task<(bool status, int generatedNumber)> GenerateInstantWinMoments(GeneratorConfig config, List<Allocable> allocables)
        {
            var instantList = await _instantWinProvider.GenerateWinningMoments(config);

            var allocatedPrizes = await _instantWinProvider.AllocatePrizes(allocables, instantList.Count);

            var counter = 0;
            for (int index = 0; index < allocatedPrizes.Count; index++)
            {
                var instantWin = new InstantWinMomentDto
                {
                    Id = Guid.NewGuid(),
                    PrizeId = allocatedPrizes[index].Id,
                    IsWon = false,
                    CreatedOn = DateTime.UtcNow,
                    ActivationDate = instantList[index]
                };
                _instantWinManager.Create(instantWin);
                counter++;
            }

            return (counter == allocatedPrizes.Count, counter);
        }

        public async Task<IEnumerable<PrizeDto>> GetPrizes()
        {
            return await Task.Run(() => _prizeManager.FindAll());
        }

        public async Task<IEnumerable<Allocable>> GetAllocables()
        {
            return await Task.Run(() =>
                    _prizeManager.FindAll().Select(p =>
                        new Allocable
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Number = 0
                        })
                );
        }

        public async Task<IEnumerable<InstantWinMomentDto>> GetMoments()
        {
            return await Task.Run(() => _instantWinManager.FindPaged());
        }

        public async Task<IEnumerable<string>> GetLimitOptions()
        {
            return await Task.Run(() =>
                Enum.GetValues(typeof(GeneratorLimitOptions))
                    .Cast<GeneratorLimitOptions>()
                    .Select(x => x.ToString()));
        }
    }
}
