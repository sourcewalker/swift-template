using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using $safeprojectname$.Extensions;

namespace $safeprojectname$
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Clear();
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            IList<object> routesData = new List<object>();

            if (!IsHolding())
            {
                routesData.Add(new { name = "Home", url = "", controller = "Home", action = "Index" });
                if (ConfigurationManager.AppSettings["Environment"] != "prod")
                {
                    routesData.Add(new { name = "Holding", url = "end-of-campaign", controller = "Home", action = "Holding" });
                }
            }
            else
            {
                routesData.Add(new { name = "Holding", url = "", controller = "Home", action = "Holding" });
            }

            routesData.Add(new { name = "TermsAndConditions", url = "terms-and-conditions", controller = "Static", action = "Terms" });
            routesData.Add(new { name = "FAQ", url = "faq", controller = "Static", action = "FAQ" });

            //Exception Route
            routesData.Add(new { name = "Error404", url = "error-404", controller = "Exception", action = "Error404", type = RouteType.Partial.ToString() });
            routesData.Add(new { name = "Error500", url = "error-500", controller = "Exception", action = "Error500", type = RouteType.Partial.ToString() });
            // SEO
            routesData.Add(new { name = "Sitemap", url = "sitemap.xml", controller = "SEO", action = "Sitemap" });
            // Shared
            routesData.Add(new { name = "Header", url = "header", controller = "Navigation", action = "Header", type = RouteType.Partial.ToString() });
            routesData.Add(new { name = "Footer", url = "footer", controller = "Navigation", action = "Footer", type = RouteType.Partial.ToString() });

            foreach (var item in routesData)
            {
                var routeName = item.GetType().GetProperty("name").GetValue(item, null).ToString();
                var routeUrl = item.GetType().GetProperty("url").GetValue(item, null).ToString();
                var routeController = item.GetType().GetProperty("controller").GetValue(item, null).ToString();
                var routeAction = item.GetType().GetProperty("action").GetValue(item, null).ToString();
                var type = item.GetType().GetProperty("type");
                var routeType = type != null ? type.GetValue(item, null).ToString() : string.Empty;
                routes.MapRoute(
                    name: routeName,
                    url: routeUrl,
                    defaults: new { controller = routeController, action = routeAction },
                    constraints: new
                    {
                        controller = @"[^\.]*"
                    }
                ).SetRouteType(routeType);
            }
        }

        private static bool IsHolding() {
            return Convert.ToDateTime(ConfigurationManager.AppSettings["EndDate"]) < DateTime.UtcNow;
        }
    }
}
