using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace $safeprojectname$.Extensions
{
    public static class RouteDataExtension
    {
        public static string GetRouteType(this Route route)
        {
            return route?.DataTokens.GetRouteType();
        }

        public static string GetRouteType(this RouteData routeData)
        {
            return routeData?.DataTokens.GetRouteType();
        }

        private static string GetRouteType(this RouteValueDictionary routeValues)
        {
            if (routeValues == null)
            {
                return null;
            }
            routeValues.TryGetValue("__RouteType", out var routeName);
            return routeName as string;
        }

        public static Route SetRouteType(this Route route, string routeType)
        {
            if (route == null)
            {
                throw new ArgumentNullException(nameof(route));
            }
            if (route.DataTokens == null)
            {
                route.DataTokens = new RouteValueDictionary();
            }
            if (string.IsNullOrEmpty(routeType))
                routeType = RouteType.Page.ToString();
            route.DataTokens["__RouteType"] = routeType;
            return route;
        }
    }
}