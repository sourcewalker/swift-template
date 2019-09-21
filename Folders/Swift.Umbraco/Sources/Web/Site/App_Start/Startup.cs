using Hangfire;
using $safeprojectname$.Infrastructure.Filters;
using Owin;
using Umbraco.Web;
using UmbracoCore = Umbraco.Core;

namespace $safeprojectname$
{
    public class Startup : UmbracoDefaultOwinStartup
    {
        public override void Configuration(IAppBuilder app)
        {
            base.Configuration(app);

            // TODO: Hangfire database installation
            #region Comment if the connectionString is not configured in web.config
            var connectionString = UmbracoCore.ApplicationContext.Current.DatabaseContext.ConnectionString;
            GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);

            var dashboardOptions = new DashboardOptions
            {
                Authorization = new[]
                {
                    new UmbracoAuthorizationFilter()
                }
            };

            app.UseHangfireDashboard("/umbraco/backoffice/Plugins/hangfire", dashboardOptions);
            app.UseHangfireServer(); 
            #endregion
        }
    }
}