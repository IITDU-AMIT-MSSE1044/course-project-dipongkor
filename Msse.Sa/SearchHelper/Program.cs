using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Nager.PublicSuffix;
using Newtonsoft.Json;
using SearchHelper.Models.Google;

namespace SearchHelper
{
    class Program
    {
        delegate bool IsMissing(HtmlResult result, List<HtmlResult> results);
        delegate string GetFollowUpQuery(string query, HtmlResult result);
        static readonly DomainParser DomainParser = new DomainParser(new WebTldRuleProvider());

        static void Main(string[] args)
        {
            var data = File.ReadAllText("data.json");
            var dataAsDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(data);
            //MrSite(dataAsDictionary);
            //MrTitle(dataAsDictionary);
            //Console.WriteLine(MpReverseJd());
            Top1Absent(dataAsDictionary);
            Console.ReadKey();
        }

        static void MrTitle(Dictionary<string, List<string>> dataAsDictionary)
        {
            bool MrTitleMissingDelegate(HtmlResult htmlResult, List<HtmlResult> htmlResults)
            {
                return !htmlResults.Any(item => item.Url.Host == htmlResult.Url.Host && item.Title == htmlResult.Title);
            }

            string MrTitleGetFollowUpQueryDelegate(string query, HtmlResult htmlResult)
            {
                var followUpQuery = $"{query} {htmlResult.Title}";
                return followUpQuery;
            }

            Console.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
            //MRTitle (English Bing, English Query)
            var mrTitleEnglishBingEnglishQuery = CalculateRocof("en-us", dataAsDictionary["en-us"], MrTitleGetFollowUpQueryDelegate, MrTitleMissingDelegate);
            File.WriteAllText("mrTitleEnglishBingEnglishQuery.txt", $"MRSite (English Bing, English Query) = {mrTitleEnglishBingEnglishQuery}");
            Console.WriteLine($"MRTitle (English Bing, English Query) = {mrTitleEnglishBingEnglishQuery}");

            //MRTitle (English Bing, Chinese Query)
            var mrTitleEnglishBingChineseQuery = CalculateRocof("en-us", dataAsDictionary["zh-CN"],
                MrTitleGetFollowUpQueryDelegate, MrTitleMissingDelegate);
            File.WriteAllText("mrTitleEnglishBingChineseQuery.txt", $"MRSite (English Bing, Chinese Query) = {mrTitleEnglishBingChineseQuery}");
            Console.WriteLine($"MRTitle (English Bing, Chinese Query) = {mrTitleEnglishBingChineseQuery}");


            //MRTitle (Chinese Bing, English Query)
            var mrTitleChineseBingEnglishQuery = CalculateRocof("zh-CN", dataAsDictionary["en-us"], MrTitleGetFollowUpQueryDelegate, MrTitleMissingDelegate);
            File.WriteAllText("mrTitleChineseBingEnglishQuery.txt", $"MRSite (Chinese Bing, English Query) = {mrTitleChineseBingEnglishQuery}");
            Console.WriteLine($"MRTitle (Chinese Bing, English Query) = {mrTitleChineseBingEnglishQuery}");

            //MRTitle (Chinese Bing, Chinese Query)
            var mrTitleChineseBingChineseQuery = CalculateRocof("zh-CN", dataAsDictionary["zh-CN"],
                MrTitleGetFollowUpQueryDelegate, MrTitleMissingDelegate);
            File.WriteAllText("mrTitleChineseBingChineseQuery.txt", $"MRSite (Chinese Bing, Chinese Query) = {mrTitleChineseBingChineseQuery}");
            Console.WriteLine($"MRTitle (Chinese Bing, Chinese Query) = {mrTitleChineseBingChineseQuery}");
            Console.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
        }

