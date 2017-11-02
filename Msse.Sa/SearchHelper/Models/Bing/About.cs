using Newtonsoft.Json;

namespace SearchHelper.Models.Bing
{
    public class About
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}