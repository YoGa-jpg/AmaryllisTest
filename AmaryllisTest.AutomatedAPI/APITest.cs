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
    }
}