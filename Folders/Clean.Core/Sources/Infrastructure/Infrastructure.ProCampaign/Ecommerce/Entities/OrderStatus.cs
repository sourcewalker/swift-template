using Newtonsoft.Json;

namespace $safeprojectname$.Ecommerce.Entities
{
    public class OrderStatus
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}
