using Core.Infrastructure.Interfaces.Logging;
using Core.Service.Interfaces;
using Core.Shared.DTO;
using Core.Shared.Utility;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using $safeprojectname$.Filters;
using $safeprojectname$.Models;

namespace $safeprojectname$.Controllers
{
    [RequireHttps]
    [BasicAuthentication]
    [System.Web.Http.RoutePrefix("participate")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ParticipateController : ApiController
    {
        private readonly IJourneyService _journeyService;
        private readonly ILoggingProvider _logger;
        private readonly ISiteService _siteService;

        public ParticipateController(
            IJourneyService journeyService,
            ILoggingProvider logger,
            ISiteService siteService)
        {
            _journeyService = journeyService;
            _logger = logger;
            _siteService = siteService;
        }

        /// <summary>
        /// Retrieve All model configuration
        /// </summary>
        /// <param name="culture">Culture of the current request</param>
        /// <remarks>
        /// This endpoint will return :
        ///  * View Model data
        /// </remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("model")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(ApiResponse))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ApiResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ApiResponse))]
        public IHttpActionResult GetModel(string culture)
        {
            dynamic expando = new ExpandoObject();

            var apiResponse = new ApiResponse
            {
                Success = false,
                Message = "Bad Request",
                Data = expando
            };

            try
            {
                if (culture == null)
                {
                    expando.Description = "Please provide culture parameter";

                    _logger.LogWarn("Participate model error", "Culture parameter is missing");
                    return Content(HttpStatusCode.BadRequest, apiResponse);
                }

                var site = _siteService.GetSiteByCulture(culture);

                apiResponse.Success = true;
                apiResponse.Message = "Participate successfull";
                apiResponse.Data = expando;

                _logger.LogTrace("Participate Model retrieval", "Ok");

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                expando.Error = e.Message;

                apiResponse.Success = false;
                apiResponse.Message = $"Error occured in {e.Source}";
                apiResponse.Data = expando;

                _logger.LogError(e.Message, e);

                return Content(HttpStatusCode.InternalServerError, apiResponse);
            }
        }

        /// <summary>
        /// Retrieve Participate status and retailer related prize
        /// </summary>
        /// <param name="culture">Culture of the current request</param>
        /// <remarks>
        /// This endpoint will return :
        ///  * ChocolateBar List
        /// </remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("status")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(ApiResponse))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ApiResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ApiResponse))]
        public IHttpActionResult GetParticipateStatus(string culture)
        {
            dynamic expando = new ExpandoObject();

            var apiResponse = new ApiResponse
            {
                Success = false,
                Message = "Bad Request",
                Data = expando
            };

            try
            {
                if (culture == null)
                {
                    expando.Description = "Please provide culture parameter";

                    _logger.LogWarn("Participate model error", "Culture parameter is missing");
                    return Content(HttpStatusCode.BadRequest, apiResponse);
                }

                var site = _siteService.GetSiteByCulture(culture);

                apiResponse.Success = true;
                apiResponse.Message = "Participate successfull";
                apiResponse.Data = expando;

                _logger.LogTrace("Participate and prize retrieval", "Ok");

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                expando.Error = e.Message;

                apiResponse.Success = false;
                apiResponse.Message = $"Error occured in {e.Source}";
                apiResponse.Data = expando;

                _logger.LogError(e.Message, e);

                return Content(HttpStatusCode.InternalServerError, apiResponse);
            }
        }

        /// <summary>
        /// Create a participate entry
        /// </summary>
        /// <param name="participate">Participation related data needed</param>
        /// <remarks>
        /// Participation to inventor participate
        /// </remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("participate")]
        [SwaggerResponse(HttpStatusCode.OK, "Data contains 'Description', 'ConsumerId', 'ParticipationId' field containing participation informations", Type = typeof(ApiResponse))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Data contains 'Error' field which will indicates validation error lists", Type = typeof(ApiResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Data contains 'Error' field which will indicates the problem", Type = typeof(ApiResponse))]
        public async Task<IHttpActionResult> Participate([FromBody]ParticipationViewModel participate)
        {
            dynamic expando = new ExpandoObject();

            var apiResponse = new ApiResponse
            {
                Success = false,
                Message = "Bad Request",
                Data = expando
            };

            try
            {
                if (participate == null)
                {
                    expando.Error = new List<string>() { "Body request data should be as documented" };

                    apiResponse.Message = "Missing or unknown request body";
                    apiResponse.Data = expando;

                    _logger.LogWarn("Participate validation Error", "Body request not found or unknown");
                    return Content(HttpStatusCode.BadRequest, apiResponse);
                }

                if (!ModelState.IsValid)
                {
                    var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    var errorList = allErrors.Select(error => error.ErrorMessage);
                    expando.Error = errorList;

                    apiResponse.Message = "Validation error occured";
                    apiResponse.Data = expando;

                    _logger.LogWarn("Participate validation Error", string.Join(", ", errorList));

                    return Content(HttpStatusCode.BadRequest, apiResponse);
                }

                var dto = new ParticipationDto
                {
                    Id = Guid.NewGuid(),
                    EmailHash = StringUtility.Md5HashEncode(participate.Email.ToLower()),
                    Email = participate.Email,
                    SiteId = _siteService.GetSiteByCulture(participate.Culture)?.Id,
                    RetailerConsent = participate.RetailerConsent,
                    NewsletterOptin = participate.NewsletterOptin
                };

                var crmResponse = await _journeyService.ParticipateAsync(dto, participate.Culture);

                var site = _siteService.GetSiteByCulture(participate.Culture);

                expando.Description = "Your participate has been taken into account.";
                expando.ConsumerId = crmResponse.Item1 ? crmResponse.Item2 : string.Empty;
                expando.ParticipationId = dto.Id;

                apiResponse.Success = true;
                apiResponse.Message = crmResponse.Item1 ? "Participate successfull" :
                                            "Your participate has been considered but CRM sync has failed";
                apiResponse.Data = expando;

                _logger.LogTrace(apiResponse.Message, $"Ok: {dto.Id}");

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                expando.Error = e.Message;

                apiResponse.Success = false;
                apiResponse.Message = $"Error occured in {e.Source}";
                apiResponse.Data = expando;

                _logger.LogError(e.Message, e);

                return Content(HttpStatusCode.InternalServerError, apiResponse);
            }
        }
    }
}