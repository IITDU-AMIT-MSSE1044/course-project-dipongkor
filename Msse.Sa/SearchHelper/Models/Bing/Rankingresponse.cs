using Newtonsoft.Json;

namespace SearchHelper.Models.Bing
{
    public class Rankingresponse
    {
        [JsonProperty("mainline")]
        public Mainline Mainline { get; set; }
        [JsonProperty("sidebar")]
        public Sidebar Sidebar { get; set; }
    }
}