        static void MrSite(Dictionary<string, List<string>> dataAsDictionary)
        {
            bool MrSiteMissingDelegate(HtmlResult htmlResult, List<HtmlResult> htmlResults)
            {
                return !htmlResults.Any(item => item.Url.Host == htmlResult.Url.Host && item.Title == htmlResult.Title);
            }

            string MrSiteGetFollowUpQueryDelegate(string query, HtmlResult htmlResult)
            {
                var domain = DomainParser.Get(htmlResult.Url.Host);
                var followUpQuery = $"{query} site:.{domain.TLD}";
                return followUpQuery;
            }

            Console.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
            //MRSite (English bing, English Query)
            var mrSiteEnglishbingEnglishQuery = CalculateRocof("en-us", dataAsDictionary["en-us"], MrSiteGetFollowUpQueryDelegate, MrSiteMissingDelegate);
            File.WriteAllText("mrSiteEnglishbingEnglishQuery.txt", $"MRSite (English bing, English Query) = {mrSiteEnglishbingEnglishQuery}");
            Console.WriteLine($"MRSite (English bing, English Query) = {mrSiteEnglishbingEnglishQuery}");

            //MRSite (English bing, Chinese Query)
            var mrSiteEnglishbingChineseQuery = CalculateRocof("en-us", dataAsDictionary["zh-CN"],
                MrSiteGetFollowUpQueryDelegate, MrSiteMissingDelegate);
            File.WriteAllText("mrSiteEnglishbingChineseQuery.txt", $"MRSite (English bing, Chinese Query) = {mrSiteEnglishbingChineseQuery}");
            Console.WriteLine($"MRSite (English bing, Chinese Query) = {mrSiteEnglishbingChineseQuery}");


            //MRSite (Chinese bing, English Query)
            var mrSiteChinesebingEnglishQuery = CalculateRocof("zh-CN", dataAsDictionary["en-us"], MrSiteGetFollowUpQueryDelegate, MrSiteMissingDelegate);
            File.WriteAllText("mrSiteChinesebingEnglishQuery.txt", $"MRSite (Chinese bing, English Query) = {mrSiteChinesebingEnglishQuery}");
            Console.WriteLine($"MRSite (Chinese bing, English Query) = {mrSiteChinesebingEnglishQuery}");

            //MRSite (Chinese bing, Chinese Query)
            var mrSiteChinesebingChineseQuery = CalculateRocof("zh-CN", dataAsDictionary["zh-CN"],
                MrSiteGetFollowUpQueryDelegate, MrSiteMissingDelegate);
            File.WriteAllText("mrSiteChinesebingChineseQuery.txt", $"MRSite (Chinese bing, Chinese Query) = {mrSiteChinesebingChineseQuery}");
            Console.WriteLine($"MRSite (Chinese bing, Chinese Query) = {mrSiteChinesebingChineseQuery}");
            Console.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
        }

