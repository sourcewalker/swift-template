using Swift.Umbraco.$safeprojectname$.Enum;
using System;

namespace Swift.Umbraco.$safeprojectname$.DTO
{
    public class CrmDto
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string AdditionalAddress { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public Countries Country { get; set; }

        public string AccountNumber { get; set; }

        public string SortCode { get; set; }

        public PaymentType PaymentType { get; set; }

        public int PrizeValue { get; set; }

        public string IBAN { get; set; }

        public string BIC { get; set; }

        public Guid ParticipationId { get; set; }
    }
}
