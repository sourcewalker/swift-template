﻿using Newtonsoft.Json;

namespace $safeprojectname$.Ecommerce.Entities
{
    public class Product
    {
        [JsonProperty("ExternalId")]
        public string ExternalId { get; set; }

        [JsonProperty("GTIN")]
        public string Gtin { get; set; }

        [JsonProperty("MPN")]
        public string Mpn { get; set; }
    }
}
