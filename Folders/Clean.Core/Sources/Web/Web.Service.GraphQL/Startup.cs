﻿using AutoMapper;
using Core.Infrastructure.Interfaces.Account;
using Core.Infrastructure.Interfaces.CRM;
using Core.Infrastructure.Interfaces.DAL;
using Core.Infrastructure.Interfaces.InstantWin;
using Core.Infrastructure.Interfaces.Logging;
using Core.Infrastructure.Interfaces.Mapping;
using Core.Infrastructure.Interfaces.Scheduler;
using Core.Infrastructure.Interfaces.Validator;
using Core.Service.Domain;
using Core.Service.Flow;
using Core.Service.Interfaces;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Hangfire;
using Hangfire.SqlServer;
using Infrastructure.AutoMapper.Profiles;
using Infrastructure.AutoMapper.Provider;
using Infrastructure.Captcha.Provider;
using Infrastructure.Community;
using Infrastructure.DAL.EF;
using Infrastructure.DAL.EF.Repository.Implementations;
using Infrastructure.Hangfire;
using Infrastructure.InstantWin.Provider;
using Infrastructure.NewRelic;
using Infrastructure.ProCampaign.Consumer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace $safeprojectname$
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });

            // Automapper configuration
            var mappingConfig = new MapperConfiguration(
                mc =>
                {
                    mc.AddProfile(new DomainMapperProfile());
                });
            services.AddSingleton(mappingConfig.CreateMapper());

            // Add runtime cache
            //services.AddMemoryCache();

            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("LocalDatabase")));

            // Add Hangfire services.
            services.AddHangfire(configuration =>
               configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(
                    Configuration.GetConnectionString("HangfireDatabase"),
                    new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        UsePageLocksOnDequeue = true,
                        DisableGlobalLocks = true
                    })
                );
            services.AddHangfireServer();

            // Infrastructures Injection
            services.AddScoped<IMappingProvider, MappingProvider>();
            services.AddScoped<IAccountProvider, KuhmunityProvider>();
            services.AddScoped<ILoggingProvider, LoggingProvider>();
            services.AddScoped<ISchedulerProvider, HangfireProvider>();
            services.AddScoped<ICrmConsumerProvider, ConsumerProvider>();
            services.AddScoped<IFormValidatorProvider, CaptchaProvider>();
            services.AddScoped<IInstantWinMomentProvider, InstantWinProvider>();

            // DAL Injection
            services.AddScoped<IFailedTransactionRepository, FailedTransactionRepository>();
            services.AddScoped<ISiteRepository, SiteRepository>();
            services.AddScoped<IParticipationRepository, ParticipationRepository>();
            services.AddScoped<IParticipantRepository, ParticipantRepository>();

            // Services Injection
            services.AddScoped<IParticipationService, ParticipationService>();
            services.AddScoped<IParticipantService, ParticipantService>();
            services.AddScoped<IFailedTransactionService, FailedTransactionService>();
            services.AddScoped<ISurveyService, SurveyService>();
            services.AddScoped<ISiteService, SiteService>();
            services.AddScoped<IJourneyService, JourneyService>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<ILegalService, LegalService>();

            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<AppSchema>();

            services.AddGraphQL(o => { o.ExposeExceptions = false; })
                .AddGraphTypes(ServiceLifetime.Scoped);

            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseGraphQL<AppSchema>();
            app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Ping}/{action=Index}/{id?}");
            });
        }
    }
}
