using Newtonsoft.Json;

namespace SearchHelper.Models.Google
{
    public class Webpage
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty]
        public string Thumbnailurl { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
    }
}