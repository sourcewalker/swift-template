using Autofac;
using Autofac.Integration.Mvc;
using Core.Infrastructure.Interfaces.Configuration;
using System.Web.Mvc;
using Web.Configuration.XML;
using $safeprojectname$.Services.Helpers;
using $safeprojectname$.Services.Interfaces;

namespace $safeprojectname$
{
    public class InjectionConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            // Register dependencies in controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register dependencies in filter attributes
            builder.RegisterFilterProvider();

            // Register dependencies in custom views
            builder.RegisterSource(new ViewRegistrationSource());

            //// Web
            builder.RegisterType<ConfigurationProvider>()
                   .As<IConfigurationProvider>()
                   .InstancePerRequest();

            //// Services
            builder.RegisterType<ApiService>()
                   .As<IApiService>()
                   .InstancePerRequest();

            var container = builder.Build();

            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}