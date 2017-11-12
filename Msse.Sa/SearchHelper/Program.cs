using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nager.PublicSuffix;
using Newtonsoft.Json;
using SearchHelper.Models.Google;

namespace SearchHelper
{
    namespace BingSearchApisQuickstart
    {

        class Program
        {
            delegate bool IsMissing(HtmlResult result, List<HtmlResult> results);

            delegate string GetFollowUpQuery(string query, HtmlResult result);
            static void Main(string[] args)
            {
                var data = File.ReadAllText("data.json");
                var dataAsDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(data);
                MrSite(dataAsDictionary);
                //MrTitle(dataAsDictionary);
                Console.ReadKey();
            }

            static void MrSite(Dictionary<string, List<string>> dataAsDictionary)
            {
                IsMissing mrSiteMissingDelegate = (htmlResult, htmlResults) =>
                {
                    return !htmlResults.Any(item =>
                        item.Url.Host == htmlResult.Url.Host && item.Title == htmlResult.Title);
                };

                GetFollowUpQuery mrSiteGetFollowUpQueryDelegate = (query, htmlResult) =>
                {
                    var domainParser = new DomainParser(new WebTldRuleProvider());
                    var domain = domainParser.Get(htmlResult.Url.Host);
                    var followUpQuery = $"{query} site:.{domain.TLD}";
                    return followUpQuery;
                };

                //MRSite (English Google, English Query)

                var mrSiteEnglishGoogleEnglishQuery = CalculateRocof("en-us", dataAsDictionary["en-us"], mrSiteGetFollowUpQueryDelegate, mrSiteMissingDelegate);
                File.WriteAllText("mrSiteEnglishGoogleEnglishQuery.txt", $"MRSite (English Google, English Query) = {mrSiteEnglishGoogleEnglishQuery}");
                Console.WriteLine($"MRSite (English Google, English Query) = {mrSiteEnglishGoogleEnglishQuery}");

                ////MRSite (English Google, Chinese Query)
                //var mrSiteEnglishGoogleChineseQuery = CalculateRocof("en-us", dataAsDictionary["zh-CN"],
                //    mrSiteGetFollowUpQueryDelegate, mrSiteMissingDelegate);
                //File.WriteAllText("mrSiteEnglishGoogleChineseQuery.txt", $"MRSite (English Google, Chinese Query) = {mrSiteEnglishGoogleChineseQuery}");
                //Console.WriteLine($"MRSite (English Google, Chinese Query) = {mrSiteEnglishGoogleChineseQuery}");


                ////MRSite (Chinese Google, English Query)

                //var mrSiteChineseGoogleEnglishQuery = CalculateRocof("zh-CN", dataAsDictionary["en-us"], mrSiteGetFollowUpQueryDelegate, mrSiteMissingDelegate);
                //File.WriteAllText("mrSiteChineseGoogleEnglishQuery.txt", $"MRSite (Chinese Google, English Query) = {mrSiteChineseGoogleEnglishQuery}");
                //Console.WriteLine($"MRSite (Chinese Google, English Query) = {mrSiteChineseGoogleEnglishQuery}");

                ////MRSite (Chinese Google, Chinese Query)
                //var mrSiteChineseGoogleChineseQuery = CalculateRocof("zh-CN", dataAsDictionary["zh-CN"],
                //    mrSiteGetFollowUpQueryDelegate, mrSiteMissingDelegate);
                //File.WriteAllText("mrSiteChineseGoogleChineseQuery.txt", $"MRSite (Chinese Google, Chinese Query) = {mrSiteChineseGoogleChineseQuery}");
                //Console.WriteLine($"MRSite (Chinese Google, Chinese Query) = {mrSiteChineseGoogleChineseQuery}");
            }

            static double CalculateRocof(string hl, List<string> queries, GetFollowUpQuery getFollowUpQuery,
                IsMissing isMissing)
            {
                var totalMrSite = 0;
                var googleSearchHelper = new GoogleSearchHelper(hl);
                foreach (var query in queries)
                {
                    var originalQueryResults = googleSearchHelper.GetHtmlResult(query)
                        .Where(m=>m.Url.IsAbsoluteUri);
                    foreach (var originalQueryResult in originalQueryResults)
                    {
                        var followUpQueryResults =
                            googleSearchHelper.GetHtmlResult(getFollowUpQuery(query, originalQueryResult));
                        if (isMissing(originalQueryResult, followUpQueryResults))
                        {
                            totalMrSite++;
                        }
                    }
                }
                return (double)totalMrSite / (queries.Count * 10);
            }
        }
    }
}
