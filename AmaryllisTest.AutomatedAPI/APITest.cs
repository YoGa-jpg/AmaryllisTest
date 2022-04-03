using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace AmaryllisTest.AutomatedAPI
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Get_Locations_ContainsMinsk()
        {
            List<Location> loaded;
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://www.metaweather.com/api/location/search/?query=min");
                loaded = JsonSerializer.Deserialize<List<Location>>(await response.Content.ReadAsStringAsync());
            }
            Assert.True(loaded.Any(loc => loc.Title == "Minsk"));
        }

        [Test]
        public async Task Check_LattitudeLongitude()
        {
            Location city;
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://www.metaweather.com/api/location/search/?query=min");
                city = JsonSerializer.Deserialize<List<Location>>(await response.Content.ReadAsStringAsync())
                    .Single(loc => loc.Title == "Minsk");
            }

            Assert.AreEqual(city.LattLong, "53.90255,27.563101");
        }

        [Test]
        public async Task Get_WeatherForecast()
        {
            Location city;
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://www.metaweather.com/api/location/search/?query=min");
                city = JsonSerializer.Deserialize<List<Location>>(await response.Content.ReadAsStringAsync())
                    .Single(loc => loc.Title == "Minsk");

                response = await client.GetAsync($"https://www.metaweather.com/api/location/{city.Woeid}");
                var locationInfo = JsonSerializer.Deserialize<LocationInfo>(await response.Content.ReadAsStringAsync());

                Assert.IsNotNull(locationInfo.ConsolidatedWeather.First());
            }
        }

        [Test]
        public async Task Check_TemperatureGap()
        {
            Location city;
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://www.metaweather.com/api/location/search/?query=min");
                city = JsonSerializer.Deserialize<List<Location>>(await response.Content.ReadAsStringAsync())
                    .Single(loc => loc.Title == "Minsk");

                response = await client.GetAsync($"https://www.metaweather.com/api/location/{city.Woeid}");
                var locationInfo = JsonSerializer.Deserialize<LocationInfo>(await response.Content.ReadAsStringAsync());

                Assert.True(locationInfo.ConsolidatedWeather.All(weather => weather.MinTemp > -5 & weather.MaxTemp < 20));
            }
        }

        [Test]
        public async Task Check_5Years()
        {
            Location city;
            DateTime currentDate = DateTime.Now;
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://www.metaweather.com/api/location/search/?query=min");
                city = JsonSerializer.Deserialize<List<Location>>(await response.Content.ReadAsStringAsync())
                    .Single(loc => loc.Title == "Minsk");

                response = await client.GetAsync($"https://www.metaweather.com/api/location/{city.Woeid}");
                var locationInfo = JsonSerializer.Deserialize<LocationInfo>(await response.Content.ReadAsStringAsync());

                response = await client.GetAsync($"https://www.metaweather.com/api/location/{city.Woeid}/{currentDate.Year - 5}/{currentDate.Month}/{currentDate.Day}");
                var weathers = JsonSerializer.Deserialize<List<ConsolidatedWeather>>(await response.Content.ReadAsStringAsync());

                Assert.True(weathers.Any(weather => weather.WeatherStateName == locationInfo.ConsolidatedWeather.First().WeatherStateName));
            }
        }
    }
}