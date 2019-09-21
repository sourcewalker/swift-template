using Swashbuckle.Swagger.Annotations;
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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PingController : ApiController
    {
        /// <summary>
        /// API Testing endpoint
        /// </summary>
        /// <remarks>Testing if the API is working properly</remarks>
        /// <response code="200">Ok</response>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("")]
        [SwaggerResponse(HttpStatusCode.OK, "Data contains 'Description' field", Type = typeof(ApiResponse))]
        public IHttpActionResult Index()
        {
            dynamic expando = new ExpandoObject();

            expando.Description = $"Please go to {Request.RequestUri.AbsoluteUri}swagger for API Documentation";

            var apiResponse = new ApiResponse
            {
                Success = true,
                Message = "Service is UP!",
                Data = expando
            };
            return Ok(apiResponse);
        }
    }
}
