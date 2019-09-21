using Core.Infrastructure.Interfaces.Configuration;
using System.Web.Mvc;
using $safeprojectname$.Services.Interfaces;

namespace $safeprojectname$.Controllers
{
    public class StaticController : BaseController
    {
        private readonly IApiService _apiService;

        // GET: Static
        public StaticController(
                IConfigurationProvider configurationProvider,
                IApiService apiService
            ) : base(configurationProvider)
        {
            _apiService = apiService;
        }

        public ActionResult Terms()
        {
            // var termsModel = await _apiService.GetPrivacyAsync();
            // return View(new HtmlString(termsModel));
            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }
    }
}