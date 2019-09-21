using Models.DTO;
using Swift.Umbraco.Business.Service.Interfaces;
using $safeprojectname$.Extensions.Moment;
using $safeprojectname$.Models;
using System;
using System.Dynamic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace $safeprojectname$.Controllers.Backoffice
{
    [PluginController("Tools")]
    public class InstantWinController : UmbracoAuthorizedApiController
    {
        private IInstantWinService _instantWinService;

        public InstantWinController(IInstantWinService instantWinService)
        {
            _instantWinService = instantWinService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetPrizes()
        {
            dynamic data = new ExpandoObject();
            try
            {
                var prizes = await _instantWinService.GetPrizes();
                data.Description = "Prize list returned successfully";
                data.Prizes = prizes;

                var response = new ApiResponse
                {
                    Success = true,
                    Message = "SUCCESS",
                    Data = data
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                data.Description = e.Message;
                var apiResponse = new ApiResponse
                {
                    Success = false,
                    Message = "FAILED",
                    Data = data
                };
                return Content(HttpStatusCode.InternalServerError, apiResponse);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllocables()
        {
            dynamic data = new ExpandoObject();
            try
            {
                var prizes = await _instantWinService.GetAllocables();
                data.Description = "Allocable list returned successfully";
                data.Allocables = prizes;

                var response = new ApiResponse
                {
                    Success = true,
                    Message = "SUCCESS",
                    Data = data
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                data.Description = e.Message;
                var apiResponse = new ApiResponse
                {
                    Success = false,
                    Message = "FAILED",
                    Data = data
                };
                return Content(HttpStatusCode.InternalServerError, apiResponse);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetMoments()
        {
            dynamic data = new ExpandoObject();
            try
            {
                var moments = await _instantWinService.GetMoments();
                data.Description = "Prize list returned successfully";
                data.Moments = moments;

                var response = new ApiResponse
                {
                    Success = true,
                    Message = "SUCCESS",
                    Data = data
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                data.Description = e.Message;
                var apiResponse = new ApiResponse
                {
                    Success = false,
                    Message = "FAILED",
                    Data = data
                };
                return Content(HttpStatusCode.InternalServerError, apiResponse);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetLimitOptions()
        {
            dynamic data = new ExpandoObject();
            try
            {
                var options = await _instantWinService.GetLimitOptions();
                data.Description = "Prize list returned successfully";
                data.LimitOptions = options;

                var response = new ApiResponse
                {
                    Success = true,
                    Message = "SUCCESS",
                    Data = data
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                data.Description = e.Message;
                var apiResponse = new ApiResponse
                {
                    Success = false,
                    Message = "FAILED",
                    Data = data
                };
                return Content(HttpStatusCode.InternalServerError, apiResponse);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Generate(GenerationModel model)
        {
            dynamic data = new ExpandoObject();

            try
            {
                var config = new GeneratorConfig
                {
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    OpenTime = model.OpenTime.ToTodayDateTime(),
                    CloseTime = model.CloseTime.ToTodayDateTime(),
                    LimitNumber = model.LimitNumber,
                    LimitOption = model.LimitOption
                };
                var generationResponse = await _instantWinService.GenerateInstantWinMoments(config, model.Allocable);
                data.GeneratedNumber = generationResponse.generatedNumber;

                var response = new ApiResponse
                {
                    Success = generationResponse.status,
                    Message = "SUCCESS",
                    Data = data
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                data.Description = e.Message;
                var apiResponse = new ApiResponse
                {
                    Success = false,
                    Message = "FAILED",
                    Data = data
                };
                return Content(HttpStatusCode.InternalServerError, apiResponse);
            }
        }
    }
}