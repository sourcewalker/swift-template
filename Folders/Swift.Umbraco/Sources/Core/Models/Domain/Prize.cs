using Umbraco.Core.Persistence;

namespace Swift.Umbraco.$safeprojectname$.Domain
{
    [TableName("Prize")]
    [PrimaryKey("Id", autoIncrement = false)]
    public class Prize : EntityBase
    {
        public string Name { get; set; }

        public int Value { get; set; }

        public int TotalNumber { get; set; }

        public int Remaining { get; set; }
    }
}
