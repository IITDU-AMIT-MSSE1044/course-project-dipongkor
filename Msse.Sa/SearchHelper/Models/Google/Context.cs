using Newtonsoft.Json;

namespace SearchHelper.Models.Google
{
    public class Context
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}