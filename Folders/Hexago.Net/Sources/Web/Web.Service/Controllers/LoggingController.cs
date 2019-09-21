using Core.Infrastructure.Interfaces.Logging;
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
    [System.Web.Http.RoutePrefix("log")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoggingController : ApiController
    {
        private readonly ILoggingProvider _logger;

        public LoggingController(ILoggingProvider logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Logging an error to the system
        /// </summary>
        /// <param name="errorMessage">Error message</param>
        /// <param name="errorDetails">Details of the error</param>
        /// <remarks>
        /// Create an error log
        /// </remarks>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal Server Error</response>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("error")]
        [SwaggerResponse(HttpStatusCode.OK, "Data contains 'Description' field", Type = typeof(ApiResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Data contains 'Error' field which will indicates the problem", Type = typeof(ApiResponse))]
        public IHttpActionResult LogError(string errorMessage, string errorDetails)
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
                _logger.LogError(errorMessage, new Exception(errorDetails));

                expando.Description = "Error logged successfully";

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

        /// <summary>
        /// Logging an information to the system
        /// </summary>
        /// <param name="information">Information message</param>
        /// <remarks>
        /// Create an information log
        /// </remarks>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal Server Error</response>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("info")]
        [SwaggerResponse(HttpStatusCode.OK, "Data contains 'Description' field", Type = typeof(ApiResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Data contains 'Error' field which will indicates the problem", Type = typeof(ApiResponse))]
        public IHttpActionResult LogInfo(string information)
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
                _logger.LogInfo(information);

                expando.Description = "Information logged successfully";

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

        /// <summary>
        /// Logging a trace to the system
        /// </summary>
        /// <param name="message">Trace message</param>
        /// <param name="traceDetails">Trace details</param>
        /// <remarks>
        /// Create a trace log
        /// </remarks>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal Server Error</response>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("trace")]
        [SwaggerResponse(HttpStatusCode.OK, "Data contains 'Description' field", Type = typeof(ApiResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Data contains 'Error' field which will indicates the problem", Type = typeof(ApiResponse))]
        public IHttpActionResult LogTrace(string message, string traceDetails)
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
                _logger.LogTrace(message, traceDetails);

                expando.Description = "Trace logged successfully";

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

        /// <summary>
        /// Logging a warning information to the system
        /// </summary>
        /// <param name="message">Warn message</param>
        /// <param name="warnDetails">Warn details</param>
        /// <remarks>
        /// Create a warn log
        /// </remarks>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal Server Error</response>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("warn")]
        [SwaggerResponse(HttpStatusCode.OK, "Data contains 'Description' field", Type = typeof(ApiResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Data contains 'Error' field which will indicates the problem", Type = typeof(ApiResponse))]
        public IHttpActionResult LogWarn(string message, string warnDetails)
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
                _logger.LogWarn(message, warnDetails);

                expando.Description = "Warn logged successfully";

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