using Swift.Umbraco.Models.DTO;
using System;
using System.Collections.Generic;

namespace Swift.Umbraco.$safeprojectname$.Manager.Interfaces
{
    public interface IInstantWinMomentManager
    {
        bool MarkAsWon(InstantWinMomentDto momentDto);

        InstantWinMomentDto CheckAvailableWinningMoment();

        InstantWinMomentDto FindById(Guid id);

        IEnumerable<InstantWinMomentDto> FindPaged();

        bool Create(InstantWinMomentDto momentDto);
    }
}
