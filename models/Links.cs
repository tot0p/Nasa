using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace TLEMAITRE1_nasa.models
{
    public class Links
    {
        [JsonPropertyName("next")]
        public string Next { get; set; } = "";

        [JsonPropertyName("previous")]
        public string Prev { get; set; } = "";

        [JsonPropertyName("self")]
        public string Self { get; set; } = "";
    }
}
