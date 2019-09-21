﻿using System.ComponentModel.DataAnnotations;
using $safeprojectname$.Constants;
using $safeprojectname$.Validation;

namespace $safeprojectname$.Models
{
    public class ParticipationViewModel
    {
        [Required(ErrorMessage = ErrorMessages.Participation.EmailRequired)]
        [EmailAddress(ErrorMessage = ErrorMessages.Participation.EmailInvalid)]
        [NotUsedEmail(ErrorMessage = ErrorMessages.Participation.AlreadyUsedEmail)]
        public string Email { get; set; }

        public string Culture { get; set; }

        public bool RetailerConsent { get; set; }

        public bool NewsletterOptin { get; set; }

        [Captcha(ErrorMessage = ErrorMessages.Participation.CaptchaInvalid)]
        public string Captcha { get; set; }
    }
}