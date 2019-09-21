using System;
using System.ComponentModel.DataAnnotations;
using $safeprojectname$.Infrastructure.Validation;

namespace $safeprojectname$.Models
{
    public class RewardRequest
    {

        [Required(ErrorMessage = "FIRSTNAME_REQUIRED")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "LASTNAME_REQUIRED")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "EMAIL_REQUIRED")]
        [EmailAddress(ErrorMessage = "EMAIL_INVALID")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PHONE_NUMBER_REQUIRED")]
        public string MobilePrivate { get; set; }

        [Required(ErrorMessage = "STREET1_REQUIRED")]
        public string Street1 { get; set; }

        public string Street2 { get; set; }

        [Required(ErrorMessage = "CITY_REQUIRED")]
        public string City { get; set; }

        [Required(ErrorMessage = "ZIPCODE_REQUIRED")]
        public string Zipcode { get; set; }

        [Required(ErrorMessage = "COUNTRY_REQUIRED")]
        //[RequiredEnum(ErrorMessage = "COUNTRY_INVALID")]
        public string Country { get; set; }

        [Required(ErrorMessage = "PAYEMENT_TYPE_REQUIRED")]
        //[RequiredEnum(ErrorMessage = "PAYEMENT_TYPE_INVALID")]
        public string PaymentType { get; set; }

        [RequiredIf("PaymentType", "BACS", ErrorMessage = "ACCOUNT_NUMBER_REQUIRED")]
        public string AccountNumber { get; set; }

        [RequiredIf("PaymentType", "BACS", ErrorMessage = "SORT_CODE_REQUIRED")]
        public string SortCode { get; set; }

        [RequiredIf("PaymentType", "BACS", ErrorMessage = "IBAN_CODE_REQUIRED")]
        [RequiredIf("Country", "IE", ErrorMessage = "IBAN_CODE_REQUIRED")]
        public string IBAN { get; set; }

        [RequiredIf("PaymentType", "BACS", ErrorMessage = "BIC_CODE_REQUIRED")]
        [RequiredIf("Country", "IE", ErrorMessage = "BIC_CODE_REQUIRED")]
        public string BIC { get; set; }

        [Required(ErrorMessage = "TERMS_CONSENT_REQUIRED")]
        [True(ErrorMessage = "TERMS_CONSENT_SHOULD_BE_TRUE")]
        public bool TermsConsent { get; set; }

        [Required(ErrorMessage = "AGE_CONSENT_REQUIRED")]
        [True(ErrorMessage = "AGE_CONSENT_SHOULD_BE_TRUE")]
        public bool Age { get; set; }

        [Captcha(ErrorMessage = "CAPTCHA_INVALID")]
        public string Captcha { get; set; }

        [Required(ErrorMessage = "PARTICIPATIONID_REQUIRED")]
        public Guid ParticipationId { get; set; }
    }
}