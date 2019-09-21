﻿using System;

namespace $safeprojectname$
{
    public class FailedTransaction : EntityBase<Guid>
    {
        public Guid? ParticipationId { get; set; }

        public Participation Participation { get; set; }

        public bool TermsConsent { get; set; }

        public bool NewsletterOptin { get; set; }
    }
}
