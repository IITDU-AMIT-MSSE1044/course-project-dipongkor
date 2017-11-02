using Newtonsoft.Json;

namespace SearchHelper.Models.Bing
{
    public class Querycontext
    {
        [JsonProperty("originalQuery")]
        public string OriginalQuery { get; set; }
    }
}