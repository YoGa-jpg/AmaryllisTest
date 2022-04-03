using System;
using System.Text.Json.Serialization;

namespace AmaryllisTest.AutomatedAPI
{
    public class LocationInfo
    {
        [JsonPropertyName("consolidated_weather")]
        public ConsolidatedWeather[] ConsolidatedWeather { get; set; }

        [JsonPropertyName("time")]
        public DateTimeOffset Time { get; set; }

        [JsonPropertyName("sun_rise")]
        public DateTimeOffset SunRise { get; set; }

        [JsonPropertyName("sun_set")]
        public DateTimeOffset SunSet { get; set; }

        [JsonPropertyName("timezone_name")]
        public string TimezoneName { get; set; }

        [JsonPropertyName("parent")]
        public Location Parent { get; set; }

        [JsonPropertyName("sources")]
        public Source[] Sources { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("location_type")]
        public string LocationType { get; set; }

        [JsonPropertyName("woeid")]
        public long Woeid { get; set; }

        [JsonPropertyName("latt_long")]
        public string LattLong { get; set; }

        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }
    }
}