using Swift.Umbraco.Business.Manager.Interfaces;
using Swift.Umbraco.Infrastructure.$safeprojectname$.Petapoco;
using Swift.Umbraco.Models.Domain;
using Swift.Umbraco.Models.DTO;
using Swift.Umbraco.Models.Enum;
using Swift.Umbraco.Models.Mapping.Helper;
using System.Linq;

namespace Swift.Umbraco.Infrastructure.$safeprojectname$.Entities
{
    public class CountryManager : GenericManager<Country>, ICountryManager
    {
        public CountryManager()
        {
        }

        public CountryDto GetCountryByCode(Countries countryCode)
        {
            //var query = new Sql()
            //                .Select("*")
            //                .From<Country>(_sqlProvider)
            //                .Where<Country>(c => c.Code == countryCode.ToString(), _sqlProvider);
            var sqlQuery = "SELECT * FROM Country WHERE Code = @0";
            var country = _database.Fetch<Country>(sqlQuery, countryCode.ToString()).FirstOrDefault();
            return country?.toDto();
        }

        public CountryDto GetCountryByCulture(string culture)
        {
            //var query = new Sql()
            //                .Select("*")
            //                .From<Country>(_sqlProvider)
            //                .Where<Country>(c => c.Culture == culture, _sqlProvider);
            var sqlQuery = "SELECT * FROM Country WHERE Culture = @0";
            var country = _database.Fetch<Country>(sqlQuery, culture).FirstOrDefault();
            return country?.toDto();
        }
    }
}
