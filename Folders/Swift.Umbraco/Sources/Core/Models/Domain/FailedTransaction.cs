using System;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Swift.Umbraco.$safeprojectname$.Domain
{
    [TableName("FailedTransaction")]
    [PrimaryKey("Id", autoIncrement = false)]
    public class FailedTransaction : EntityBase
    {
        [ForeignKey(typeof(Participation), Name = "FK_FailedTransaction_Participation")]
        public Guid ParticipationId { get; set; }

        [ResultColumn]
        public Participation Participation { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string MobilePrivate { get; set; }

        public string Street1 { get; set; }

        [NullSetting(NullSetting = NullSettings.Null)]
        public string Street2 { get; set; }

        public string City { get; set; }

        public string Zipcode { get; set; }
    }
}
