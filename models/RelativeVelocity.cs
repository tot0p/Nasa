using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace TLEMAITRE1_nasa.models
{
    public class RelativeVelocity
    {

        [JsonPropertyName("kilometers_per_second")]
        public string KilometersPerSecond { get; set; } = "";

        [JsonPropertyName("kilometers_per_hour")]
        public string KilometersPerHour { get; set; } = "";

        [JsonPropertyName("miles_per_hour")]
        public string MilesPerHour { get; set; } = "";

    }
}
