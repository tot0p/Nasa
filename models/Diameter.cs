using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace TLEMAITRE1_nasa.models
{
    public class Diameter
    {
        [JsonPropertyName("estimated_diameter_min")]
        public double EstimatedDiameterMin { get; set; } = 0;

        [JsonPropertyName("estimated_diameter_max")]
        public double EstimatedDiameterMax { get; set; } = 0;
    }
}
