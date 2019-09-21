using Swift.Umbraco.$safeprojectname$.Domain;
using System;

namespace Swift.Umbraco.$safeprojectname$.DTO
{
    public class ParticipationDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public Guid ParticipantId { get; set; }

        public ParticipantDto Participant { get; set; }

        public Guid? PrizeId { get; set; }

        public Prize Prize { get; set; }

        public Guid? InstanWinMomentId { get; set; }

        public InstantWinMomentDto InstantWinMoment { get; set; }

        public Guid? CountryId { get; set; }

        public CountryDto Country { get; set; }

        public string EmailHash { get; set; }

        public string PrivacyVersion { get; set; }

        public string JourneyStatus { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset UpdatedOn { get; set; }
    }
}
