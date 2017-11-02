using Newtonsoft.Json;

namespace SearchHelper.Models.Bing
{
    public class Item1
    {
        [JsonProperty("answerType")]
        public string AnswerType { get; set; }
        [JsonProperty("value")]
        public Value3 Value { get; set; }
    }
}