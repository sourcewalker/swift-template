using Core.Infrastructure.Interfaces.Configuration;
using Core.Infrastructure.Interfaces.Logging;
using Core.Service.Interfaces;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Dynamic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using $safeprojectname$.Filters;
using $safeprojectname$.Models;

namespace $safeprojectname$.Controllers
{
    [RequireHttps]
    [BasicAuthentication]
    [System.Web.Http.RoutePrefix("config")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ConfigurationController : ApiController
    {
        private readonly IConfigurationProvider _configProvider;
        private readonly ISiteService _siteService;
        private readonly ILoggingProvider _logger;

        public ConfigurationController(
            IConfigurationProvider configProvider,
            ISiteService siteService,
            ILoggingProvider logger)
        {
            _configProvider = configProvider;
            _siteService = siteService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieve site configuration culture related
        /// </summary>
        /// <param name="culture">Culture associated to the site</param>
        /// <remarks>
        /// Get Site configuration by culture
        /// </remarks>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal Server Error</response>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getsitebyculture")]
        [SwaggerResponse(HttpStatusCode.OK, "Data contains 'Site' field which contains the site configuration", Type = typeof(ApiResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Data contains 'Error' field which will indicates the problem", Type = typeof(ApiResponse))]
        public IHttpActionResult GetSiteByCulture(string culture)
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
                expando.Site = _siteService.GetSiteByCulture(culture);

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