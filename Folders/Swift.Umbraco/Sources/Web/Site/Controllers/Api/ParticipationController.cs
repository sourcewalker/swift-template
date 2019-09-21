using Swift.Umbraco.Business.Service.Interfaces;
using Swift.Umbraco.Models.DTO;
using Swift.Umbraco.Models.Enum;
using $safeprojectname$.Extensions.Storage;
using $safeprojectname$.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Umbraco.Web.WebApi;

namespace $safeprojectname$.Controllers.Api
{
    public class ParticipationController : UmbracoApiController
    {
        private readonly IParticipationService _participationService;
        private readonly IValidationService _validationService;

        public ParticipationController(
            IParticipationService participationService,
            IValidationService validationService)
        {
            _participationService = participationService;
            _validationService = validationService;
        }

        [HttpPost]
        public async Task<IHttpActionResult> EmailCheck([FromBody] EmailRequest model)
        {
            dynamic expando = new ExpandoObject();

            var apiResponse = new ApiResponse
            {
                Success = false,
                Message = "Bad Request"
            };

            try
            {
                if (!ModelState.IsValid)
                {
                    expando.Description = "EMAIL_REQUIRED_OR_INVALID";
                    apiResponse.Data = expando;
                    return Content(HttpStatusCode.BadRequest, apiResponse);
                }

                var hasNotCompleted = await _validationService.HasNotCompletedPreviousWonFlow(model.Email);
                if (hasNotCompleted.Status)
                {
                    expando.Description = "USER_HAS_NOT_COMPLETED_PREVIOUS_FLOW";
                    expando.ParticipationId = hasNotCompleted.ParticipationId;
                    expando.ParticipantId = hasNotCompleted.ParticipantId;
                    apiResponse.Data = expando;
                    apiResponse.Success = true;
                    apiResponse.Message = "User should continue previous flow";
                    return Ok(apiResponse);
                }

                var isOpen = await _validationService.IsOpenHoursForParticipationAsync();
                if (!isOpen)
                {
                    expando.Description = "PARTICIPATION_CLOSED";
                    apiResponse.Data = expando;
                    return Content(HttpStatusCode.BadRequest, apiResponse);
                }

                var canParticipate = await _validationService.CanUserParticipateAsync(model.Email);
                if (!canParticipate.status)
                {
                    expando.Description = canParticipate.errorMessage;
                    apiResponse.Data = expando;
                    return Content(HttpStatusCode.BadRequest, apiResponse);
                }

                var participateResult = await _participationService.GetOrCreateEmailValidatedParticipationAsync(model.Email);

                expando.Description = participateResult.creationStatus ?
                                        "PARTICIPATION_CREATED_SUCCESSFULLY" :
                                        "PARTICIPATION_ALREADY_CREATED";
                expando.ParticipationId = participateResult.participationId;
                expando.ParticipantId = participateResult.participantId;
                apiResponse.Success = true;
                apiResponse.Message = "Email validation successfull";
                apiResponse.Data = expando;

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                apiResponse.Success = false;
                apiResponse.Message = e.Message;
                expando.Description = $"Error occured in {e.Source}: {e.StackTrace}";
                apiResponse.Data = expando;

                return Content(HttpStatusCode.InternalServerError, apiResponse);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> LogoAndWinCheck()
        {
            dynamic expando = new ExpandoObject();

            var apiResponse = new ApiResponse
            {
                Success = false
            };

            var model = new LogoWinRequest
            {
                PhotoInput = HttpContext.Current.Request.Files[0],
                ParticipantId = Guid.Parse(HttpContext.Current.Request.Form["ParticipantId"]),
                ParticipationId = Guid.Parse(HttpContext.Current.Request.Form["ParticipationId"]),
            };

            try
            {
                if (model.PhotoInput == null)
                {
                    expando.Description = "PICTURE_REQUIRED";
                    apiResponse.Message = "Please provide a picture";
                    apiResponse.Data = expando;
                    return Content(HttpStatusCode.BadRequest, apiResponse);
                }
                if (model.ParticipationId == null || model.ParticipationId == default)
                {
                    expando.Description = "PARTICIPATION_ID_REQUIRED";
                    apiResponse.Message = "Please provide a participation Id";
                    apiResponse.Data = expando;
                    return Content(HttpStatusCode.BadRequest, apiResponse);
                }
                if (model.ParticipantId == null || model.ParticipantId == default)
                {
                    expando.Description = "PARTICIPANT_ID_REQUIRED";
                    apiResponse.Message = "Please provide a participant Id";
                    apiResponse.Data = expando;
                    return Content(HttpStatusCode.BadRequest, apiResponse);
                }

                var filePath = FileHelper.StoreFileTemporarily(model.PhotoInput);
                bool isLogoValid = await _validationService.CheckValidLogoAsync(filePath);
                FileHelper.RemoveTemporarilyStoredFile(filePath);

                if (!isLogoValid)
                {
                    expando.Description = "LOGO_INVALID";
                    apiResponse.Message = "Could not validate uploaded pictures";
                    apiResponse.Data = expando;
                    return Content(HttpStatusCode.BadRequest, apiResponse);
                }

                var participation = new ParticipationDto
                {
                    Id = model.ParticipationId,
                    ParticipantId = model.ParticipantId
                };

                await _participationService.UpdateLogoValidatedParticipationAsync(participation);

                var instantWinResult = await _participationService.UpdateInstantWinStatusAsync(participation);

                expando.Description = instantWinResult.winStatus ?
                                        "LOGO_VALID_AND_WON" :
                                        "LOGO_VALID_BUT_LOST";
                expando.GameStatus = instantWinResult.winStatus ? "WIN" : "LOSE";
                expando.PrizeName = instantWinResult.winStatus ? instantWinResult.prize.Name : string.Empty;
                apiResponse.Success = true;
                apiResponse.Message = "Logo valid and instant win checked";
                apiResponse.Data = expando;

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                apiResponse.Success = false;
                apiResponse.Message = e.Message;
                expando.Description = $"Error occured in {e.Source}: {e.StackTrace}";
                apiResponse.Data = expando;

                return Content(HttpStatusCode.InternalServerError, apiResponse);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> RewardDelivery([FromBody]RewardRequest model)
        {
            dynamic expando = new ExpandoObject();

            var apiResponse = new ApiResponse
            {
                Success = false,
                Message = "Bad Request"
            };

            try
            {
                // Code for testing purpose 
                if (model.Email.ToUpperInvariant() == "TEST.PROXIMITY@PROXIMITYBBDO.FR")
                {
                    expando.SyncStatus = true;
                    expando.ConsumerId = "aaaabbbbccccdddd";
                    expando.CountrySelected = "UK";
                    expando.PaymentMethod = "BACS";
                    apiResponse.Success = true;
                    apiResponse.Message = "Information synchronization successfull";
                    apiResponse.Data = expando;

                    return Ok(apiResponse);
                }
                // End - Testing

                if (model == null)
                {
                    expando.Description = new List<string>() { "Body request data should not be null" };

                    apiResponse.Message = "Missing or unknown request body";
                    apiResponse.Data = expando;

                    return Content(HttpStatusCode.BadRequest, apiResponse);
                }

                if (model.ParticipationId == default)
                {
                    expando.Description = new List<string>() { "ParticipationId should not be null" };

                    apiResponse.Message = "Missing or unknown request body";
                    apiResponse.Data = expando;

                    return Content(HttpStatusCode.BadRequest, apiResponse);
                }

                if (!ModelState.IsValid)
                {
                    var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    var errorList = allErrors.Select(error => error.ErrorMessage);
                    expando.Description = errorList;

                    apiResponse.Message = "Validation error occured";
                    apiResponse.Data = expando;

                    return Content(HttpStatusCode.BadRequest, apiResponse);
                }

                var userInfo = new UserInfoDto
                {
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    Email = model.Email,
                    MobilePrivate = model.MobilePrivate,
                    Street1 = model.Street1,
                    Street2 = model.Street2,
                    City = model.City,
                    Zipcode = model.Zipcode,
                    Country = model.Country.ToUpper() == "UK" ?
                                    Countries.UK : Countries.IE,
                    PaymentType = model.PaymentType.ToUpper() == "BACS" ?
                                    PaymentType.BACS_TRANSFER : PaymentType.CHEQUE,
                    AccountNumber = model.AccountNumber,
                    SortCode = model.SortCode,
                    IBAN = model.IBAN,
                    BIC = model.BIC,
                    TermsConsent = model.TermsConsent,
                    ParticipationId = model.ParticipationId
                };
                var syncStatus = await _participationService.UpdateUserInformationAsync(userInfo);

                expando.SyncStatus = syncStatus.success;
                expando.ConsumerId = syncStatus.consumerId;
                expando.CountrySelected = model.Country.ToUpper();
                expando.PaymentMethod = model.PaymentType.ToUpper();
                apiResponse.Success = true;
                apiResponse.Message = "Information synchronization successfull";
                apiResponse.Data = expando;

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                apiResponse.Success = false;
                apiResponse.Message = e.Message;
                expando.Description = $"Error occured in {e.Source}: {e.StackTrace}";
                apiResponse.Data = expando;

                return Content(HttpStatusCode.InternalServerError, apiResponse);
            }
        }
    }
}