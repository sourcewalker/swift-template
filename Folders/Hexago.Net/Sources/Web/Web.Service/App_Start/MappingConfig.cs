using AutoMapper;
using Infrastructure.AutoMapper.Profiles;
using $safeprojectname$.Mapping;

namespace $safeprojectname$
{
    public class MappingConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                // Mapping on BLL
                config.AddProfile<DomainMapperProfile>();
                config.AddProfile(new DomainMapperProfile());

                // Mapping on Presentation
                config.AddProfile<ViewMapperProfile>();
                config.AddProfile(new ViewMapperProfile());

            });
        }
    }
}