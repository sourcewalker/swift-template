using Core.Shared.Models;
using Elmah;
using System;
using System.Collections;
using System.Configuration;
using System.Web;
using Configuration = $safeprojectname$.Constant.Configuration;

namespace $safeprojectname$.AdminPage
{
    public class LogPageFactory : ErrorLogPageFactory
    {
        public override IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            var environment = context.Request.Params["environment"];

            if (string.IsNullOrEmpty(environment))
            {
                if (context.Request.UrlReferrer != null && !"/stylesheet".Equals(context.Request.PathInfo, StringComparison.OrdinalIgnoreCase))
                {
                    environment = HttpUtility.ParseQueryString(context.Request.UrlReferrer.Query)["environment"];
                    var separator = context.Request.Url.Query.Length > 0 ? "&" : "?";
                    if (!string.IsNullOrEmpty(environment))
                        context.Response.Redirect($"{context.Request.RawUrl}{separator}environment={environment}");
                }
            }

            var handler = base.GetHandler(context, requestType, url, pathTranslated);

            var error = new SqlErrorLog(this.GetConnectionStringByEnvironment(environment));

            IDictionary config = (IDictionary)ConfigurationManager.GetSection("elmah/errorLog");
            error.ApplicationName = (string)config["applicationName"];

            //object v = typeof(ErrorLog).GetField("_contextKey", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            //context.Items[v] = err;

            return handler;
        }

        private string GetConnectionStringByEnvironment(string environment)
        {
            if (string.IsNullOrEmpty(environment))
            {
                return Configuration.Instance.LocalConnectionString;
            }

            if (Environments.Production.ToString().ToLower().Contains(environment))
            {
                return Configuration.Instance.ProductionConnectionString;
            }
            return Configuration.Instance.LocalConnectionString;
        }
    }
}
