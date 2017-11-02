using System;
using Newtonsoft.Json;

namespace SearchHelper.Models.Bing
{
    public class BingSearchResult
    {
        [JsonProperty("_type")]
        public string Type { get; set; }
        public Instrumentation instrumentation { get; set; }
        public Querycontext queryContext { get; set; }
        public Webpages webPages { get; set; }
        public Relatedsearches relatedSearches { get; set; }
        public Rankingresponse rankingResponse { get; set; }
    }

    public class Instrumentation
    {
        public string pingUrlBase { get; set; }
        public string pageLoadPingUrl { get; set; }
    }

    public class Querycontext
    {
        public string originalQuery { get; set; }
    }

    public class Webpages
    {
        public string webSearchUrl { get; set; }
        public string webSearchUrlPingSuffix { get; set; }
        public int totalEstimatedMatches { get; set; }
        public Value[] value { get; set; }
    }

    public class Value
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string urlPingSuffix { get; set; }
        public string displayUrl { get; set; }
        public string snippet { get; set; }
        public DateTime dateLastCrawled { get; set; }
        public About[] about { get; set; }
    }

    public class About
    {
        public string name { get; set; }
    }

    public class Relatedsearches
    {
        public string id { get; set; }
        public Value1[] value { get; set; }
    }

    public class Value1
    {
        public string text { get; set; }
        public string displayText { get; set; }
        public string webSearchUrl { get; set; }
        public string webSearchUrlPingSuffix { get; set; }
    }

    public class Rankingresponse
    {
        public Mainline mainline { get; set; }
        public Sidebar sidebar { get; set; }
    }

    public class Mainline
    {
        public Item[] items { get; set; }
    }

    public class Item
    {
        public string answerType { get; set; }
        public int resultIndex { get; set; }
        public Value2 value { get; set; }
    }

    public class Value2
    {
        public string id { get; set; }
    }

    public class Sidebar
    {
        public Item1[] items { get; set; }
    }

    public class Item1
    {
        public string answerType { get; set; }
        public Value3 value { get; set; }
    }

    public class Value3
    {
        public string id { get; set; }
    }
}