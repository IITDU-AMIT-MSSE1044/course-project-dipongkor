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
    }
}