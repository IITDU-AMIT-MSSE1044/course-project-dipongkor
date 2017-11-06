using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nager.PublicSuffix;

namespace SearchHelper.Tests
{
    [TestClass]
    public class GoogleSearchHelperTests
    {
        [TestMethod]
        public async Task GoogleSearchHelperTest_GetSearchResults()
        {
            var googleSearchHelper = new GoogleSearchHelper();
            var serchTerm = "lecture";
            var result = await googleSearchHelper.GetSearchResults(serchTerm);
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void GoogleSearchHelperTest_DomainParser()
        {
            var domainParser = new DomainParser(new WebTldRuleProvider());
            var domainName = domainParser.Get("sub.test.co.uk");
            domainName.TLD.Should().Be("co.uk");
        }

        [TestMethod]
        public void GetSearchResult_Html()
        {
            var googleSearchHelper = new GoogleSearchHelper();
            var serchTerm = "lecture";
            var result =  googleSearchHelper.GetHtmlResult(serchTerm);
            var cites = result.DocumentNode.Descendants("h3")
                .Select(m => new {Tile = m.FirstChild.InnerText, Url = m.FirstChild.Attributes["href"] });

            foreach (var cite in cites)
            {
                var uri = cite.Url.Value.Substring(cite.Url.Value.IndexOf("=") + 1, cite.Url.Value.IndexOf("&"));
                Console.WriteLine(uri);
            }
            result.Should().NotBe(null);
        }

    }
}