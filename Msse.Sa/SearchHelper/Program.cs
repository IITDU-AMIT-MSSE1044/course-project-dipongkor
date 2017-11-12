using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Nager.PublicSuffix;
using Newtonsoft.Json;
using SearchHelper.Models.Bing;

namespace SearchHelper
{
    namespace BingSearchApisQuickstart
    {

        class Program
        {
            delegate bool IsMissing(HtmlResult result, List<HtmlResult> results);

            delegate string GetFollowUpQuery(string query, HtmlResult result);

            delegate string GetReverseJdQuery(string query);

            static void Main(string[] args)
            {
                var data = File.ReadAllText("data.json");
                var dataAsDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(data);
                //MrSite(dataAsDictionary);
                //MrTitle(dataAsDictionary);
                //Console.WriteLine(MpReverseJd());
                Console.WriteLine(UniversalSwapJd());
                Console.ReadKey();
            }

            static void MrTitle(Dictionary<string, List<string>> dataAsDictionary)
            {
                IsMissing mrTitleMissingDelegate = (htmlResult, htmlResults) =>
                {
                    return !htmlResults.Any(item =>
                        item.Url.Host == htmlResult.Url.Host && item.Title == htmlResult.Title);
                };

                GetFollowUpQuery mrTitleGetFollowUpQueryDelegate = (query, htmlResult) =>
                {
                    var followUpQuery = $"{query} {htmlResult.Title}";
                    return followUpQuery;
                };

                Console.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
                //MRTitle (English Bing, English Query)
                var mrTitleEnglishBingEnglishQuery = CalculateRocof("en-us", dataAsDictionary["en-us"], mrTitleGetFollowUpQueryDelegate, mrTitleMissingDelegate);
                File.WriteAllText("mrTitleEnglishBingEnglishQuery.txt", $"MRSite (English Bing, English Query) = {mrTitleEnglishBingEnglishQuery}");
                Console.WriteLine($"MRTitle (English Bing, English Query) = {mrTitleEnglishBingEnglishQuery}");

                //MRTitle (English Bing, Chinese Query)
                var mrTitleEnglishBingChineseQuery = CalculateRocof("en-us", dataAsDictionary["zh-CN"],
                    mrTitleGetFollowUpQueryDelegate, mrTitleMissingDelegate);
                File.WriteAllText("mrTitleEnglishBingChineseQuery.txt", $"MRSite (English Bing, Chinese Query) = {mrTitleEnglishBingChineseQuery}");
                Console.WriteLine($"MRTitle (English Bing, Chinese Query) = {mrTitleEnglishBingChineseQuery}");


                //MRTitle (Chinese Bing, English Query)
                var mrTitleChineseBingEnglishQuery = CalculateRocof("zh-CN", dataAsDictionary["en-us"], mrTitleGetFollowUpQueryDelegate, mrTitleMissingDelegate);
                File.WriteAllText("mrTitleChineseBingEnglishQuery.txt", $"MRSite (Chinese Bing, English Query) = {mrTitleChineseBingEnglishQuery}");
                Console.WriteLine($"MRTitle (Chinese Bing, English Query) = {mrTitleChineseBingEnglishQuery}");

                //MRTitle (Chinese Bing, Chinese Query)
                var mrTitleChineseBingChineseQuery = CalculateRocof("zh-CN", dataAsDictionary["zh-CN"],
                    mrTitleGetFollowUpQueryDelegate, mrTitleMissingDelegate);
                File.WriteAllText("mrTitleChineseBingChineseQuery.txt", $"MRSite (Chinese Bing, Chinese Query) = {mrTitleChineseBingChineseQuery}");
                Console.WriteLine($"MRTitle (Chinese Bing, Chinese Query) = {mrTitleChineseBingChineseQuery}");
                Console.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
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

                Console.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
                //MRSite (English bing, English Query)
                var mrSiteEnglishbingEnglishQuery = CalculateRocof("en-us", dataAsDictionary["en-us"], mrSiteGetFollowUpQueryDelegate, mrSiteMissingDelegate);
                File.WriteAllText("mrSiteEnglishbingEnglishQuery.txt", $"MRSite (English bing, English Query) = {mrSiteEnglishbingEnglishQuery}");
                Console.WriteLine($"MRSite (English bing, English Query) = {mrSiteEnglishbingEnglishQuery}");

                //MRSite (English bing, Chinese Query)
                var mrSiteEnglishbingChineseQuery = CalculateRocof("en-us", dataAsDictionary["zh-CN"],
                    mrSiteGetFollowUpQueryDelegate, mrSiteMissingDelegate);
                File.WriteAllText("mrSiteEnglishbingChineseQuery.txt", $"MRSite (English bing, Chinese Query) = {mrSiteEnglishbingChineseQuery}");
                Console.WriteLine($"MRSite (English bing, Chinese Query) = {mrSiteEnglishbingChineseQuery}");


                //MRSite (Chinese bing, English Query)
                var mrSiteChinesebingEnglishQuery = CalculateRocof("zh-CN", dataAsDictionary["en-us"], mrSiteGetFollowUpQueryDelegate, mrSiteMissingDelegate);
                File.WriteAllText("mrSiteChinesebingEnglishQuery.txt", $"MRSite (Chinese bing, English Query) = {mrSiteChinesebingEnglishQuery}");
                Console.WriteLine($"MRSite (Chinese bing, English Query) = {mrSiteChinesebingEnglishQuery}");

                //MRSite (Chinese bing, Chinese Query)
                var mrSiteChinesebingChineseQuery = CalculateRocof("zh-CN", dataAsDictionary["zh-CN"],
                    mrSiteGetFollowUpQueryDelegate, mrSiteMissingDelegate);
                File.WriteAllText("mrSiteChinesebingChineseQuery.txt", $"MRSite (Chinese bing, Chinese Query) = {mrSiteChinesebingChineseQuery}");
                Console.WriteLine($"MRSite (Chinese bing, Chinese Query) = {mrSiteChinesebingChineseQuery}");
                Console.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
            }

