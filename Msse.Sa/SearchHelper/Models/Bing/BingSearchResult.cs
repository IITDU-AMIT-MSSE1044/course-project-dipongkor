using Newtonsoft.Json;

namespace SearchHelper.Models.Bing
{
    public class BingSearchResult
    {
        [JsonProperty("_type")]
        public string Type { get; set; }
        [JsonProperty("instrumentation")]
        public Instrumentation Instrumentation { get; set; }
        [JsonProperty("queryContext")]
        public Querycontext QueryContext { get; set; }
        [JsonProperty("webPages")]
        public Webpages WebPages { get; set; }
        [JsonProperty("relatedSearches")]
        public Relatedsearches RelatedSearches { get; set; }
        [JsonProperty("rankingResponse")]
        public Rankingresponse RankingResponse { get; set; }
    }
}