using System;

namespace Swift.Umbraco.$safeprojectname$.DTO
{
    public class FailedTransactionDto
    {
        public Guid Id { get; set; }

        public Guid ParticipationId { get; set; }

        public ParticipationDto Participation { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string MobilePrivate { get; set; }

        public string Street1 { get; set; }

        public string Street2 { get; set; }

        public string City { get; set; }

        public string Zipcode { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset UpdatedOn { get; set; }
    }
}
