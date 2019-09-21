using Swift.Umbraco.$safeprojectname$.Enum;
using System;

namespace Swift.Umbraco.$safeprojectname$.DTO
{
    public class UserInfoDto
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string MobilePrivate { get; set; }

        public string Street1 { get; set; }

        public string Street2 { get; set; }

        public string City { get; set; }

        public string Zipcode { get; set; }

        public Countries Country { get; set; }

        public PaymentType PaymentType { get; set; }

        public string AccountNumber { get; set; }

        public string SortCode { get; set; }

        public string IBAN { get; set; }

        public string BIC { get; set; }

        public bool TermsConsent { get; set; }

        public Guid ParticipationId { get; set; }
    }
}
