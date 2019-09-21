using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using $safeprojectname$.Constants;

namespace $safeprojectname$
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static Phase _currentPhase;

        protected void Application_Start()
        {
            InjectionConfig.Configure();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            MappingConfig.Configure();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public static void SetPhase(Phase phase)
        {
            _currentPhase = phase;
        }

        public static Phase GetPhase()
        {
            return _currentPhase;
        }

        public static bool CheckRouteByPath(string path)
        {

            var isRoutePresent = false;
            foreach (Route route in RouteTable.Routes)
            {
                if (route.Url == path)
                {
                    isRoutePresent = true;
                }
            }
            return isRoutePresent;
        }
    }
}
