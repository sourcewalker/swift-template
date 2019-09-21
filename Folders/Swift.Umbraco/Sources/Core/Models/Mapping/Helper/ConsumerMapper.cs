using Swift.Umbraco.$safeprojectname$.Domain;
using Swift.Umbraco.$safeprojectname$.DTO;
using System.Collections.Generic;

namespace Swift.Umbraco.$safeprojectname$.Mapping.Helper
{
    public static class ConsumerMapper
    {
        public static ConsumerDto toDto(this Consumer consumer)
            => AutoMapper.Mapper.Map<Consumer, ConsumerDto>(consumer);

        public static Consumer toEntity(this ConsumerDto consumerDto)
            => AutoMapper.Mapper.Map<ConsumerDto, Consumer>(consumerDto);

        public static IEnumerable<Consumer> toEntities(this IEnumerable<ConsumerDto> consumerDtos)
            => AutoMapper.Mapper.Map<IEnumerable<ConsumerDto>, IEnumerable<Consumer>>(consumerDtos);

        public static IEnumerable<ConsumerDto> toDtos(this IEnumerable<Consumer> consumers)
            => AutoMapper.Mapper.Map<IEnumerable<Consumer>, IEnumerable<ConsumerDto>>(consumers);
    }
}