        static double MpReverseJd()
        {
            var data = File.ReadAllText("hot.json");
            var queries = JsonConvert.DeserializeObject<List<string>>(data);
            var jdSum = 0.0;
            var googleSearchHelper = new GoogleSearchHelper("en-us");

            string GetOriginalQuery(string query)
            {
                var words = query.Split(' ').Select(q => $"\"{q}\"");
                return string.Join(" ", words);
            }

            string GetReversedQuery(string query)
            {
                var words = query.Split(' ').Reverse().Select(q => $"\"{q}\"");
                return string.Join(" ", words);
            }

            foreach (var query in queries)
            {
                var originaQueryResults = googleSearchHelper.GetHtmlResults(GetOriginalQuery(query));
                var reverseQueryResults = googleSearchHelper.GetHtmlResults(GetReversedQuery(query));

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
            var googleSearchHelper = new GoogleSearchHelper("en-us");

            string GetOriginalQuery(string query)
            {
                var words = query.Split(' ').Select(q => $"{q}");
                return string.Join(" ", words);
            }

            string GetReversedQuery(string query)
            {
                var words = query.Split(' ').Reverse().Select(q => $"{q}");
                return string.Join(" ", words);
            }

            foreach (var query in queries)
            {
                var originaQueryResults = googleSearchHelper.GetHtmlResults(GetOriginalQuery(query));
                var reverseQueryResults = googleSearchHelper.GetHtmlResults(GetReversedQuery(query));

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
            var googleSearchHelper = new GoogleSearchHelper("en-us");

            string GetOriginalQuery(string query)
            {
                var words = query.Split(' ').Select(q => $"{q}");
                return $"{string.Join(" ", words)} site:.com, site:.edu, site:.mil or site:.lc";
            }

            string GetReversedQuery(string query)
            {
                var words = query.Split(' ').Reverse().Select(q => $"{q}");
                return $"{string.Join(" ", words)} site:.com, site:.edu, site:.mil or site:.lc";
            }

            foreach (var query in queries)
            {
                var originaQueryResults = googleSearchHelper.GetHtmlResults(GetOriginalQuery(query));
                var reverseQueryResults = googleSearchHelper.GetHtmlResults(GetReversedQuery(query));

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

        static void Top1Absent(Dictionary<string, List<string>> dataAsDictionary)
        {
            bool MrSiteMissingDelegate(HtmlResult htmlResult, List<HtmlResult> htmlResults)
            {
                return !htmlResults.Any(item => item.Url.Host == htmlResult.Url.Host && item.Title == htmlResult.Title);
            }

            string MrSiteGetFollowUpQueryDelegate(string query, HtmlResult htmlResult)
            {
                var domain = DomainParser.Get(htmlResult.Url.Host);
                var followUpQuery = $"{query} site:.{domain.TLD}";
                return followUpQuery;
            }

            Console.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
            //MRSite (English bing, English Query)
            var top1AbsentEnglishbingEnglishQuery = CalculateRocof("en-us", dataAsDictionary["en-us"],
                MrSiteGetFollowUpQueryDelegate, MrSiteMissingDelegate, 1);
            File.WriteAllText("Top1AbsentEnglishbingEnglishQuery.txt", $"Top1Absent (English bing, English Query) = {top1AbsentEnglishbingEnglishQuery}");
            Console.WriteLine($"Top1Absent (English bing, English Query) = {top1AbsentEnglishbingEnglishQuery}");

            //MRSite (English bing, Chinese Query)
            var top1AbsentEnglishbingChineseQuery = CalculateRocof("en-us", dataAsDictionary["zh-CN"],
                MrSiteGetFollowUpQueryDelegate, MrSiteMissingDelegate, 1);
            File.WriteAllText("Top1AbsentEnglishbingChineseQuery.txt", $"Top1Absent (English bing, Chinese Query) = {top1AbsentEnglishbingChineseQuery}");
            Console.WriteLine($"Top1Absent (English bing, Chinese Query) = {top1AbsentEnglishbingChineseQuery}");


            //MRSite (Chinese bing, English Query)
            var top1AbsentChinesebingEnglishQuery = CalculateRocof("zh-CN", dataAsDictionary["en-us"],
                MrSiteGetFollowUpQueryDelegate, MrSiteMissingDelegate, 1);
            File.WriteAllText("Top1AbsentChinesebingEnglishQuery.txt", $"Top1Absent (Chinese bing, English Query) = {top1AbsentChinesebingEnglishQuery}");
            Console.WriteLine($"Top1Absent (Chinese bing, English Query) = {top1AbsentChinesebingEnglishQuery}");

            //MRSite (Chinese bing, Chinese Query)
            var top1AbsentChinesebingChineseQuery = CalculateRocof("zh-CN", dataAsDictionary["zh-CN"],
                MrSiteGetFollowUpQueryDelegate, MrSiteMissingDelegate, 1);
            File.WriteAllText("Top1AbsentChinesebingChineseQuery.txt", $"Top1Absent (Chinese bing, Chinese Query) = {top1AbsentChinesebingChineseQuery}");
            Console.WriteLine($"Top1Absent (Chinese bing, Chinese Query) = {top1AbsentChinesebingChineseQuery}");
            Console.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
        }

        static void Top5Absent(Dictionary<string, List<string>> dataAsDictionary)
        {
            bool MrSiteMissingDelegate(HtmlResult htmlResult, List<HtmlResult> htmlResults)
            {
                return !htmlResults.Any(item => item.Url.Host == htmlResult.Url.Host && item.Title == htmlResult.Title);
            }

            string MrSiteGetFollowUpQueryDelegate(string query, HtmlResult htmlResult)
            {
                var domain = DomainParser.Get(htmlResult.Url.Host);
                var followUpQuery = $"{query} site:.{domain.TLD}";
                return followUpQuery;
            }

            Console.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
            //Top5Absent (English bing, English Query)
            var top5AbsentEnglishbingEnglishQuery = CalculateRocof("en-us", dataAsDictionary["en-us"],
                MrSiteGetFollowUpQueryDelegate, MrSiteMissingDelegate, 5);
            File.WriteAllText("top5AbsentEnglishbingEnglishQuery.txt", $"Top5Absent (English bing, English Query) = {top5AbsentEnglishbingEnglishQuery}");
            Console.WriteLine($"Top5Absent (English bing, English Query) = {top5AbsentEnglishbingEnglishQuery}");

            //Top5Absent (English bing, Chinese Query)
            var top5AbsentEnglishbingChineseQuery = CalculateRocof("en-us", dataAsDictionary["zh-CN"],
                MrSiteGetFollowUpQueryDelegate, MrSiteMissingDelegate, 1);
            File.WriteAllText("top5AbsentEnglishbingChineseQuery.txt", $"Top5Absent (English bing, Chinese Query) = {top5AbsentEnglishbingChineseQuery}");
            Console.WriteLine($"Top5Absent (English bing, Chinese Query) = {top5AbsentEnglishbingChineseQuery}");


            //Top5Absent (Chinese bing, English Query)
            var top5AbsentChinesebingEnglishQuery = CalculateRocof("zh-CN", dataAsDictionary["en-us"],
                MrSiteGetFollowUpQueryDelegate, MrSiteMissingDelegate, 1);
            File.WriteAllText("top5AbsentChinesebingEnglishQuery.txt", $"Top5Absent (Chinese bing, English Query) = {top5AbsentChinesebingEnglishQuery}");
            Console.WriteLine($"Top5Absent (Chinese bing, English Query) = {top5AbsentChinesebingEnglishQuery}");

            //Top5Absent (Chinese bing, Chinese Query)
            var top5AbsentChinesebingChineseQuery = CalculateRocof("zh-CN", dataAsDictionary["zh-CN"],
                MrSiteGetFollowUpQueryDelegate, MrSiteMissingDelegate, 1);
            File.WriteAllText("top5AbsentChinesebingChineseQuery.txt", $"Top5Absent (Chinese bing, Chinese Query) = {top5AbsentChinesebingChineseQuery}");
            Console.WriteLine($"Top5Absent (Chinese bing, Chinese Query) = {top5AbsentChinesebingChineseQuery}");
            Console.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
        }

        static double CalculateRocof(string hl, List<string> queries, GetFollowUpQuery getFollowUpQuery,
            IsMissing isMissing, int top = 10)
        {
            var totalMrSite = 0;
            var googleSearchHelper = new GoogleSearchHelper(hl);
            var topCounter = 0;
            foreach (var query in queries)
            {
                var originalQueryResults = googleSearchHelper.GetHtmlResults(query)
                    .Where(m => m.Url.IsAbsoluteUri);
                foreach (var originalQueryResult in originalQueryResults)
                {
                    if (topCounter == top)
                    {
                        topCounter = 0;
                        break;
                    }

                    var followUpQueryResults =
                        googleSearchHelper.GetHtmlResults(getFollowUpQuery(query, originalQueryResult));

                    if (isMissing(originalQueryResult, followUpQueryResults))
                    {
                        totalMrSite++;
                    }
                    topCounter++;
                }
            }
            return (double)totalMrSite / (queries.Count * top);
        }
    }
}
