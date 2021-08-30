using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlainWiki.Models;

namespace PlainWiki.Services.Interfaces
{
    public interface IWikiPageService
    {
        IEnumerable<WikiPages> GetWikiPages();
        Task<WikiPages> GetWikiPage(int? id);

        Task SaveChangesAsync();
        void Add(WikiPages wikiPages);
        void Update(WikiPages wikiPages);
        void Remove(WikiPages wikiPages);
    }
}
