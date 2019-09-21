using System;

namespace $safeprojectname$.Models
{
    public class DataLayerModel
    {
        public string gameStatusHead { get; set; }
        public string nameOfPrizeWonHead { get; set; }
        public string paymentMethodHead { get; set; }
        public string countrySelectedHead { get; set; }
        public Guid participationID { get; set; }

        public Guid participantID { get; set; }

        public string consumerID { get; set; }
    }
}