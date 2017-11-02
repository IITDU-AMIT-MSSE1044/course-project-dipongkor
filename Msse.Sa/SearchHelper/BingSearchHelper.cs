using System;
using System.Net.Http;
using System.Threading.Tasks;
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
        private const string Mkt = "en-us";
        private const string Safesearch = "Off";

        protected HttpClient HttpClient { get; }

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
    }
}