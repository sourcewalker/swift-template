using Core.Infrastructure.Interfaces.Configuration;
using System.Threading;
using System.Web.Mvc;
using $safeprojectname$.ViewModels;

namespace $safeprojectname$.Controllers
{
    public class NavigationController : BaseController
    {
        private readonly IConfigurationProvider _configurationProvider;

        public NavigationController(
                IConfigurationProvider configurationProvider
            ) : base(configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        // GET: Navigation
        public ActionResult Header()
        {
            var model = new HeaderViewModel
            {
                Terms = Url.RouteUrl("TermsAndConditions"),
                FAQsUrl = Url.RouteUrl("FAQ")
            };

            return PartialView("~/Views/Shared/Navigation/_Header.cshtml", model);
        }

        public ActionResult Footer()
        {
            var model = new FooterViewModel
            {
                TermsOfUseUrl = _configurationProvider.GetSharedConfig("Legal:TermsOfUseLink"),
                PrivacyNoticeUrl = string.Format(
                    _configurationProvider.GetSharedConfig("Legal:PrivacyNoticeLink"),
                    _configurationProvider.GetSharedEnvironmentConfigByCulture("Mldz:SiteId",
                        Thread.CurrentThread.CurrentCulture)),
                ContactUsUrl = string.Format(
                    _configurationProvider.GetSharedConfig("Legal:ContactUsLink"),
                    _configurationProvider.GetSharedEnvironmentConfigByCulture("Mldz:SiteId",
                        Thread.CurrentThread.CurrentCulture)),
                CookiePolicyUrl = _configurationProvider.GetSharedConfig("Legal:CookiePolicyLink")
            };

            return PartialView("~/views/shared/Navigation/_Footer.cshtml", model);
        }
    }
}