using Newtonsoft.Json;

namespace SearchHelper.Models.Google
{
    public class Pagemap
    {
        [JsonProperty("cse_thumbnail")]
        public CseThumbnail[] CseThumbnail { get; set; }
        [JsonProperty("metatags")]
        public Metatag[] Metatags { get; set; }
        [JsonProperty("cse_image")]
        public CseImage[] CseImage { get; set; }
        [JsonProperty("webpage")]
        public Webpage[] Webpage { get; set; }
        [JsonProperty("imageobject")]
        public Imageobject[] Imageobject { get; set; }
        [JsonProperty("person")]
        public Person[] Person { get; set; }
        [JsonProperty("videoobject")]
        public Videoobject[] Videoobject { get; set; }
        [JsonProperty("searchresultspage")]
        public Searchresultspage[] Searchresultspage { get; set; }
    }
}