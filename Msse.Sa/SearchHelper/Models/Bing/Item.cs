using Newtonsoft.Json;

namespace SearchHelper.Models.Bing
{
    public class Item
    {
        [JsonProperty("answerType")]
        public string AnswerType { get; set; }
        [JsonProperty("resultIndex")]
        public int ResultIndex { get; set; }
        [JsonProperty("value")]
        public Value2 Value { get; set; }
    }
}