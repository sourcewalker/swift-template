﻿using Newtonsoft.Json;

namespace $safeprojectname$.Ecommerce.Entities
{
    public class Source
    {
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Origin")]
        public string Origin { get; set; }
    }
}
