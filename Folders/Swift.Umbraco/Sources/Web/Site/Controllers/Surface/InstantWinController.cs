using Swift.Umbraco.Business.Service.Interfaces;
using Swift.Umbraco.Models.DTO;
using $safeprojectname$.Extensions.Storage;
using $safeprojectname$.Models;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace $safeprojectname$.Controllers.Surface
{
    public class InstantWinController : SurfaceController
    {
        private readonly IParticipationService _participationService;
        private readonly IValidationService _validationService;
        private readonly IInstantWinService _instantWinService;
        private readonly IConfigurationService _configurationService;

        public InstantWinController(
            IConfigurationService configurationService,
            IParticipationService participationService,
            IValidationService validationService,
            IInstantWinService instantWinService)
        {
            _configurationService = configurationService;
            _participationService = participationService;
            _validationService = validationService;
            _instantWinService = instantWinService;
        }

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> LogoAndWin([FromBody]LogoWinRequest model)
        {
            if (model.PhotoInput == null)
            {
                ViewBag.Error = "PICTURE_REQUIRED";
                return CurrentUmbracoPage();
            }
            if (model.ParticipationId == null || model.ParticipationId == default)
            {
                ViewBag.Error = "PARTICIPATION_ID_REQUIRED";
                return CurrentUmbracoPage();
            }
            if (model.ParticipantId == null || model.ParticipantId == default)
            {
                ViewBag.Error = "PARTICIPANT_ID_REQUIRED";
                return CurrentUmbracoPage();
            }

            var filePath = FileHelper.StoreFileTemporarily(model.PhotoInput);
            bool isLogoValid = await _validationService.CheckValidLogoAsync(filePath);
            FileHelper.RemoveTemporarilyStoredFile(filePath);

            if (!isLogoValid)
            {
                ViewBag.Error = "LOGO_INVALID";
                return CurrentUmbracoPage();
            }

            var participation = new ParticipationDto
            {
                Id = model.ParticipationId,
                ParticipantId = model.ParticipantId
            };

            await _participationService.UpdateLogoValidatedParticipationAsync(participation);

            var instantWinResult = await _participationService.UpdateInstantWinStatusAsync(participation);

            var congratsOrLosePageId = instantWinResult.winStatus ?
                                        _configurationService.GetCongratulationPageId() :
                                        _configurationService.GetLosePageId();

            return RedirectToUmbracoPage(congratsOrLosePageId);
        }
    }
}