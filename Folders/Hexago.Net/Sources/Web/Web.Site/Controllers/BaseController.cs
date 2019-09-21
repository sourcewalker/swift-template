using Core.Infrastructure.Interfaces.Configuration;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using $safeprojectname$.ViewModels;

namespace $safeprojectname$.Controllers
{
    public class BaseController : Controller
    {
        #region private fields

        private HttpContextBase _currentHttpContext;
        private readonly IConfigurationProvider _configurationProvider;

        #endregion

        #region Constructor and destructor

        public BaseController(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        #endregion

        #region Properties

        public string CurrentUrl { get; set; }

        public string CurrentHost { get; set; }

        #endregion

        #region Subscribed Events

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            _currentHttpContext = filterContext.HttpContext;
            var currentUrl = string.Empty;
            var currentHost = string.Empty;

            if (_currentHttpContext.Request.Url != null)
                currentUrl = _currentHttpContext.Request.Url.GetLeftPart(UriPartial.Path);
            currentHost = _currentHttpContext.Request.Url.GetLeftPart(UriPartial.Authority);

            var culture = _configurationProvider.GetCultureByUrl(currentUrl);
            var cookies = _currentHttpContext.Request.Cookies;

            CurrentUrl = currentUrl;
            CurrentHost = currentHost;

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            ConfigureSEO();
            ConfigureSocialMedia();
            ConfigureTagging(cookies);
        }

        #endregion

        #region Helper Methods

        private void ConfigureSEO()
        {
            TempData["Mldz_SiteId"] = _configurationProvider.GetSharedEnvironmentConfigByCulture("Mldz:SiteId",
                                            Thread.CurrentThread.CurrentCulture);
            TempData["Meta_Title"] = _configurationProvider.GetSharedEnvironmentConfigByCulture("Meta_Title",
                                            Thread.CurrentThread.CurrentCulture);
            TempData["Meta_Keywords"] = _configurationProvider.GetSharedEnvironmentConfigByCulture("Meta_Keywords",
                                            Thread.CurrentThread.CurrentCulture);
            TempData["Meta_Description"] = _configurationProvider.GetSharedEnvironmentConfigByCulture("Meta_Description",
                                                Thread.CurrentThread.CurrentCulture);
            TempData["host_Name"] = CurrentHost;
        }

        private void ConfigureSocialMedia()
        {
            TempData["FBShare_AppId"] = _configurationProvider.GetSharedConfig("FBShare:AppId");
            TempData["FBShare_Site_Name"] = _configurationProvider.GetSharedConfig("FBShare:Site_Name");
            TempData["FBShare_Image_Alt"] = _configurationProvider.GetSharedConfig("FBShare:Image_Alt");
            TempData["FBShare_Title"] = _configurationProvider.GetSharedConfig("FBShare:Title");
            TempData["FBShare_Description"] = _configurationProvider.GetSharedConfig("FBShare:Description");
        }

        private void ConfigureTagging(HttpCookieCollection cookies)
        {
            TempData["Gtm_Id"] = _configurationProvider.GetSharedEnvironmentConfigByCulture("GtmId",
                                    Thread.CurrentThread.CurrentCulture);
            TempData["Google_SV"] = _configurationProvider.GetSharedEnvironmentConfigByCulture("Google_SV",
                                        Thread.CurrentThread.CurrentCulture);
            TempData["GA_Tracking_Code"] = _configurationProvider.GetSharedEnvironmentConfigByCulture("GATrackingCode",
                                                Thread.CurrentThread.CurrentCulture);
            TempData["datalayer"] = GetDataLayer(cookies);
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

        #endregion
    }
}