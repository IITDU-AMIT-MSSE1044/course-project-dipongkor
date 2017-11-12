﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;
using SearchHelper.Models.Bing;

namespace SearchHelper
{
    public class BingSearchHelper
    {
        //Bing Search Config
        private const string SearchKey = "56bb069f0d564756b02e4db14e5c4ea4";
        private const string BaseUrl = "https://api.cognitive.microsoft.com/bing/v7.0/search";
        private const int Count = 20;
        private const int Offset = 0;
        private readonly string Mkt = "en-us";
        private const string Safesearch = "Off";

        protected HttpClient HttpClient { get; }

        public BingSearchHelper(string mkt)
        {
            Mkt = mkt;
        }

        public BingSearchHelper()
        {
            HttpClient = new HttpClient {BaseAddress = new Uri(BaseUrl)};
            HttpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SearchKey);
        }

        public async Task<BingSearchResult> GetSearchResults(string serchTerm)
        {
            var query = $"?q={serchTerm}&count={Count}&offset={Offset}&mkt={Mkt}&safesearch={Safesearch}";
            var response = await HttpClient.GetAsync(query);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BingSearchResult>(content);
        }

        public List<HtmlResult> GetHtmlResults(string query)
        {
            var url = $"https://www.bing.com/search?mkt={Mkt}&q={query}&safe=off&count=10";
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var htmlResults = doc.DocumentNode.Descendants("h2")
                .Where(m => m.FirstChild.Name == "a")
                .Select(m =>
                {
                    var href = m.FirstChild.Attributes["href"];
                    return new HtmlResult { Title = m.FirstChild.InnerText, Url = new Uri(href.Value, UriKind.RelativeOrAbsolute)};
                }).ToList();

            return htmlResults;
        }
    }
}