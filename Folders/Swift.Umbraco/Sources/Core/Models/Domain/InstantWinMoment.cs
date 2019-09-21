using System;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Swift.Umbraco.$safeprojectname$.Domain
{
    [TableName("InstantWinMoment")]
    [PrimaryKey("Id", autoIncrement = false)]
    public class InstantWinMoment : EntityBase
    {
        [ForeignKey(typeof(Prize), Name = "FK_InstantWinMoment_Prize")]
        public Guid PrizeId { get; set; }

        [ResultColumn]
        public Prize Prize { get; set; }

        public DateTime ActivationDate { get; set; }

        public bool IsWon { get; set; }
    }
}
