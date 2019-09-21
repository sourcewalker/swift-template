using Newtonsoft.Json;

namespace $safeprojectname$.Ecommerce.Entities
{
    public class Payment
    {
        [JsonProperty("Type")]
        public string Type { get; set; }
    }
}
