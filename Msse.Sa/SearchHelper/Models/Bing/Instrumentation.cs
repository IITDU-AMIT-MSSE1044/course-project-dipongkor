using Newtonsoft.Json;

namespace SearchHelper.Models.Bing
{
    public class Instrumentation
    {
        [JsonProperty("pingUrlBase")]
        public string PingUrlBase { get; set; }
        [JsonProperty("pageLoadPingUrl")]
        public string PageLoadPingUrl { get; set; }
    }
}