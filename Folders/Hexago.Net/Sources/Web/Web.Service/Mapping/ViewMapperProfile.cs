using AutoMapper;
using Core.Shared.DTO;
using $safeprojectname$.Models;

namespace $safeprojectname$.Mapping
{
    public class ViewMapperProfile : Profile
    {
        public ViewMapperProfile()
        {
            CreateMap<ExtractModel, ParticipationDto>();
            CreateMap<ParticipationDto, ExtractModel>()
                .ForMember(dest => dest.ParticipationDate,
                           opts => opts.MapFrom(src => src.CreatedDate));
        }
    }
}