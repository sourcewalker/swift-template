using Core.Infrastructure.Interfaces.Configuration;
using Core.Infrastructure.Interfaces.Logging;
using Core.Service.Interfaces;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Dynamic;
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
    [System.Web.Http.RoutePrefix("legal")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LegalController : ApiController
    {
        private readonly IConfigurationProvider _configProvider;
        private readonly ILegalService _legalService;
        private readonly ILoggingProvider _logger;

        public LegalController(
            IConfigurationProvider configProvider,
            ILegalService legalService,
            ILoggingProvider logger)
        {
            _configProvider = configProvider;
            _legalService = legalService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieve Terms and conditions/ Privacy legal text
        /// </summary>
        /// <remarks>
        /// Get Legal text
        /// </remarks>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal Server Error</response>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("privacy")]
        [SwaggerResponse(HttpStatusCode.OK, "Data contains 'Terms' field which contains the terms legal text", Type = typeof(ApiResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Data contains 'Error' field which will indicates the problem", Type = typeof(ApiResponse))]
        public async Task<IHttpActionResult> GetTerms()
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
                expando.Terms = await _legalService.GetPrivacyPolicyTextAsync();

                apiResponse.Success = true;
                apiResponse.Message = "Operation success";
                apiResponse.Data = expando;

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                expando = new ExpandoObject();
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
