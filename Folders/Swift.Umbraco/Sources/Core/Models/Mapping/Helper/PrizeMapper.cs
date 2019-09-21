
using Swift.Umbraco.$safeprojectname$.Domain;
using Swift.Umbraco.$safeprojectname$.DTO;
using System.Collections.Generic;

namespace Swift.Umbraco.$safeprojectname$.Mapping.Helper
{
    public static class PrizeMapper
    {
        public static PrizeDto toDto(this Prize prize)
            => AutoMapper.Mapper.Map<Prize, PrizeDto>(prize);

        public static Prize toEntity(this PrizeDto prizeDto)
            => AutoMapper.Mapper.Map<PrizeDto, Prize>(prizeDto);

        public static IEnumerable<Prize> toEntities(this IEnumerable<PrizeDto> prizeDtos)
            => AutoMapper.Mapper.Map<IEnumerable<PrizeDto>, IEnumerable<Prize>>(prizeDtos);

        public static IEnumerable<PrizeDto> toDtos(this IEnumerable<Prize> prizes)
            => AutoMapper.Mapper.Map<IEnumerable<Prize>, IEnumerable<PrizeDto>>(prizes);
    }
}
