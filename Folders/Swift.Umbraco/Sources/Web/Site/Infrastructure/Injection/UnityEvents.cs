using Swift.Umbraco.Business.Configuration;
using Swift.Umbraco.Business.Journey;
using Swift.Umbraco.Business.Manager.Interfaces;
using Swift.Umbraco.Business.Service.Interfaces;
using Swift.Umbraco.Business.Validation;
using Swift.Umbraco.Infrastructure.DAL.Entities;
using Swift.Umbraco.Infrastructure.DAL.Petapoco;
using Swift.Umbraco.Infrastructure.DAL.Umbraco;
using Swift.Umbraco.Infrastructure.Features.Captcha.Provider;
using Swift.Umbraco.Infrastructure.Features.Hangfire;
using Swift.Umbraco.Infrastructure.Features.InstantWin.Generator;
using Swift.Umbraco.Infrastructure.Features.Interfaces;
using Swift.Umbraco.Infrastructure.Features.LogoGrab.Provider;
using Swift.Umbraco.Infrastructure.Features.ProCampaign.Provider;
using System.Web.Http;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.Editors;
using Umbraco.Web.HealthCheck;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity.Lifetime;
using Unity.RegistrationByConvention;
using MVC = Umbraco.Web.Mvc;
using WebAPI = Umbraco.Web.WebApi;

namespace $safeprojectname$
{
    public class UnityEvents : IApplicationEventHandler
    {
        public void OnApplicationInitialized(
            UmbracoApplicationBase httpApplication,
            ApplicationContext applicationContext)
        { }

        public void OnApplicationStarting(
            UmbracoApplicationBase httpApplication,
            ApplicationContext applicationContext)
        { }

        public void OnApplicationStarted(
            UmbracoApplicationBase httpApplication,
            ApplicationContext applicationContext
        )
        {
            var container = UnityConfig.Container;
            var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);

            // MVC Configuration
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // Web API Configuration
            GlobalConfiguration.Configuration.DependencyResolver =
                            new Unity.AspNet.WebApi.UnityDependencyResolver(container);

            container.RegisterTypes(
                AllClasses.FromAssemblies(typeof(UnityEvents).Assembly),
                WithMappings.FromMatchingInterface,
                WithName.Default
            );

            // -- UmbracoContext resolution for backoffice controllers --
            container.RegisterFactory<UmbracoContext>(
                (containr, type, name) => UmbracoContext.Current,
                new PerRequestLifetimeManager());
            container.RegisterFactory<IContentService>(
                (containr, type, name) => applicationContext.Services.ContentService,
                new PerRequestLifetimeManager());
            container.RegisterFactory<IMediaService>(
                (containr, type, name) => applicationContext.Services.MediaService,
                new PerRequestLifetimeManager());
            container.RegisterInstance<ITypedPublishedContentQuery>(
                            umbracoHelper.ContentQuery,
                            new ContainerControlledLifetimeManager());

            // -- Umbraco controller injection --
            container.RegisterType<RenderMvcController>(
                            new InjectionConstructor(new ResolvedParameter<UmbracoContext>())
                        );
            container.RegisterType<LegacyTreeController>(
                new InjectionConstructor());
            container.RegisterType<MVC.UmbracoAuthorizeAttribute>(
                new InjectionConstructor());
            container.RegisterType<WebAPI.UmbracoAuthorizeAttribute>(
                new InjectionConstructor());
            container.RegisterType<UserTreeController>(
                new InjectionConstructor());
            container.RegisterType<UsersController>(
                new InjectionConstructor());
            container.RegisterType<HealthCheckController>(
                new InjectionConstructor());

            // -- Application specific type registration --
            // Business Logic
            container.RegisterType<IConfigurationService, ConfigurationService>(
                new ContainerControlledLifetimeManager());
            container.RegisterType<IInstantWinService, InstantWinService>(
                new ContainerControlledLifetimeManager());
            container.RegisterType<IParticipationService, ParticipationService>(
                new ContainerControlledLifetimeManager());
            container.RegisterType<ISynchronizationService, SynchronizationService>(
                new ContainerControlledLifetimeManager());
            container.RegisterType<IValidationService, ValidationService>(
                new ContainerControlledLifetimeManager());

            // Data Access Layer
            //// Umbraco
            container.RegisterType<IPublishedContentManager, PublishedContentManager>(
                new ContainerControlledLifetimeManager());
            container.RegisterType<IContentServiceManager, ContentServiceManager>(
                new ContainerControlledLifetimeManager());
            container.RegisterType<IMediaServiceManager, MediaServiceManager>(
                new ContainerControlledLifetimeManager());
            //// Repositories
            container.RegisterType(typeof(IGenericManager<>), typeof(GenericManager<>));
            container.RegisterType<IConsumerManager, ConsumerManager>(
                new ContainerControlledLifetimeManager());
            container.RegisterType<ICountryManager, CountryManager>(
                new ContainerControlledLifetimeManager());
            container.RegisterType<IFailedTransactionManager, FailedTransactionManager>(
                new ContainerControlledLifetimeManager());
            container.RegisterType<IInstantWinMomentManager, InstantWinMomentManager>(
                new ContainerControlledLifetimeManager());
            container.RegisterType<IParticipationManager, ParticipationManager>(
                new ContainerControlledLifetimeManager());
            container.RegisterType<IParticipantManager, ParticipantManager>(
                new ContainerControlledLifetimeManager());
            container.RegisterType<IPrizeManager, PrizeManager>(
                new ContainerControlledLifetimeManager());

            // Infrastructure
            container.RegisterType<ICrmConsumerProvider, ConsumerProvider>(
                new ContainerControlledLifetimeManager());
            container.RegisterType<IFormValidatorProvider, CaptchaProvider>(
                new ContainerControlledLifetimeManager());
            container.RegisterType<ILogoValidatorProvider, LogoGrabProvider>(
                new ContainerControlledLifetimeManager());
            container.RegisterType<ISchedulerProvider, HangfireProvider>(
                new ContainerControlledLifetimeManager());
            container.RegisterType<IInstantWinMomentProvider, InstantWinProvider>(
                new ContainerControlledLifetimeManager());
        }
    }
}