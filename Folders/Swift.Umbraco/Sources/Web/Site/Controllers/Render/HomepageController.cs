using Swift.Umbraco.Business.Service.Interfaces;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web.Models;

namespace $safeprojectname$.Controllers.Render
{
    public class HomepageController : BaseController
    {
        private readonly IValidationService _validationService;

        public HomepageController(
            IConfigurationService configurationService,
            IValidationService validationService)
            : base(configurationService)
        {
            _validationService = validationService;
        }

        public async Task<ActionResult> Homepage(RenderModel model)
        {
            ViewBag.IsOpenHour = await _validationService.IsOpenHoursForParticipationAsync();
            return base.Index(model);
        }
    }
}