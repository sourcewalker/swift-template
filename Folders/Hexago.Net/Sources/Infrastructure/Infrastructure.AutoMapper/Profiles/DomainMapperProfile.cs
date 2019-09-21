using AutoMapper;
using Core.Model;
using Core.Shared.DTO;

namespace $safeprojectname$.Profiles
{
    public class DomainMapperProfile : Profile
    {
        public DomainMapperProfile()
        {
            CreateMap<Site, SiteDto>();
            CreateMap<SiteDto, Site>();

            CreateMap<Participation, ParticipationDto>();
            CreateMap<ParticipationDto, Participation>();

            CreateMap<Participant, ParticipantDto>();
            CreateMap<ParticipantDto, Participant>();

            CreateMap<FailedTransaction, FailedTransactionDto>();
            CreateMap<FailedTransactionDto, FailedTransaction>();
        }
    }
}
