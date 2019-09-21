using Hangfire.Annotations;
using Hangfire.Dashboard;
using System.Web;
using Umbraco.Core.Security;

namespace $safeprojectname$.Infrastructure.Filters
{
    public class UmbracoAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull]DashboardContext context)
        {
            return new HttpContextWrapper(HttpContext.Current).GetUmbracoAuthTicket() != null;
        }
    }
}