using System;
using System.Text.Json.Serialization;

namespace AmaryllisTest.AutomatedAPI
{
    public class ConsolidatedWeather
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("weather_state_name")]
        public string WeatherStateName { get; set; }

        [JsonPropertyName("weather_state_abbr")]
        public string WeatherStateAbbr { get; set; }

        [JsonPropertyName("wind_direction_compass")]
        public string WindDirectionCompass { get; set; }

        [JsonPropertyName("created")]
        public DateTimeOffset Created { get; set; }

        [JsonPropertyName("applicable_date")]
        public DateTimeOffset ApplicableDate { get; set; }

        [JsonPropertyName("min_temp")]
        public double MinTemp { get; set; }

        [JsonPropertyName("max_temp")]
        public double MaxTemp { get; set; }

        [JsonPropertyName("the_temp")]
        public float TheTemp { get; set; }

        [JsonPropertyName("wind_speed")]
        public double WindSpeed { get; set; }

        [JsonPropertyName("wind_direction")]
        public double WindDirection { get; set; }

        [JsonPropertyName("air_pressure")]
        public double AirPressure { get; set; }

        [JsonPropertyName("humidity")]
        public double Humidity { get; set; }

        //[JsonPropertyName("visibility")]
        //public float Visibility { get; set; }

        [JsonPropertyName("predictability")]
        public int Predictability { get; set; }
    }
}