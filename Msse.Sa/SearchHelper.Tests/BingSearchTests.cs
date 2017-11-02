using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SearchHelper.Tests
{
    [TestClass]
    public class BingSearchTests
    {
        [TestMethod]
        public async Task GetSearchResults()
        {
            var bingSearchHelper = new BingSearchHelper();
            var serchTerm = "lecture";
            var result = await bingSearchHelper.GetSearchResults(serchTerm);
            result.Should().NotBeNull();
            result.WebPages.Webpage.Length.Should().Be(20);
        }
    }
}
