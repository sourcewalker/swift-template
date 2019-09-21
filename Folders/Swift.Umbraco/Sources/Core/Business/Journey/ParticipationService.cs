using Swift.Umbraco.$safeprojectname$.Manager.Interfaces;
using Swift.Umbraco.$safeprojectname$.Service.Interfaces;
using Swift.Umbraco.Models.DTO;
using Swift.Umbraco.Models.Enum;
using System;
using System.Threading.Tasks;

namespace Swift.Umbraco.$safeprojectname$.Journey
{
    public class ParticipationService : IParticipationService
    {
        private readonly IInstantWinService _instantWinService;
        private readonly IPrizeManager _prizeManager;
        private readonly IParticipationManager _participationManager;
        private readonly IParticipantManager _participantManager;
        private readonly ISynchronizationService _crmService;
        private readonly ICountryManager _countryManager;

        public ParticipationService(
            IInstantWinService instantWinService,
            IPrizeManager prizeManager,
            IParticipantManager participantManager,
            IParticipationManager participationManager,
            ICountryManager countryManager,
            ISynchronizationService crmService)
        {
            _instantWinService = instantWinService;
            _participationManager = participationManager;
            _prizeManager = prizeManager;
            _participantManager = participantManager;
            _crmService = crmService;
            _countryManager = countryManager;
        }

        public async Task<(bool creationStatus, Guid participationId, Guid participantId)> GetOrCreateEmailValidatedParticipationAsync(string email)
        {
            var participation = _participationManager.FindByEmail(email);
            if (participation != null &&
                (participation.JourneyStatus == JourneyStatus.EMAIL_VALIDATED.ToString() ||
                participation.JourneyStatus == JourneyStatus.LOGO_VALIDATED.ToString()))
            {
                return (false, participation.Id, participation.ParticipantId);
            }
            var created = await Task.Run(() => _participationManager.CreateParticipation(email));
            return (true, created.participationId, created.participantId);
        }

        public async Task<bool> UpdateLogoValidatedParticipationAsync(ParticipationDto participation)
        {
            var participationDto = _participationManager.FindById(participation.Id);
            participationDto.JourneyStatus = JourneyStatus.LOGO_VALIDATED.ToString();
            return await Task.Run(() => _participationManager.UpdateParticipation(participationDto));
        }

        public async Task<(bool winStatus, PrizeDto prize)> UpdateInstantWinStatusAsync(
            ParticipationDto participationDto)
        {
            var instantStatus = _instantWinService.WinCheck();
            var status = instantStatus.isWinner;

            var participantDto = _participantManager.FindById(participationDto.ParticipantId);

            if (status)
            {
                participationDto.JourneyStatus = JourneyStatus.WON_CHECKED.ToString();
                participationDto.PrizeId = instantStatus.prize.Id;
                participationDto.InstanWinMomentId = instantStatus.instantWin.Id;
                participantDto.LastParticipatedDate = DateTime.UtcNow;
                participantDto.LastWonDate = DateTime.UtcNow;
            }
            else
            {
                participationDto.JourneyStatus = JourneyStatus.LOST_CHECKED.ToString();
                participantDto.LastParticipatedDate = DateTime.UtcNow;
            }

            await Task.Run(() =>
            {
                _participantManager.UpdateParticipant(participantDto);
                _participationManager.UpdateParticipation(participationDto);
            });

            return (instantStatus.isWinner, instantStatus.prize);
        }

        public string GetWonPrize(Guid participationId)
        {
            var participationDto = _participationManager.FindById(participationId);
            var prize = _prizeManager.FindById(participationDto.PrizeId.Value);

            return prize.Value.ToString();
        }

        public async Task<(bool success, string consumerId)> UpdateUserInformationAsync(UserInfoDto userInfo)
        {
            var consumerId = string.Empty;
            var participationDto = _participationManager.FindById(userInfo.ParticipationId);

            if (participationDto.JourneyStatus != JourneyStatus.WON_CHECKED.ToString())
            {
                return (false, consumerId);
            }

            participationDto.CountryId = _countryManager.GetCountryByCode(userInfo.Country)?.Id;
            participationDto.JourneyStatus = userInfo.PaymentType == PaymentType.BACS_TRANSFER ?
                                    JourneyStatus.BACS_TRANSFERT_NOT_SYNC.ToString() :
                                    JourneyStatus.CHEQUE_PAYMENT_NOT_SYNC.ToString();
            await Task.Run(() => _participationManager.UpdateParticipation(participationDto));

            var prize = _prizeManager.FindById(participationDto.PrizeId.Value);

            if (prize == null)
            {
                return (false, consumerId);
            }

            var data = new CrmDto();
            data.Firstname = userInfo.Firstname;
            data.Lastname = userInfo.Lastname;
            data.Email = userInfo.Email;
            data.PhoneNumber = userInfo.MobilePrivate;
            data.Address = userInfo.Street1;
            data.AdditionalAddress = userInfo.Street2;
            data.City = userInfo.City;
            data.ZipCode = userInfo.Zipcode;
            data.Country = userInfo.Country;
            data.AccountNumber = userInfo.AccountNumber;
            data.SortCode = userInfo.SortCode;
            data.PaymentType = userInfo.PaymentType;
            data.PrizeValue = prize.Value;
            data.ParticipationId = participationDto.Id;
            data.IBAN = userInfo.IBAN;
            data.BIC = userInfo.BIC;

            var response = await _crmService.SendDataToSynchronizeAsync(data);

            if (response.Success)
            {
                consumerId = response.ConsumerId;
                // Update Successful sync Status
                participationDto.JourneyStatus = userInfo.PaymentType == PaymentType.BACS_TRANSFER ?
                                        JourneyStatus.BACS_PARTICIPATION_SYNCED.ToString() :
                                        JourneyStatus.CHEQUE_PARTICIPATION_SYNCED.ToString();
                await Task.Run(() => _participationManager.UpdateParticipation(participationDto));

                // Save ConsumerId
                var participantDto = _participantManager.FindById(participationDto.ParticipantId);
                participantDto.ConsumerId = consumerId;
                await Task.Run(() => _participantManager.UpdateParticipant(participantDto));
            }

            return (response.Success, consumerId);
        }

        public bool CheckStatus(Guid participationId, JourneyStatus status)
        {
            var participationDto = _participationManager.FindById(participationId);
            return participationDto.JourneyStatus == status.ToString();
        }

        public string GetEmail(Guid participationId)
        {
            var participationDto = _participationManager.FindById(participationId);
            return _participantManager.FindById(participationDto.ParticipantId)?.Email;
        }
    }
}
