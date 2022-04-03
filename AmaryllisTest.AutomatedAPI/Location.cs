using System;
using System.Runtime.Intrinsics.X86;
using System.Text.Json.Serialization;

namespace AmaryllisTest.AutomatedAPI
{
    public class Location
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("location_type")]
        public string LocationType { get; set; }

        [JsonPropertyName("latt_long")]
        public string LattLong { get; set; }

        [JsonPropertyName("woeid")]
        public int Woeid { get; set; }

        [JsonPropertyName("distance")]
        public int Distance { get; set; }
    }
}