using Autofac;
using Autofac.Integration.WebApi;
using Core.Infrastructure.Interfaces.DAL;
using Core.Infrastructure.Interfaces.Logging;
using Core.Infrastructure.Interfaces.Mapping;
using Core.Service.Domain;
using Core.Service.Interfaces;
using Infrastructure.AutoMapper.Provider;
using Infrastructure.DAL.EF.Repository.Implementations;
using Infrastructure.Elmah;
using System.Reflection;
using System.Web.Http;

namespace $safeprojectname$
{
    public class InjectionConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac model binder provider.
            builder.RegisterWebApiModelBinderProvider();

            // Infrastructures
            builder.RegisterType<MappingProvider>()
                   .As<IMappingProvider>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<ElmahProvider>()
                   .As<ILoggingProvider>()
                   .InstancePerLifetimeScope();

            // DAL
            builder.RegisterType<FailedTransactionRepository>()
                   .As<IFailedTransactionRepository>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<SiteRepository>()
                   .As<ISiteRepository>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<ParticipantRepository>()
                   .As<IParticipantRepository>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<ParticipationRepository>()
                   .As<IParticipationRepository>()
                   .InstancePerLifetimeScope();
            

            // Services
            builder.RegisterType<ParticipationService>()
                   .As<IParticipationService>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<ParticipantService>()
                   .As<IParticipantService>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<FailedTransactionService>()
                   .As<IFailedTransactionService>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<SiteService>()
                   .As<ISiteService>()
                   .InstancePerLifetimeScope();

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}