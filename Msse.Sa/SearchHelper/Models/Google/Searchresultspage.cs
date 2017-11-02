using Newtonsoft.Json;

namespace SearchHelper.Models.Google
{
    public class Searchresultspage
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("significantlinks")]
        public string Significantlinks { get; set; }
        [JsonProperty("maincontentofpage")]
        public string Maincontentofpage { get; set; }
        [JsonProperty("breadcrumb")]
        public string Breadcrumb { get; set; }
    }
}