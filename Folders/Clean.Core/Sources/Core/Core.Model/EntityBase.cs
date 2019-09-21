using System;

namespace $safeprojectname$
{
    public abstract class EntityBase<TId> where TId : struct
    {
        public TId Id { get; set; }

        public byte[] TimeStamp { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
