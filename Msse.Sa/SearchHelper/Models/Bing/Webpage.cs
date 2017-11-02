using System;
using Newtonsoft.Json;

namespace SearchHelper.Models.Bing
{
    public class Webpage
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("urlPingSuffix")]
        public string UrlPingSuffix { get; set; }
        [JsonProperty("displayUrl")]
        public string DisplayUrl { get; set; }
        [JsonProperty("snippet")]
        public string Snippet { get; set; }
        [JsonProperty("dateLastCrawled")]
        public DateTime DateLastCrawled { get; set; }
        [JsonProperty("about")]
        public About[] About { get; set; }
    }
}