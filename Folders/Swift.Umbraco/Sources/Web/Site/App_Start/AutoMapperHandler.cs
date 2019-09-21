using AutoMapper;
using Swift.Umbraco.Models.Mapping;
using Umbraco.Core;
using Umbraco.Core.Models.Mapping;

namespace $safeprojectname$
{
    public class AutoMapperHandler : MapperConfiguration
    {
        public override void ConfigureMappings(IConfiguration config, ApplicationContext applicationContext)
        {
            config.AddProfile<DomainMapperProfile>();
            config.AddProfile(new DomainMapperProfile());
        }
    }
}