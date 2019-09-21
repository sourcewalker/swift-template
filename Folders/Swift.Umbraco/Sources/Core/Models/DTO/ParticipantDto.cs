using Swift.Umbraco.$safeprojectname$.Enum;
using System;

namespace Swift.Umbraco.$safeprojectname$.DTO
{
    public class ParticipantDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public Countries Country { get; set; }

        public Guid? ConsumerCrmId { get; set; }

        public string ConsumerId { get; set; }

        public ConsumerDto Consumer { get; set; }

        public DateTimeOffset LastWonDate { get; set; }

        public DateTimeOffset LastParticipatedDate { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset UpdatedOn { get; set; }
    }
}
