using System.Text.Json.Serialization;

namespace AmaryllisTest.AutomatedAPI
{
    public class Source
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("crawl_rate")]
        public long CrawlRate { get; set; }
    }
}