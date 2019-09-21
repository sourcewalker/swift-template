using AutoMapper;
//using Core.Common.Mapping;

namespace $safeprojectname$
{
    public class MappingConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                // Mapping on BLL
                //config.AddProfile<DomainMapperProfile>();
                //config.AddProfile(new DomainMapperProfile());

            });
        }
    }
}