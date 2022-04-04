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
            //List<Location> loaded;
            //using (HttpClient client = new HttpClient())
            //{
            //    var response = await client.GetAsync("https://www.metaweather.com/api/location/search/?query=min");
            //    loaded = JsonSerializer.Deserialize<List<Location>>(await response.Content.ReadAsStringAsync());
            //}
            //Assert.True(loaded.Any(loc => loc.Title == "Minsk"));
            Assert.IsNotNull(await GetLocation("min", "Minsk"));
        }

        [Test]
        public async Task Check_LattitudeLongitude()
        {
            Location city = await GetLocation("min", "Minsk");
            //using (HttpClient client = new HttpClient())
            //{
            //    var response = await client.GetAsync("https://www.metaweather.com/api/location/search/?query=min");
            //    city = JsonSerializer.Deserialize<List<Location>>(await response.Content.ReadAsStringAsync())
            //        .Single(loc => loc.Title == "Minsk");
            //}

            Assert.AreEqual(city.LattLong, "53.90255,27.563101");
        }

        [Test]
        public async Task Get_WeatherForecast()
        {
            Location city = await GetLocation("min", "Minsk");
            LocationInfo locationInfo = await GetLocationInfo(city);
            //using (HttpClient client = new HttpClient())
            //{
            //    var response = await client.GetAsync("https://www.metaweather.com/api/location/search/?query=min");
            //    city = JsonSerializer.Deserialize<List<Location>>(await response.Content.ReadAsStringAsync())
            //        .Single(loc => loc.Title == "Minsk");

            //    response = await client.GetAsync($"https://www.metaweather.com/api/location/{city.Woeid}");
            //    var locationInfo = JsonSerializer.Deserialize<LocationInfo>(await response.Content.ReadAsStringAsync());

            //    Assert.IsNotNull(locationInfo.ConsolidatedWeather.First());
            //}
            Assert.IsNotNull(locationInfo.ConsolidatedWeather.First());
        }

        [Test]
        public async Task Check_TemperatureGap()
        {
            Location city = await GetLocation("min", "Minsk");
            LocationInfo locationInfo = await GetLocationInfo(city);
            //using (HttpClient client = new HttpClient())
            //{
            //    var response = await client.GetAsync("https://www.metaweather.com/api/location/search/?query=min");
            //    city = JsonSerializer.Deserialize<List<Location>>(await response.Content.ReadAsStringAsync())
            //        .Single(loc => loc.Title == "Minsk");

            //    response = await client.GetAsync($"https://www.metaweather.com/api/location/{city.Woeid}");
            //    var locationInfo = JsonSerializer.Deserialize<LocationInfo>(await response.Content.ReadAsStringAsync());

            //    Assert.True(locationInfo.ConsolidatedWeather.All(weather => weather.MinTemp > -5 & weather.MaxTemp < 20));
            //}
            Assert.True(locationInfo.ConsolidatedWeather.All(weather => weather.MinTemp > -5 & weather.MaxTemp < 20));
        }

        [Test]
        public async Task Check_5Years()
        {
            DateTime currentDate = DateTime.Now;
            Location city = await GetLocation("min", "Minsk");
            LocationInfo locationInfo = await GetLocationInfo(city);
            List<ConsolidatedWeather> weathers = await GetByDate(currentDate.Year - 5, currentDate.Month, currentDate.Day,
                city);
            //using (HttpClient client = new HttpClient())
            //{
            //    var response = await client.GetAsync("https://www.metaweather.com/api/location/search/?query=min");
            //    city = await GetLocation("min", "Minsk");
            //    //city = JsonSerializer.Deserialize<List<Location>>(await response.Content.ReadAsStringAsync())
            //    //    .Single(loc => loc.Title == "Minsk");

            //    //response = await client.GetAsync($"https://www.metaweather.com/api/location/{city.Woeid}");
            //    //var locationInfo = JsonSerializer.Deserialize<LocationInfo>(await response.Content.ReadAsStringAsync());
            //    var locationInfo = await GetLocationInfo(city);

            //    response = await client.GetAsync($"https://www.metaweather.com/api/location/{city.Woeid}/{currentDate.Year - 5}/{currentDate.Month}/{currentDate.Day}");
            //    var weathers = JsonSerializer.Deserialize<List<ConsolidatedWeather>>(await response.Content.ReadAsStringAsync());

            //    Assert.True(weathers.Any(weather => weather.WeatherStateName == locationInfo.ConsolidatedWeather.First().WeatherStateName));
            //}
            Assert.True(weathers.Any(weather => weather.WeatherStateName == locationInfo.ConsolidatedWeather.First().WeatherStateName));
        }

        public async Task<Location> GetLocation(string query, string toSearch)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"https://www.metaweather.com/api/location/search/?query={query}");
                return JsonSerializer.Deserialize<List<Location>>(await response.Content.ReadAsStringAsync())
                    .Single(loc => loc.Title == toSearch);
            }
        }

        public async Task<LocationInfo> GetLocationInfo(Location location)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"https://www.metaweather.com/api/location/{location.Woeid}");
                return JsonSerializer.Deserialize<LocationInfo>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<List<ConsolidatedWeather>> GetByDate(int year, int month, int day, Location city)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"https://www.metaweather.com/api/location/{city.Woeid}/{year}/{month}/{day}");
                return JsonSerializer.Deserialize<List<ConsolidatedWeather>>(await response.Content.ReadAsStringAsync());
            }
        }
    }
}