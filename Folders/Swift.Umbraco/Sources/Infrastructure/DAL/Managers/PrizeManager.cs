using Swift.Umbraco.Business.Manager.Interfaces;
using Swift.Umbraco.Infrastructure.$safeprojectname$.Petapoco;
using Swift.Umbraco.Models.Domain;
using Swift.Umbraco.Models.DTO;
using Swift.Umbraco.Models.Mapping.Helper;
using System;
using System.Collections.Generic;

namespace Swift.Umbraco.Infrastructure.$safeprojectname$.Entities
{
    public class PrizeManager : GenericManager<Prize>, IPrizeManager
    {
        private readonly IInstantWinMomentManager _instantMomentManager;

        public PrizeManager(IInstantWinMomentManager instantMomentManager)
        {
            _instantMomentManager = instantMomentManager;
        }

        public IEnumerable<PrizeDto> FindAll()
        {
            return GetAll().toDtos();
        }

        public PrizeDto FindById(Guid id)
        {
            return GetById(id).toDto();
        }

        public bool UpdateRemainingNumber(Guid instantMomentId, out PrizeDto prizeDto)
        {
            var instantMoment = _instantMomentManager.FindById(instantMomentId);
            var prize = GetById(instantMoment.PrizeId);
            prize.Remaining -= 1;

            prizeDto = prize.toDto();

            return Update(prize);
        }
    }
}
