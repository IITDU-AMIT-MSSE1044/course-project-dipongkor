using Newtonsoft.Json;

namespace SearchHelper.Models.Google
{

    public class GoogleSearchResult
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }
        [JsonProperty("url")]
        public Url Url { get; set; }
        [JsonProperty("queries")]
        public Queries Queries { get; set; }
        [JsonProperty("context")]
        public Context Context { get; set; }
        [JsonProperty("searchInformation")]
        public Searchinformation SearchInformation { get; set; }
        [JsonProperty("items")]
        public Item[] Items { get; set; }
    }
}