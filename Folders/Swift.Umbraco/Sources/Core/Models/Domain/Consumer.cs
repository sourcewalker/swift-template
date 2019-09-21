using System;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Swift.Umbraco.$safeprojectname$.Domain
{

    [TableName("Consumer")]
    [PrimaryKey("Id", autoIncrement = false)]
    public class Consumer : EntityBase
    {
        [ForeignKey(typeof(Country), Name = "FK_Consumer_Country")]
        public Guid CountryId { get; set; }

        [ResultColumn]
        public Country Country { get; set; }

        [Index(IndexTypes.NonClustered, Name = "IX_Consumer_EmailHash")]
        public string EmailHash { get; set; }

        public string ConsumerId { get; set; }
    }
}
