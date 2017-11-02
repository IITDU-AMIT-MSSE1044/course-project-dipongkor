using Newtonsoft.Json;

namespace SearchHelper.Models.Bing
{
    public class Relatedsearches
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("value")]
        public Relatedsearch[] Relatedsearch { get; set; }
    }
}