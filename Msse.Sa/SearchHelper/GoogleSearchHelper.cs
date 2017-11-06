using System;
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

        protected HttpClient HttpClient { get; }

        public GoogleSearchHelper()
        {
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public async Task<GoogleSearchResult> GetSearchResults(string serchTerm)
        {
            var query = $"?key={SearchKey}&cx={Cx}&q={serchTerm}&num={Count}&safe={Safesearch}";
            var response = await HttpClient.GetAsync(query);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GoogleSearchResult>(content);
        }

        public HtmlDocument GetHtmlResult(string query)
        {

            var url = $"https://www.google.com/search?num=10&safe=off&q={query}";
            var web = new HtmlWeb();
            var doc = web.Load(url);

            return doc;
        }
    }
}