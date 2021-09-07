using System;
using Xunit;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PlainWiki.Controllers;
using PlainWiki.Data;
using PlainWiki.Models;
using PlainWiki.Services;
using PlainWiki.Services.Interfaces;
using Xunit;
using Xunit.Abstractions;

namespace TestPlainWiki
{
    public class Tests
    {
        private readonly IWikiPageService _wikiPageService;
        public Tests(IWikiPageService wikiPageService)
        {
            _wikiPageService = wikiPageService;
        }

        [Fact]
        public void ServiceHasPagesTest()
        {
            var wikiPages= _wikiPageService.GetWikiPages();
            Assert.NotNull(wikiPages);
        }

        [Fact]
        public void ServiceAddPageTest()
        {
            var wikiPages = new WikiPages
            {
                ID = 166,
                Title = "TestString",
                RichText = "TestString"
            };
            _wikiPageService.Add(wikiPages);
            Assert.NotNull(_wikiPageService.GetWikiPage(166));
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
    }
}