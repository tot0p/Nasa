using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace TLEMAITRE1_nasa.models
{
    public class MissDistance
    {
        [JsonPropertyName("astronomical")]
        public string Astronomical { get; set; } = "";

        [JsonPropertyName("lunar")]
        public string Lunar { get; set; } = "";

        [JsonPropertyName("kilometers")]

        public string Kilometers { get; set; } = "";

        [JsonPropertyName("miles")]

        public string Miles { get; set; } = "";
    }
}
