using Newtonsoft.Json;

namespace SearchHelper.Models.Google
{
    public class Searchinformation
    {
        [JsonProperty("searchTime")]
        public float SearchTime { get; set; }
        [JsonProperty("formattedSearchTime")]
        public string FormattedSearchTime { get; set; }
        [JsonProperty("totalResults")]
        public string TotalResults { get; set; }
        [JsonProperty("formattedTotalResults")]
        public string FormattedTotalResults { get; set; }
    }
}