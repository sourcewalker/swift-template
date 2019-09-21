using Swift.Umbraco.Business.Manager.Interfaces;
using Swift.Umbraco.Infrastructure.$safeprojectname$.Petapoco;
using Swift.Umbraco.Models.Domain;
using Swift.Umbraco.Models.DTO;
using Swift.Umbraco.Models.Enum;
using Swift.Umbraco.Models.Mapping.Helper;
using Swift.Umbraco.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Swift.Umbraco.Infrastructure.$safeprojectname$.Entities
{
    public class ParticipationManager : GenericManager<Participation>, IParticipationManager
    {
        private readonly IParticipantManager _participantManager;

        public ParticipationManager(IParticipantManager participantManager)
        {
            _participantManager = participantManager;
        }

        public ParticipationDto FindById(Guid id)
        {
            return GetById(id).toDto();
        }

        public ParticipationDto FindByEmail(string email)
        {
            var sqlQuery = "SELECT * FROM Participation WHERE EmailHash = @0";
            return _database.Fetch<Participation>(sqlQuery, StringHelper.Md5HashEncode(email.ToLower())).FirstOrDefault().toDto();
        }

        public IEnumerable<ParticipationDto> FindAllByEmail(string email)
        {
            var sqlQuery = "SELECT * FROM Participation WHERE EmailHash = @0";
            return _database.Fetch<Participation>(sqlQuery, StringHelper.Md5HashEncode(email.ToLower())).toDtos();
        }

        public (Guid participationId, Guid participantId) CreateParticipation(string email)
        {
            var participant = new ParticipantDto
            {
                Email = email,
                ConsumerCrmId = null
            };
            var participantId = _participantManager.GetOrCreateParticipant(participant);

            var participation = new ParticipationDto
            {
                Email = email,
                EmailHash = StringHelper.Md5HashEncode(email.ToLower()),
                ParticipantId = participantId,
                JourneyStatus = JourneyStatus.EMAIL_VALIDATED.ToString(),
                PrizeId = null,
                InstanWinMomentId = null,
                CountryId = null
            };

            var participationId = Create(participation.toEntity());

            return (participationId, participantId);
        }

        public bool UpdateParticipation(ParticipationDto participation)
        {
            var participationEntity = GetById(participation.Id);

            if (participation.EmailHash != default)
            {
                participationEntity.EmailHash = participation.EmailHash;
            }
            if (participation.PrizeId != default)
            {
                participationEntity.PrizeId = participation.PrizeId;
            }
            if (participation.EmailHash != default)
            {
                participationEntity.EmailHash = participation.EmailHash;
            }
            if (participation.InstanWinMomentId != default)
            {
                participationEntity.InstanWinMomentId = participation.InstanWinMomentId;
            }
            if (participation.CountryId != default)
            {
                participationEntity.CountryId = participation.CountryId;
            }
            if (participation.PrivacyVersion != default)
            {
                participationEntity.PrivacyVersion = participation.PrivacyVersion;
            }
            if (participation.JourneyStatus != default)
            {
                participationEntity.JourneyStatus = participation.JourneyStatus;
            }

            return Update(participationEntity);
        }
    }
}
