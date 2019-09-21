using AutoMapper;
using Core.Shared.DTO;
using System.Collections.Generic;
using $safeprojectname$.Models;

namespace $safeprojectname$.Mapping
{
    public class ExtractMapper : IExtractMapper
    {
        private readonly IMapper _mapper;

        public ExtractMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ExtractModel toExtractModel(ParticipationDto participation)
            => _mapper.Map<ParticipationDto, ExtractModel>(participation);

        public IEnumerable<ExtractModel> toExtractModels(IEnumerable<ParticipationDto> participations)
            => _mapper.Map<IEnumerable<ParticipationDto>, IEnumerable<ExtractModel>>(participations);
    }
}
