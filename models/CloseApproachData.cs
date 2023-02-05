using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace TLEMAITRE1_nasa.models
{
    public class CloseApproachData
    {

        [JsonPropertyName("close_approach_date")]
        public string CloseApproachDate { get; set; } = "";

        [JsonPropertyName("close_approach_date_full")]

        public string CloseApproachDateFull { get; set; } = "";

        [JsonPropertyName("epoch_date_close_approach")]

        public long EpochDateCloseApproach { get; set; } = 0;


        [JsonPropertyName("relative_velocity")]
        public RelativeVelocity RelativeVelocity { get; set; } = new RelativeVelocity();

        [JsonPropertyName("miss_distance")]
        public MissDistance MissDistance { get; set; } = new MissDistance();

        [JsonPropertyName("orbiting_body")]

        public string OrbitingBody { get; set; } = "";

        


    }
}
