using Newtonsoft.Json;

namespace SearchHelper.Models.Google
{
    public class Url
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("template")]
        public string Template { get; set; }
    }
}