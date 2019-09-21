using System;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Swift.Umbraco.$safeprojectname$.Domain
{
    [TableName("Participation")]
    [PrimaryKey("Id", autoIncrement = false)]
    public class Participation : EntityBase
    {
        [ForeignKey(typeof(Participant), Name = "FK_Participation_Participant")]
        public Guid ParticipantId { get; set; }

        [ResultColumn]
        public Participant Participant { get; set; }

        [NullSetting(NullSetting = NullSettings.Null)]
        [ForeignKey(typeof(Prize), Name = "FK_Participation_Prize")]
        public Guid? PrizeId { get; set; }

        [ResultColumn]
        public Prize Prize { get; set; }

        [NullSetting(NullSetting = NullSettings.Null)]
        [ForeignKey(typeof(InstantWinMoment), Name = "FK_Participation_InstantWinMoment")]
        public Guid? InstanWinMomentId { get; set; }

        [ResultColumn]
        public InstantWinMoment InstantWinMoment { get; set; }

        [NullSetting(NullSetting = NullSettings.Null)]
        [ForeignKey(typeof(Country), Name = "FK_Participation_Country")]
        public Guid? CountryId { get; set; }

        [ResultColumn]
        public Country Country { get; set; }

        [IndexAttribute(IndexTypes.NonClustered, Name = "IX_Participation_EmailHash")]
        public string EmailHash { get; set; }

        [NullSetting(NullSetting = NullSettings.Null)]
        public string PrivacyVersion { get; set; }

        [NullSetting(NullSetting = NullSettings.Null)]
        public string JourneyStatus { get; set; }
    }
}
