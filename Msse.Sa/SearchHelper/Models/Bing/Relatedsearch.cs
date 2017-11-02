using Newtonsoft.Json;

namespace SearchHelper.Models.Bing
{
    public class Relatedsearch
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("displayText")]
        public string DisplayText { get; set; }
        [JsonProperty("webSearchUrl")]
        public string WebSearchUrl { get; set; }
        [JsonProperty("webSearchUrlPingSuffix")]
        public string WebSearchUrlPingSuffix { get; set; }
    }
}