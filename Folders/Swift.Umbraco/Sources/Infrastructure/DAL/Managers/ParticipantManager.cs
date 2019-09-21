using Swift.Umbraco.Business.Manager.Interfaces;
using Swift.Umbraco.Infrastructure.$safeprojectname$.Petapoco;
using Swift.Umbraco.Models.Domain;
using Swift.Umbraco.Models.DTO;
using Swift.Umbraco.Models.Mapping.Helper;
using Swift.Umbraco.Models.Utility;
using System;
using System.Linq;

namespace Swift.Umbraco.Infrastructure.$safeprojectname$.Entities
{
    public class ParticipantManager : GenericManager<Participant>, IParticipantManager
    {
        private readonly IConsumerManager _consumerManager;

        public ParticipantManager(IConsumerManager consumerManager)
        {
            _consumerManager = consumerManager;
        }

        public ParticipantDto GetByEmail(string email)
        {
            //var query = new Sql()
            //                .Select("*")
            //                .From<Participant>(_sqlProvider)
            //                .Where<Participant>(cons => cons.Email == email, _sqlProvider);
            var sqlQuery = "SELECT * FROM Participant WHERE Email = @0";
            return _database.Fetch<Participant>(sqlQuery, email).FirstOrDefault().toDto();
        }

        public ParticipantDto FindById(Guid id)
        {
            return GetById(id).toDto();
        }

        public Guid GetOrCreateParticipant(ParticipantDto participant)
        {
            var emailHash = StringHelper.Md5HashEncode(participant.Email.ToLower());


            if (_consumerManager.IsAlreadyExistingConsumerId(participant.Country, emailHash, out var consumerCrmId))
            {
                //var query = new Sql()
                //            .Select("*")
                //            .From<Participant>(_sqlProvider)
                //            .Where<Participant>(cons => cons.ConsumerCrmId == consumerCrmId, _sqlProvider);
                var sqlQuery = "SELECT * FROM Participant WHERE ConsumerCrmId = @0";
                return _database.Fetch<Participant>(sqlQuery, consumerCrmId).FirstOrDefault().Id;
            }

            if (!string.IsNullOrEmpty(participant.ConsumerId))
            {
                var crmConsumerId = _consumerManager.GetOrCreateConsumerId(participant.ConsumerId, participant.Country, emailHash);
                participant.ConsumerCrmId = crmConsumerId;
            }

            return Create(participant.toEntity());
        }

        public bool UpdateParticipant(ParticipantDto participant)
        {
            var participantEntity = GetById(participant.Id);
            var emailHash = StringHelper.Md5HashEncode(participant.Email.ToLower());

            if (!string.IsNullOrEmpty(participant.ConsumerId))
            {
                var crmConsumerId = _consumerManager.GetOrCreateConsumerId(participant.ConsumerId, participant.Country, emailHash);
                participantEntity.ConsumerCrmId = crmConsumerId;
            }

            participantEntity.LastParticipatedDate = participant.LastParticipatedDate.ToUtc();
            participantEntity.LastWonDate = participant.LastWonDate.ToUtc();
            participantEntity.Email = participant.Email;

            return Update(participantEntity);
        }
    }
}
