using System;

namespace Swift.Umbraco.$safeprojectname$.DTO
{
    public class InstantWinMomentDto
    {
        public Guid Id { get; set; }

        public Guid PrizeId { get; set; }

        public PrizeDto Prize { get; set; }

        public DateTimeOffset ActivationDate { get; set; }

        public bool IsWon { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset UpdatedOn { get; set; }
    }
}
