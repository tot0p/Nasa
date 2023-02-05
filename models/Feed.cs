using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Data.SqlTypes;
using System.Collections.Specialized;

namespace TLEMAITRE1_nasa.models
{
    public class Feed
    {

        [JsonPropertyName("links")]
        public Links Links { get; set; } = new Links();

        [JsonPropertyName("element_count")]
        public int ElementCount { get; set; } = 0;

        [JsonPropertyName("near_earth_objects")]
        public Dictionary<string, List<NearEarthObject>> NearEarthObjects { get; set; } = new Dictionary<string, List<NearEarthObject>>();



        public string GetRange()
        {
            Uri uri = new Uri(Links.Self);
            string query = uri.Query;
            NameValueCollection queryCollection = System.Web.HttpUtility.ParseQueryString(query);
            return queryCollection["start_date"] + " / " + queryCollection["end_date"];
        }
    }
}
