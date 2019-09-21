using System;

namespace $safeprojectname$
{
    public class Site : EntityBase<Guid>
    {
        public string Culture { get; set; }

        public string Name { get; set; }

        public string Domain { get; set; }
    }
}