            static double MpReverseJd()
            {
                var data = File.ReadAllText("hot.json");
                var queries = JsonConvert.DeserializeObject<List<string>>(data);
                var jdSum = 0.0;
                var bingSearchHelper = new BingSearchHelper("en-us");

                GetReverseJdQuery getOriginalQuery = query =>
                {
                    var words = query.Split(' ').Select(q => $"\"{q}\"");
                    return string.Join(" ", words);
                };

                GetReverseJdQuery getReversedQuery = query =>
                {
                    var words = query.Split(' ').Reverse().Select(q => $"\"{q}\"");
                    return string.Join(" ", words);
                };

                foreach (var query in queries)
                {
                    var originaQueryResults = bingSearchHelper.GetHtmlResults(getOriginalQuery(query));
                    var reverseQueryResults = bingSearchHelper.GetHtmlResults(getReversedQuery(query));

                    jdSum +=
                        (double)
                        originaQueryResults.Select(m => m.Title)
                            .ToList()
                            .Intersect(reverseQueryResults.Select(m => m.Title).ToList())
                            .Count() /
                        originaQueryResults.Select(m => m.Title)
                            .ToList()
                            .Union(reverseQueryResults.Select(m => m.Title).ToList())
                            .Count();
                }

                return jdSum / queries.Count;
            }

            static double UniversalSwapJd()
            {
                var data = File.ReadAllText("hot.json");
                var queries = JsonConvert.DeserializeObject<List<string>>(data);
                var jdSum = 0.0;
                var bingSearchHelper = new BingSearchHelper("en-us");

                GetReverseJdQuery getOriginalQuery = query =>
                {
                    var words = query.Split(' ').Select(q => $"{q}");
                    return string.Join(" ", words);
                };

                GetReverseJdQuery getReversedQuery = query =>
                {
                    var words = query.Split(' ').Reverse().Select(q => $"{q}");
                    return string.Join(" ", words);
                };

                foreach (var query in queries)
                {
                    var originaQueryResults = bingSearchHelper.GetHtmlResults(getOriginalQuery(query));
                    var reverseQueryResults = bingSearchHelper.GetHtmlResults(getReversedQuery(query));

                    jdSum +=
                        (double)
                        originaQueryResults.Select(m => m.Title)
                            .ToList()
                            .Intersect(reverseQueryResults.Select(m => m.Title).ToList())
                            .Count() /
                        originaQueryResults.Select(m => m.Title)
                            .ToList()
                            .Union(reverseQueryResults.Select(m => m.Title).ToList())
                            .Count();
                }

                return jdSum / queries.Count;
            }

            static double SwapJdWithDomain()
            {
                var data = File.ReadAllText("hot.json");
                var queries = JsonConvert.DeserializeObject<List<string>>(data);
                var jdSum = 0.0;
                var bingSearchHelper = new BingSearchHelper("en-us");

                GetReverseJdQuery getOriginalQuery = query =>
                {
                    var words = query.Split(' ').Select(q => $"{q}");
                    return $"{string.Join(" ", words)} site:.com, site:.edu, site:.mil or site:.lc";
                };

                GetReverseJdQuery getReversedQuery = query =>
                {
                    var words = query.Split(' ').Reverse().Select(q => $"{q}");
                    return $"{string.Join(" ", words)} site:.com, site:.edu, site:.mil or site:.lc";
                };

                foreach (var query in queries)
                {
                    var originaQueryResults = bingSearchHelper.GetHtmlResults(getOriginalQuery(query));
                    var reverseQueryResults = bingSearchHelper.GetHtmlResults(getReversedQuery(query));

                    jdSum +=
                        (double)
                        originaQueryResults.Select(m => m.Title)
                            .ToList()
                            .Intersect(reverseQueryResults.Select(m => m.Title).ToList())
                            .Count() /
                        originaQueryResults.Select(m => m.Title)
                            .ToList()
                            .Union(reverseQueryResults.Select(m => m.Title).ToList())
                            .Count();
                }

                return jdSum / queries.Count;
            }

            static double CalculateRocof(string hl, List<string> queries, GetFollowUpQuery getFollowUpQuery,
                IsMissing isMissing)
            {
                var totalMrSite = 0;
                var bingSearchHelper = new BingSearchHelper(hl);
                foreach (var query in queries)
                {
                    var originalQueryResults = bingSearchHelper.GetHtmlResults(query)
                        .Where(m => m.Url.IsAbsoluteUri);
                    foreach (var originalQueryResult in originalQueryResults)
                    {
                        var followUpQueryResults =
                            bingSearchHelper.GetHtmlResults(getFollowUpQuery(query, originalQueryResult));
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
