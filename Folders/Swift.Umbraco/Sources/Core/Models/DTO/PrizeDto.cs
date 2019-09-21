using System;

namespace Swift.Umbraco.$safeprojectname$.DTO
{
    public class PrizeDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Value { get; set; }

        public int TotalNumber { get; set; }

        public int Remaining { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset UpdatedOn { get; set; }
    }
}
