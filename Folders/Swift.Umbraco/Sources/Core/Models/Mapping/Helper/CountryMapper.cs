
using Swift.Umbraco.$safeprojectname$.Domain;
using Swift.Umbraco.$safeprojectname$.DTO;
using System.Collections.Generic;

namespace Swift.Umbraco.$safeprojectname$.Mapping.Helper
{
    public static class CountryMapper
    {
        public static CountryDto toDto(this Country country)
            => AutoMapper.Mapper.Map<Country, CountryDto>(country);

        public static Country toEntity(this CountryDto countryDto)
            => AutoMapper.Mapper.Map<CountryDto, Country>(countryDto);

        public static IEnumerable<Country> toEntities(this IEnumerable<CountryDto> countryDtos)
            => AutoMapper.Mapper.Map<IEnumerable<CountryDto>, IEnumerable<Country>>(countryDtos);

        public static IEnumerable<CountryDto> toDtos(this IEnumerable<Country> countrys)
            => AutoMapper.Mapper.Map<IEnumerable<Country>, IEnumerable<CountryDto>>(countrys);
    }
}
