using AutoMapper;
using Swift.Umbraco.$safeprojectname$.Domain;
using Swift.Umbraco.$safeprojectname$.DTO;
using Swift.Umbraco.$safeprojectname$.Utility;
using System;

namespace Swift.Umbraco.$safeprojectname$.Mapping
{
    public class DomainMapperProfile : Profile
    {
        public DomainMapperProfile()
        {
            var utcTimeZoneInfo = TimeZoneInfo.Utc;

            Mapper.CreateMap<Consumer, ConsumerDto>()
                .ForMember(dest => dest.CountryEnum,
                           opts => opts.Ignore())
                .ForMember(dest => dest.UpdatedOn,
                           opts => opts.MapFrom(src => src.UpdatedOn.AddOffset(utcTimeZoneInfo)))
                .ForMember(dest => dest.CreatedOn,
                           opts => opts.MapFrom(src => src.CreatedOn.AddOffset(utcTimeZoneInfo)));
            Mapper.CreateMap<ConsumerDto, Consumer>()
                .ForMember(dest => dest.UpdatedOn,
                           opts => opts.MapFrom(src => src.UpdatedOn.ToUtc()))
                .ForMember(dest => dest.CreatedOn,
                           opts => opts.MapFrom(src => src.CreatedOn.ToUtc()));

            Mapper.CreateMap<Country, CountryDto>()
                .ForMember(dest => dest.UpdatedOn,
                           opts => opts.MapFrom(src => src.UpdatedOn.AddOffset(utcTimeZoneInfo)))
                .ForMember(dest => dest.CreatedOn,
                           opts => opts.MapFrom(src => src.CreatedOn.AddOffset(utcTimeZoneInfo)));
            Mapper.CreateMap<CountryDto, Country>()
                .ForMember(dest => dest.UpdatedOn,
                           opts => opts.MapFrom(src => src.UpdatedOn.ToUtc()))
                .ForMember(dest => dest.CreatedOn,
                           opts => opts.MapFrom(src => src.CreatedOn.ToUtc()));

            Mapper.CreateMap<FailedTransaction, FailedTransactionDto>()
                .ForMember(dest => dest.UpdatedOn,
                           opts => opts.MapFrom(src => src.UpdatedOn.AddOffset(utcTimeZoneInfo)))
                .ForMember(dest => dest.CreatedOn,
                           opts => opts.MapFrom(src => src.CreatedOn.AddOffset(utcTimeZoneInfo)));
            Mapper.CreateMap<FailedTransactionDto, FailedTransaction>()
                .ForMember(dest => dest.UpdatedOn,
                           opts => opts.MapFrom(src => src.UpdatedOn.ToUtc()))
                .ForMember(dest => dest.CreatedOn,
                           opts => opts.MapFrom(src => src.CreatedOn.ToUtc()));

            Mapper.CreateMap<InstantWinMoment, InstantWinMomentDto>()
                .ForMember(dest => dest.UpdatedOn,
                           opts => opts.MapFrom(src => src.UpdatedOn.AddOffset(utcTimeZoneInfo)))
                .ForMember(dest => dest.CreatedOn,
                           opts => opts.MapFrom(src => src.CreatedOn.AddOffset(utcTimeZoneInfo)))
                .ForMember(dest => dest.ActivationDate,
                           opts => opts.MapFrom(src => src.ActivationDate.AddOffset(utcTimeZoneInfo)));
            Mapper.CreateMap<InstantWinMomentDto, InstantWinMoment>()
                .ForMember(dest => dest.UpdatedOn,
                           opts => opts.MapFrom(src => src.UpdatedOn.ToUtc()))
                .ForMember(dest => dest.CreatedOn,
                           opts => opts.MapFrom(src => src.CreatedOn.ToUtc()))
                .ForMember(dest => dest.ActivationDate,
                           opts => opts.MapFrom(src => src.ActivationDate.ToUtc()));

            Mapper.CreateMap<Participation, ParticipationDto>()
                .ForMember(dest => dest.Email,
                           opts => opts.Ignore())
                .ForMember(dest => dest.UpdatedOn,
                           opts => opts.MapFrom(src => src.UpdatedOn.AddOffset(utcTimeZoneInfo)))
                .ForMember(dest => dest.CreatedOn,
                           opts => opts.MapFrom(src => src.CreatedOn.AddOffset(utcTimeZoneInfo)));
            Mapper.CreateMap<ParticipationDto, Participation>()
                .ForMember(dest => dest.UpdatedOn,
                           opts => opts.MapFrom(src => src.UpdatedOn.ToUtc()))
                .ForMember(dest => dest.CreatedOn,
                           opts => opts.MapFrom(src => src.CreatedOn.ToUtc()));

            Mapper.CreateMap<Participant, ParticipantDto>()
                .ForMember(dest => dest.Country,
                           opts => opts.Ignore())
                .ForMember(dest => dest.ConsumerId,
                           opts => opts.Ignore())
                .ForMember(dest => dest.UpdatedOn,
                           opts => opts.MapFrom(src => src.UpdatedOn.AddOffset(utcTimeZoneInfo)))
                .ForMember(dest => dest.CreatedOn,
                           opts => opts.MapFrom(src => src.CreatedOn.AddOffset(utcTimeZoneInfo)))
                .ForMember(dest => dest.LastWonDate,
                           opts => opts.MapFrom(src => src.LastWonDate.AddOffset(utcTimeZoneInfo)))
                .ForMember(dest => dest.LastParticipatedDate,
                           opts => opts.MapFrom(src => src.LastParticipatedDate.AddOffset(utcTimeZoneInfo)));
            Mapper.CreateMap<ParticipantDto, Participant>()
                .ForMember(dest => dest.UpdatedOn,
                           opts => opts.MapFrom(src => src.UpdatedOn.ToUtc()))
                .ForMember(dest => dest.CreatedOn,
                           opts => opts.MapFrom(src => src.CreatedOn.ToUtc()))
                .ForMember(dest => dest.LastWonDate,
                           opts => opts.MapFrom(src => src.LastWonDate.ToUtc()))
                .ForMember(dest => dest.LastParticipatedDate,
                           opts => opts.MapFrom(src => src.LastParticipatedDate.ToUtc()));

            Mapper.CreateMap<Prize, PrizeDto>()
                .ForMember(dest => dest.UpdatedOn,
                           opts => opts.MapFrom(src => src.UpdatedOn.AddOffset(utcTimeZoneInfo)))
                .ForMember(dest => dest.CreatedOn,
                           opts => opts.MapFrom(src => src.CreatedOn.AddOffset(utcTimeZoneInfo)));
            Mapper.CreateMap<PrizeDto, Prize>()
                .ForMember(dest => dest.UpdatedOn,
                           opts => opts.MapFrom(src => src.UpdatedOn.ToUtc()))
                .ForMember(dest => dest.CreatedOn,
                           opts => opts.MapFrom(src => src.CreatedOn.ToUtc()));
        }
    }
}
