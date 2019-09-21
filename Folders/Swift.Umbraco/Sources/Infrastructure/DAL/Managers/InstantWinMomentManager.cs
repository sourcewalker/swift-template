using Swift.Umbraco.Business.Manager.Interfaces;
using Swift.Umbraco.Infrastructure.$safeprojectname$.Petapoco;
using Swift.Umbraco.Models.Domain;
using Swift.Umbraco.Models.DTO;
using Swift.Umbraco.Models.Mapping.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Swift.Umbraco.Infrastructure.$safeprojectname$.Entities
{
    public class InstantWinMomentManager : GenericManager<InstantWinMoment>, IInstantWinMomentManager
    {
        public InstantWinMomentManager()
        {

        }

        public InstantWinMomentDto FindById(Guid id)
        {
            return GetById(id).toDto();
        }

        public InstantWinMomentDto CheckAvailableWinningMoment()
        {
            //var query = new Sql()
            //                .Select("*")
            //                .From<InstantWinMoment>(_sqlProvider)
            //                .Where<InstantWinMoment>(moment =>
            //                            !moment.IsWon &&
            //                            moment.ActivationDate.ToUniversalTime() <= DateTime.UtcNow, _sqlProvider)
            //                .OrderBy<InstantWinMoment>(m => m.ActivationDate, _sqlProvider);
            var sqlQuery = "SELECT * " +
                           "FROM InstantWinMoment " +
                           "WHERE IsWon = 'False' " +
                                "AND ActivationDate <= @0 " +
                           "ORDER BY ActivationDate";
            return _database.Fetch<InstantWinMoment>(sqlQuery, DateTime.UtcNow).FirstOrDefault().toDto();
        }

        public bool MarkAsWon(InstantWinMomentDto momentDto)
        {
            var moment = GetById(momentDto.Id);
            moment.IsWon = true;
            return Update(moment);
        }

        public bool Create(InstantWinMomentDto momentDto)
        {
            return Create(momentDto);
        }

        public IEnumerable<InstantWinMomentDto> FindPaged()
        {
            return GetAll().Take(3).toDtos();
        }
    }
}
