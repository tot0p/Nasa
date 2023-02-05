using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TLEMAITRE1_nasa.models
{    
    public class Apod
    {
        [JsonPropertyName("date")]
        public string Date { get; set; } = "";

        [JsonPropertyName("explanation")]
        public string Explanation { get; set; } = "";
        [JsonPropertyName("hdurl")]
        public string HdUrl { get; set; } = "";

        [JsonPropertyName("media_type")]
        public string MediaType { get; set; } = "";

        [JsonPropertyName("service_version")]
        public string ServiceVersion { get; set; } = "";

        [JsonPropertyName("title")]
        public string Title { get; set; } = "";

        [JsonPropertyName("url")]
        public string Url { get; set; } = "";

        [JsonPropertyName("copyright")]
        public string Copyright { get; set; } = "";
    }
}


