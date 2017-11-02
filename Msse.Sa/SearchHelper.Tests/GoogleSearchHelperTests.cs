using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}