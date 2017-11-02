using Newtonsoft.Json;

namespace SearchHelper.Models.Bing
{
    public class Mainline
    {
        [JsonProperty("items")]
        public Item[] Items { get; set; }
    }
}