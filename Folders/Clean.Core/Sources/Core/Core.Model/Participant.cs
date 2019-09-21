﻿using System;

namespace $safeprojectname$
{
    public class Participant : EntityBase<Guid>
    {
        public string EmailHash { get; set; }

        public Guid? ParticipationId { get; set; }

        public Participation Participation { get; set; }

        public string ConsumerId { get; set; }
    }
}
