using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;
using SearchHelper.Models.Google;

namespace SearchHelper
{
    public class GoogleSearchHelper
    {
        private const string SearchKey = "AIzaSyBqz1ptbOrmpafUsZ4r7mS0oc11LCCmCTU";
        private const string Cx = "016744644215251562370:yoxh7_21puy";
        private const string BaseUrl = "https://www.googleapis.com/customsearch/v1";
        private const string Safesearch = "off";
        private const int Count = 10;
        private readonly string Hl = "";

        protected HttpClient HttpClient { get; }

        public GoogleSearchHelper()
        {
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public GoogleSearchHelper(string hl)
        {
            Hl = hl;
        }



        public async Task<GoogleSearchResult> GetSearchResults(string serchTerm)
        {
            var query = $"?key={SearchKey}&cx={Cx}&q={serchTerm}&num={Count}&safe={Safesearch}";
            var response = await HttpClient.GetAsync(query);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GoogleSearchResult>(content);
        }

        public List<HtmlResult> GetHtmlResults(string query)
        {
            var url = $"https://www.google.com.hk/search?safe=strict&hl={Hl}&q={query}&num=10&searchType=";
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var htmlResults = doc.DocumentNode.Descendants("h3")
                .Where(x => x.FirstChild.Name == "a")
                .Select(m => new HtmlResult
                {
                    Title = m.FirstChild.InnerText,
                    Url = new Uri(m.FirstChild.Attributes["href"].Value.Substring(m.FirstChild.Attributes["href"].Value.IndexOf("=") + 1, m.FirstChild.Attributes["href"].Value.IndexOf("&")), UriKind.RelativeOrAbsolute)
                }).ToList();
            return htmlResults;
        }
    }
}