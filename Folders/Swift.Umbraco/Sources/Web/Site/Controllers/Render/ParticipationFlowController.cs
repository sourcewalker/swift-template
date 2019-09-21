using Swift.Umbraco.Business.Service.Interfaces;
using Swift.Umbraco.Models.Enum;
using System.Web.Mvc;
using Umbraco.Web.Models;

namespace $safeprojectname$.Controllers.Render
{
    public class ParticipationFlowController : BaseController
    {
        private readonly IParticipationService _participationService;

        public ParticipationFlowController(
            IParticipationService participationService,
            IConfigurationService configurationService) :
                        base(configurationService)
        {
            _participationService = participationService;
        }

        public ActionResult Participate(RenderModel model)
        {
            if (ParticipationId == default ||
               !_participationService.CheckStatus(ParticipationId, JourneyStatus.EMAIL_VALIDATED))
            {
                return Redirect("/");
            }

            return base.Index(model);
        }

        public ActionResult Congratulations(RenderModel model)
        {
            if (ParticipationId == default ||
               !_participationService.CheckStatus(ParticipationId, JourneyStatus.WON_CHECKED))
            {
                return Redirect("/");
            }

            ViewBag.PrizeName = _participationService.GetWonPrize(ParticipationId);
            return base.Index(model);
        }

        public ActionResult EntryForm(RenderModel model)
        {
            // Code for testing purpose
            if (Request.QueryString["token"] == "EVG$wLc@HC4XVb*t9")
            {
                ViewBag.Email = "test.proximity@proximitybbdo.fr";
                return base.Index(model);
            }
            // End - Testing

            if (ParticipationId == default ||
               !_participationService.CheckStatus(ParticipationId, JourneyStatus.WON_CHECKED))
            {
                return Redirect("/");
            }

            ViewBag.Email = _participationService.GetEmail(ParticipationId);
            return base.Index(model);
        }

        public ActionResult ThankYou(RenderModel model)
        {
            if (ParticipationId == default ||
              !(_participationService.CheckStatus(ParticipationId, JourneyStatus.CHEQUE_PARTICIPATION_SYNCED) ||
                _participationService.CheckStatus(ParticipationId, JourneyStatus.BACS_PARTICIPATION_SYNCED)))
            {
                return Redirect("/");
            }

            return base.Index(model);
        }

        public ActionResult Lose(RenderModel model)
        {
            if (ParticipationId == default ||
               !_participationService.CheckStatus(ParticipationId, JourneyStatus.LOST_CHECKED))
            {
                return Redirect("/");
            }

            return base.Index(model);
        }
    }
}