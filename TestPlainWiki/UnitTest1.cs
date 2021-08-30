using System;
using Xunit;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PlainWiki.Controllers;
using PlainWiki.Data;
using PlainWiki.Models;
using Xunit;
using Xunit.Abstractions;

namespace TestPlainWiki.Tests
{
    public class Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private WikiPageController _wikiPageController;
        public Tests(ITestOutputHelper testOutputHelper)
        {
            Assert.True(true);
            _testOutputHelper = testOutputHelper;
        }
        
        [Fact]
        public void TestWikiPagesModel()
        {
            var wikiPages = new WikiPages
            {
                ID = 166,
                Title = "TestString",
                RichText = "TestString"
            };
            var wikiPagesTitle = wikiPages.Title;
            Assert.NotNull(wikiPagesTitle);
        }

        [Fact]
        public async void TestWikiPageControllerCreate()
        {
            var wikiPages = new WikiPages
            {
                ID = 166,
                Title = "TestString",
                RichText = "TestString"
            };
            Assert.True(_wikiPageController.WikiPagesExists(166));
        }
        

    }
}