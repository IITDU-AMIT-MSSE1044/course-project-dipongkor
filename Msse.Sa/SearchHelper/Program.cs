using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nager.PublicSuffix;
using Newtonsoft.Json;

namespace SearchHelper
{
    namespace BingSearchApisQuickstart
    {

        class Program
        {

            static void Main(string[] args)
            {
                var data = File.ReadAllText("data.json");

                var dataAsDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(data);
                var googleSearchHelper = new GoogleSearchHelper();
                var domainParser = new DomainParser(new WebTldRuleProvider());
                var totalMRSite = 0;
                foreach (var query in dataAsDictionary["en"])
                {
                    var originalQueryResult = googleSearchHelper.GetSearchResults(query).GetAwaiter().GetResult();

                    foreach (var originalQueryResultItem in originalQueryResult.Items)
                    {
                        var domainName = domainParser.Get(originalQueryResultItem.DisplayLink);
                        var followUpQuery = $"{query} site:.{domainName.TLD}";

                        var followUpQueryResult = googleSearchHelper.GetSearchResults(followUpQuery).GetAwaiter().GetResult();

                        if (
                            !followUpQueryResult.Items.Any(
                                item =>
                                    item.Title == originalQueryResultItem.Title &&
                                    item.Link == originalQueryResultItem.Link))
                        {
                            totalMRSite++;
                        }
                    }
                }
                Console.WriteLine(totalMRSite);
                Console.ReadKey();
            }
        }
    }
}
