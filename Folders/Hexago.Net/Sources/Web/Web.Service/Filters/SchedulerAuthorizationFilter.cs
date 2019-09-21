using Hangfire.Dashboard;
using System.Configuration;
using System.Security.Principal;
using System.Threading;

namespace $safeprojectname$.Filters
{
    public class SchedulerAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var filter = new BasicAuthAuthorizationFilter(
            new BasicAuthAuthorizationFilterOptions
            {
                RequireSsl = true,
                LoginCaseSensitive = true,
                Users = new[]
                {
                    new BasicAuthAuthorizationUser
                    {
                        Login = ConfigurationManager.AppSettings["BasicAuth:Proximity:Key"],
                        PasswordClear = ConfigurationManager.AppSettings["BasicAuth:Proximity:Password"]
                    },
                    new BasicAuthAuthorizationUser
                    {
                        Login = ConfigurationManager.AppSettings["BasicAuth:System:Key"],
                        PasswordClear = ConfigurationManager.AppSettings["BasicAuth:System:Password"]
                    },
                    new BasicAuthAuthorizationUser
                    {
                        Login = ConfigurationManager.AppSettings["BasicAuth:External:Key"],
                        PasswordClear = ConfigurationManager.AppSettings["BasicAuth:External:Password"]
                    }
                }
            });

            var hasAccess = filter.Authorize(context.GetOwinEnvironment());

            if (hasAccess)
            {
                var user = new GenericIdentity("Hangfire");
                var principal = new GenericPrincipal(user, null);
                Thread.CurrentPrincipal = principal;
            }

            return hasAccess;
        }
    }
}