using Core.Infrastructure.Interfaces.Configuration;
using System.Web.Mvc;

namespace $safeprojectname$.Controllers
{
    public class ExceptionController : BaseController
    {
        public ExceptionController(IConfigurationProvider configurationProvider) :
            base(configurationProvider)
        { }

        public ActionResult Error404()
        {
            return View();
        }

        public ActionResult Error500()
        {
            return View();
        }
    }
}