using System;
using Newtonsoft.Json;

namespace SearchHelper.Models.Bing
{
    public class BingSearchResult
    {
        [JsonProperty("_type")]
        public string Type { get; set; }
        [JsonProperty("instrumentation")]
        public Instrumentation Instrumentation { get; set; }
        [JsonProperty("queryContext")]
        public Querycontext QueryContext { get; set; }
        [JsonProperty("webPages")]
        public Webpages WebPages { get; set; }
        [JsonProperty("relatedSearches")]
        public Relatedsearches RelatedSearches { get; set; }
        [JsonProperty("rankingResponse")]
        public Rankingresponse RankingResponse { get; set; }
    }

    public class Instrumentation
    {
        [JsonProperty("pingUrlBase")]
        public string PingUrlBase { get; set; }
        [JsonProperty("pageLoadPingUrl")]
        public string PageLoadPingUrl { get; set; }
    }

    public class Querycontext
    {
        [JsonProperty("originalQuery")]
        public string OriginalQuery { get; set; }
    }

    public class Webpages
    {
        [JsonProperty("webSearchUrl")]
        public string WebSearchUrl { get; set; }
        [JsonProperty("webSearchUrlPingSuffix")]
        public string WebSearchUrlPingSuffix { get; set; }
        [JsonProperty("totalEstimatedMatches")]
        public int TotalEstimatedMatches { get; set; }
        [JsonProperty("value")]
        public Webpage[] Webpage { get; set; }
    }

    public class Webpage
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("urlPingSuffix")]
        public string UrlPingSuffix { get; set; }
        [JsonProperty("displayUrl")]
        public string DisplayUrl { get; set; }
        [JsonProperty("snippet")]
        public string Snippet { get; set; }
        [JsonProperty("dateLastCrawled")]
        public DateTime DateLastCrawled { get; set; }
        [JsonProperty("about")]
        public About[] About { get; set; }
    }

    public class About
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Relatedsearches
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("value")]
        public Relatedsearch[] Relatedsearch { get; set; }
    }

    public class Relatedsearch
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("displayText")]
        public string DisplayText { get; set; }
        [JsonProperty("webSearchUrl")]
        public string WebSearchUrl { get; set; }
        [JsonProperty("webSearchUrlPingSuffix")]
        public string WebSearchUrlPingSuffix { get; set; }
    }

    public class Rankingresponse
    {
        [JsonProperty("mainline")]
        public Mainline Mainline { get; set; }
        [JsonProperty("sidebar")]
        public Sidebar Sidebar { get; set; }
    }

    public class Mainline
    {
        [JsonProperty("items")]
        public Item[] Items { get; set; }
    }

    public class Item
    {
        [JsonProperty("answerType")]
        public string AnswerType { get; set; }
        [JsonProperty("resultIndex")]
        public int ResultIndex { get; set; }
        [JsonProperty("value")]
        public Value2 Value { get; set; }
    }

    public class Value2
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Sidebar
    {
        [JsonProperty("items")]
        public Item1[] Items { get; set; }
    }

    public class Item1
    {
        [JsonProperty("answerType")]
        public string AnswerType { get; set; }
        [JsonProperty("value")]
        public Value3 Value { get; set; }
    }

    public class Value3
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}