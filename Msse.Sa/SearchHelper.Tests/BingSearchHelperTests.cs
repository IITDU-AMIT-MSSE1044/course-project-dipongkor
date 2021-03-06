﻿using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SearchHelper.Tests
{
    [TestClass]
    public class BingSearchHelperTests
    {
        [TestMethod]
        public async Task BingSearchHelperTest_GetSearchResults()
        {
            var bingSearchHelper = new BingSearchHelper();
            var serchTerm = "lecture";
            var result = await bingSearchHelper.GetSearchResults(serchTerm);
            result.Should().NotBeNull();
            result.WebPages.Webpage.Length.Should().Be(20);
        }
    }
}
