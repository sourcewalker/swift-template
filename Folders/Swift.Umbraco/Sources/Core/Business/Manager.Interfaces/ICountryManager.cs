using Swift.Umbraco.Models.DTO;
using Swift.Umbraco.Models.Enum;

namespace Swift.Umbraco.$safeprojectname$.Manager.Interfaces
{
    public interface ICountryManager
    {
        CountryDto GetCountryByCode(Countries countryCode);

        CountryDto GetCountryByCulture(string culture);
    }
}
