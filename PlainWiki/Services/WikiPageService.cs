using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlainWiki.Data;
using PlainWiki.Models;
using PlainWiki.Services.Interfaces;

namespace PlainWiki.Services
{
    public class WikiPageService : IWikiPageService
    {
        
        private readonly ApplicationDataContext _context;

        public WikiPageService(ApplicationDataContext context)
        {
            _context = context;
        }
        public IEnumerable<WikiPages> GetWikiPages()
        {
            return _context.WikiPages;
        }

        public Task<WikiPages> GetWikiPage(int? id)
        {
            return _context.WikiPages.FirstOrDefaultAsync(m => m.ID == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Add(WikiPages wikiPages)
        {
            _context.Add(wikiPages);
        }

        public void Update(WikiPages wikiPages)
        {
            _context.Update(wikiPages);
        }

        public void Remove(WikiPages wikiPages)
        {
            _context.WikiPages.Remove(wikiPages);
        }
        public async Task<List<WikiPages>> SearchAsync(string searchString)
        {
            
            var wikiPages = from m in GetWikiPages() 
                            select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                wikiPages = wikiPages.Where(s => s.Title.Contains(searchString));
            }
            
            return wikiPages.ToList();
        }
    }
}
