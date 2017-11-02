using Newtonsoft.Json;

namespace SearchHelper.Models.Google
{
    public class Queries
    {
        [JsonProperty("request")]
        public Request[] Request { get; set; }
        [JsonProperty("nextPage")]
        public Nextpage[] NextPage { get; set; }
    }
}