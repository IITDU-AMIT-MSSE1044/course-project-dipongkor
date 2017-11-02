using Newtonsoft.Json;

namespace SearchHelper.Models.Google
{
    public class CseImage
    {
        [JsonProperty("src")]
        public string Src { get; set; }
    }
}