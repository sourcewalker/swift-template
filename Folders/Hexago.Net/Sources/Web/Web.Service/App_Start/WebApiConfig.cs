using System;
using System.Web.Http;
using $safeprojectname$.Extensions;
using $safeprojectname$.Filters;

namespace $safeprojectname$
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.MediaTypeMappings
                .Add(new System.Net.Http.Formatting.RequestHeaderMapping("Accept", "text/html",
                              StringComparison.InvariantCultureIgnoreCase,
                              true, "application/json"));

            config.Formatters.Add(new CsvMediaTypeFormatter());

            config.Filters.Add(new BasicAuthenticationAttribute());
        }
    }
}
