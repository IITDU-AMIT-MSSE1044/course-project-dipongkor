using Newtonsoft.Json;

namespace SearchHelper.Models.Google
{
    public class Person
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}