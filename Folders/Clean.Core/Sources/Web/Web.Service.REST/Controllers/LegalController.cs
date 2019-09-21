﻿//using Core.Infrastructure.Interfaces.Configuration;
using Core.Infrastructure.Interfaces.Logging;
using Core.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Dynamic;
using System.Threading.Tasks;
using $safeprojectname$.Models;

namespace $safeprojectname$.Controllers
{
    [ApiController]
    [RequireHttps]
    [Route("[controller]")]
    public class LegalController : ControllerBase
    {
        private readonly ILegalService _legalService;
        private readonly ILoggingProvider _logger;

        public LegalController(
            ILegalService legalService,
            ILoggingProvider logger)
        {
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
        /// <response code="400">BadRequest</response>
        [HttpGet("privacy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetTerms()
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

                return BadRequest(apiResponse);
            }
        }
    }
}
