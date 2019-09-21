﻿using System;

namespace $safeprojectname$.DTO
{
    public class ParticipationDto : BaseDto
    {
        public Guid? SiteId { get; set; }

        public SiteDto Site { get; set; }

        public string EmailHash { get; set; }

        public string ConsumerId { get; set; }

        public string Status { get; set; }

        public string ApiStatus { get; set; }

        public string ApiMessage { get; set; }

        public string Email { get; set; }

        public bool NewsletterOptin { get; set; }

        public bool RetailerConsent { get; set; }
    }
}
