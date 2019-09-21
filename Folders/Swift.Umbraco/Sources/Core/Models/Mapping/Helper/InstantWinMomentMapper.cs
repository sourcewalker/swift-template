
using Swift.Umbraco.$safeprojectname$.Domain;
using Swift.Umbraco.$safeprojectname$.DTO;
using System.Collections.Generic;

namespace Swift.Umbraco.$safeprojectname$.Mapping.Helper
{
    public static class InstantWinMomentMapper
    {
        public static InstantWinMomentDto toDto(this InstantWinMoment instantWinMoment)
            => AutoMapper.Mapper.Map<InstantWinMoment, InstantWinMomentDto>(instantWinMoment);

        public static InstantWinMoment toEntity(this InstantWinMomentDto instantWinMomentDto)
            => AutoMapper.Mapper.Map<InstantWinMomentDto, InstantWinMoment>(instantWinMomentDto);

        public static IEnumerable<InstantWinMoment> toEntities(this IEnumerable<InstantWinMomentDto> instantWinMomentDtos)
            => AutoMapper.Mapper.Map<IEnumerable<InstantWinMomentDto>, IEnumerable<InstantWinMoment>>(instantWinMomentDtos);

        public static IEnumerable<InstantWinMomentDto> toDtos(this IEnumerable<InstantWinMoment> instantWinMoments)
            => AutoMapper.Mapper.Map<IEnumerable<InstantWinMoment>, IEnumerable<InstantWinMomentDto>>(instantWinMoments);
    }
}
