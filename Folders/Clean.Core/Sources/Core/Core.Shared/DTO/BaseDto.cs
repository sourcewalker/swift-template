using System;

namespace $safeprojectname$.DTO
{
    public class BaseDto
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
