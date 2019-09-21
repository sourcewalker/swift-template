using Newtonsoft.Json;
using Swift.Umbraco.Business.Helper;
using Swift.Umbraco.Business.Service.Interfaces;
using Swift.Umbraco.Models.DTO;
using $safeprojectname$.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace $safeprojectname$.Controllers.Render
{
    public class BaseController : RenderMvcController
    {
        private readonly IConfigurationService _configurationService;

        protected Guid ParticipationId { get; set; }

        protected Guid ParticipantId { get; set; }

        public BaseController(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var config = _configurationService.GetCampaignConfiguration();
            HandleCampaignGate(filterContext, config);
            var cookies = filterContext.HttpContext.Request.Cookies;
            ConfigureTagging(cookies);
        }

        private void HandleCampaignGate(ActionExecutingContext filterContext, CampaignConfiguration config)
        {
            var excludedList = _configurationService.GetNotRedirectedPages().Select(p => p.GetPath());

            if (filterContext.RequestContext.HttpContext.Request.Url == null ||
                excludedList.Any(filterContext.RequestContext.HttpContext.Request.Url.AbsoluteUri.Contains))
                return;

            if (DateTime.UtcNow < config.StartDate)
            {
                filterContext.Result = new RedirectResult(
                                _configurationService.GetStaticPagePathByName("Holding"));
                return;
            }

            if (config.EndDate < DateTime.UtcNow)
            {
                filterContext.Result = new RedirectResult(
                                _configurationService.GetStaticPagePathByName("End Of Campaign"));
                return;
            }
        }

        private void ConfigureTagging(HttpCookieCollection cookies)
        {
            var tagObject = GetDataLayer(cookies);
            ParticipationId = tagObject.participationID;
            ParticipantId = tagObject.participantID;
            TempData["datalayer"] = tagObject;
        }

        private DataLayerModel GetDataLayer(HttpCookieCollection cookies)
        {
            var cookieName = ConfigurationManager.AppSettings["Datalayer:CookieName"];
            var sessionName = ConfigurationManager.AppSettings["Datalayer:SessionName"];

            var tagObject = new DataLayerModel();

            if (cookies.Get(cookieName) != null)
            {
                if (Session[sessionName] != null)
                {
                    var sessionTag = Session[sessionName] as DataLayerModel;
                    var cookieTag = JsonConvert.DeserializeObject<DataLayerModel>(cookies.Get(cookieName).Value);
                    tagObject = cookieTag.Equals(sessionTag) ? sessionTag : cookieTag;
                }
                else
                {
                    tagObject = JsonConvert.DeserializeObject<DataLayerModel>(cookies.Get(cookieName).Value);
                    Session[sessionName] = tagObject;
                }
            }
            else
            {
                if (Session[sessionName] != null)
                {
                    tagObject = Session[sessionName] as DataLayerModel;
                }
            }

            return tagObject;
        }
    }
}