using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nager.PublicSuffix;
using SearchHelper.Models.Google;

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
            var googleSearchHelper = new GoogleSearchHelper("en-us");
            var serchTerm = "lecture";
            var result =  googleSearchHelper.GetHtmlResult(serchTerm);
            //var cites = result.DocumentNode.Descendants("h3")
            //    .Select(m =>  new HtmlResult
            //    {
            //        Title = m.FirstChild.InnerText,
            //        Url = new Uri(m.FirstChild.Attributes["href"].Value.Substring(m.FirstChild.Attributes["href"].Value.IndexOf("=") + 1, m.FirstChild.Attributes["href"].Value.IndexOf("&amp;sa"))
            //    });

            result.Should().Should().NotBe(null);
        }

    }
}