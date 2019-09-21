using AutoMapper;
using Infrastructure.AutoMapper.Profiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace $safeprojectname$
{
    [TestClass]
    public class AssemblyConfig
    {
        [AssemblyInitialize]
        public static void Init(TestContext context)
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<DomainMapperProfile>();
                config.AddProfile(new DomainMapperProfile());
            });
        }

        [AssemblyCleanup]
        public static void Clean()
        {
            Mapper.Reset();
        }
    }
}
