using Newtonsoft.Json;

namespace SearchHelper.Models.Bing
{
    public class Webpages
    {
        [JsonProperty("webSearchUrl")]
        public string WebSearchUrl { get; set; }
        [JsonProperty("webSearchUrlPingSuffix")]
        public string WebSearchUrlPingSuffix { get; set; }
        [JsonProperty("totalEstimatedMatches")]
        public int TotalEstimatedMatches { get; set; }
        [JsonProperty("value")]
        public Webpage[] Webpage { get; set; }
    }
}