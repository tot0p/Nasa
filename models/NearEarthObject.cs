using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Diagnostics.Contracts;

namespace TLEMAITRE1_nasa.models
{
    public class NearEarthObject
    {

        [JsonPropertyName("links")]
        public Links Links { get; set; } = new Links();

        [JsonPropertyName("id")]
        public string Id { get; set; } = "";

        [JsonPropertyName("neo_reference_id")]

        public string NeoReferenceId { get; set; } = "";

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("nasa_jpl_url")]
        public string NasaJplUrl { get; set; } = "";

        [JsonPropertyName("absolute_magnitude_h")]
        public double AbsoluteMagnitudeH { get; set; } = 0;

        [JsonPropertyName("estimated_diameter")]
        public Dictionary<string, Diameter> EstimatedDiameter { get; set; } = new Dictionary<string, Diameter>();

        [JsonPropertyName("is_potentially_hazardous_asteroid")]
        public bool IsPotentiallyHazardousAsteroid { get; set; } = false;

        [JsonPropertyName("close_approach_data")]
        public List<CloseApproachData> CloseApproachData { get; set; } = new List<CloseApproachData>();

        [JsonPropertyName("is_sentry_object")]
        public bool IsSentryObject { get; set; } = false;



    }
}
