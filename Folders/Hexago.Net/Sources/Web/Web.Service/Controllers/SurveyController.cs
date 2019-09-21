using Core.Infrastructure.Interfaces.Logging;
using Core.Service.Interfaces;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using $safeprojectname$.Extensions;
using $safeprojectname$.Filters;
using $safeprojectname$.Mapping.Helper;
using $safeprojectname$.Models;

namespace $safeprojectname$.Controllers
{
    [RequireHttps]
    [BasicAuthentication]
    [System.Web.Http.RoutePrefix("survey")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SurveyController : ApiController
    {
        private readonly ISurveyService _surveyService;
        private readonly ILoggingProvider _logger;

        public SurveyController(
            ISurveyService surveyService,
            ILoggingProvider logger)
        {
            _logger = logger;
            _surveyService = surveyService;
        }

        /// <summary>
        /// Retrieve votes between a time frame to json
        /// </summary>
        /// <param name="from">Start date of the export(string should be datetime convertible: 
        /// "yyyy-MM-ddTHH:mm:ss" or
        /// "yyyy-MM-ddTHH:mm" or
        /// "yyyy-MM-dd" or
        /// "yyyy/MM/dd" or
        /// "YYYY/MM")
        /// </param>
        /// <param name="to">End date of the export(string should be datetime convertible:
        /// "yyyy-MM-ddTHH:mm:ss" or
        /// "yyyy-MM-ddTHH:mm" or
        /// "yyyy-MM-dd" or
        /// "yyyy/MM/dd" or
        /// "YYYY/MM")
        /// </param>
        /// <remarks>
        /// Extract votes between start and end
        /// </remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("extract/json")]
        [SwaggerResponse(HttpStatusCode.OK, "Data contains 'Export' field which will contains the survey data", Type = typeof(ApiResponse))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Data contains 'Error' field which will indicates validation error lists", Type = typeof(ApiResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Data contains 'Error' field which will indicates the problem", Type = typeof(ApiResponse))]
        public IHttpActionResult ExtractToJson(string from = "", string to = "")
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
                var startDate = DateTimeOffset.MinValue;

                if (!string.IsNullOrEmpty(from))
                {
                    if (DateTimeOffset.TryParse(from, out var startOut))
                    {
                        startDate = startOut;
                    }
                    else
                    {
                        _logger.LogWarn("Extract Error", $"On {DateTimeOffset.UtcNow.ToString()}");

                        expando.Error = new List<string>() { "Start date invalid: Please enter a valid date" };
                        apiResponse.Message = "Validation error occured";
                        apiResponse.Data = expando;

                        return Content(HttpStatusCode.BadRequest, apiResponse);
                    }
                }

                var endDate = DateTimeOffset.MaxValue;

                if (!string.IsNullOrEmpty(to))
                {
                    if (DateTimeOffset.TryParse(to, out var endOut))
                    {
                        endDate = endOut;
                    }
                    else
                    {
                        _logger.LogWarn("Extract Error", $"On {DateTimeOffset.UtcNow.ToString()}");

                        expando.Error = "End date invalid: Please enter a valid date";
                        apiResponse.Message = "Validation error occured";
                        apiResponse.Data = expando;

                        return Content(HttpStatusCode.BadRequest, apiResponse);
                    }
                }

                if (endDate.TimeOfDay.TotalSeconds == 0)
                    endDate = new DateTimeOffset(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59, TimeSpan.Zero);

                expando.Export = _surveyService.ExtractParticipation(startDate, endDate);

                apiResponse.Success = true;
                apiResponse.Message = "Extracted successfully";
                apiResponse.Data = expando;

                _logger.LogWarn("Extract Success", $"On {DateTime.UtcNow.ToString()}");

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
        /// Retrieve votes between a time frame in csv
        /// </summary>
        /// <param name="from">Start date of the export(string should be datetime convertible: 
        /// "yyyy-MM-ddTHH:mm:ss" or
        /// "yyyy-MM-ddTHH:mm" or
        /// "yyyy-MM-dd" or
        /// "yyyy/MM/dd" or
        /// "YYYY/MM")
        /// </param>
        /// <param name="to">End date of the export(string should be datetime convertible:
        /// "yyyy-MM-ddTHH:mm:ss" or
        /// "yyyy-MM-ddTHH:mm" or
        /// "yyyy-MM-dd" or
        /// "yyyy/MM/dd" or
        /// "YYYY/MM")
        /// </param>
        /// <remarks>
        /// Extract votes between start and end to csv
        /// </remarks>
        /// <response code="200">Ok</response>
        /// <response code="406">Not Acceptable</response>
        /// <response code="500">Internal Server Error</response>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("extract/csv")]
        [SwaggerResponse(HttpStatusCode.OK, "Dates are valid")]
        [SwaggerResponse(HttpStatusCode.NotAcceptable, "Error parsing dates (No Content in body)")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Internal server error (No Content in body)")]
        public HttpResponseMessage ExtractToCsv(string from = "", string to = "")
        {
            try
            {
                var startDate = DateTime.MinValue;

                if (!string.IsNullOrEmpty(from))
                {
                    if (DateTime.TryParse(from, out var startOut))
                    {
                        startDate = startOut;
                    }
                    else
                    {
                        _logger.LogWarn("Extract Error", $"On {DateTimeOffset.UtcNow.ToString()}");

                        throw new HttpResponseException(
                          new HttpResponseMessage(HttpStatusCode.NotAcceptable));
                    }
                }

                var endDate = DateTime.MaxValue;

                if (!string.IsNullOrEmpty(to))
                {
                    if (DateTime.TryParse(to, out var endOut))
                    {
                        endDate = endOut;
                    }
                    else
                    {
                        _logger.LogWarn("Extract Error", $"On {DateTimeOffset.UtcNow.ToString()}");

                        throw new HttpResponseException(
                          new HttpResponseMessage(HttpStatusCode.NotAcceptable));
                    }
                }

                if (endDate.TimeOfDay.TotalSeconds == 0)
                    endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);

                var votes = _surveyService.ExtractParticipation(startDate, endDate);
                var voteExtracts = votes.toExtractModels();
                var csvFormatter = new CsvMediaTypeFormatter();

                var response = new HttpResponseMessage
                {
                    Content = new ObjectContent<List<ExtractModel>>(
                        voteExtracts.ToList(),
                        csvFormatter,
                        "text/csv"
                    )
                };

                response.Content.Headers.ContentDisposition =
                    new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "Export.csv"
                    };

                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);

                throw new HttpResponseException(
                          new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }
    }
}
