using Newtonsoft.Json;

namespace SearchHelper.Models.Bing
{
    public class Sidebar
    {
        [JsonProperty("items")]
        public Item1[] Items { get; set; }
    }
}