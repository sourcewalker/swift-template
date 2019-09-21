using System;

namespace Swift.Umbraco.$safeprojectname$.DTO
{
    public class CountryDto
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset UpdatedOn { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Culture { get; set; }
    }
}
