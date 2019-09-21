using Swift.Umbraco.$safeprojectname$.Enum;
using System;

namespace Swift.Umbraco.$safeprojectname$.DTO
{
    public class ConsumerDto
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset UpdatedOn { get; set; }

        public Guid CountryId { get; set; }

        public CountryDto Country { get; set; }

        public Countries CountryEnum { get; set; }

        public string EmailHash { get; set; }

        public string ConsumerId { get; set; }
    }
}
