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
        private readonly ApplicationDataContext _context;
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
        public void TestWikiPageController()
        {
            _wikiPageController = new WikiPageController(_context);
            var result = _wikiPageController.Create() as ViewResult;
            _testOutputHelper.WriteLine(result.ToString());
            Assert.NotNull(result);
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