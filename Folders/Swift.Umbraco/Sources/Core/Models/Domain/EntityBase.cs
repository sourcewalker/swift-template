using System;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Swift.Umbraco.$safeprojectname$.Domain
{
    public class EntityBase
    {
        [PrimaryKeyColumn(AutoIncrement = false)]
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        [NullSetting(NullSetting = NullSettings.Null)]
        public DateTime UpdatedOn { get; set; }
    }
}
