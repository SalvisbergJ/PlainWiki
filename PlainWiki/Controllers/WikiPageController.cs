using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlainWiki.Data;
using PlainWiki.Models;
using PlainWiki.Services.Interfaces;

namespace PlainWiki.Controllers
{
    
    public class WikiPageController : Controller
    {
        private readonly IWikiPageService _wikiPageService;

        public WikiPageController(IWikiPageService wikiPageService)
        {
            _wikiPageService = wikiPageService;
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wikiPages = await _wikiPageService.GetWikiPage(id);
            if (wikiPages == null)
            {
                return NotFound();
            }

            return View(wikiPages);
        }

        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,RichText")] WikiPages wikiPages)
        {
            if (ModelState.IsValid)
            {
                _wikiPageService.Add(wikiPages);
                await _wikiPageService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wikiPages);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wikiPages = await _wikiPageService.GetWikiPage(id);
            if (wikiPages == null)
            {
                return NotFound();
            }
            return View(wikiPages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,RichText")] WikiPages wikiPages)
        {
            if (id != wikiPages.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _wikiPageService.Update(wikiPages);
                    await _wikiPageService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WikiPagesExists(wikiPages.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(wikiPages);
        }

        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wikiPages = await _wikiPageService.GetWikiPage(id);
            if (wikiPages == null)
            {
                return NotFound();
            }

            return View(wikiPages);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wikiPages = await _wikiPageService.GetWikiPage(id);
            _wikiPageService.Remove(wikiPages);
            await _wikiPageService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public bool WikiPagesExists(int id)
        {
            return _wikiPageService.GetWikiPage(id) != null;
        }

        public async Task<IActionResult> Open(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wikiPages = await _wikiPageService.GetWikiPage(id);
            if (wikiPages == null)
            {
                return NotFound();
            }

            return View(wikiPages);
        }
        public async Task<IActionResult> Index(string searchString)
        {
            var wikiPages = _wikiPageService.Search(searchString);
            return View(wikiPages);
        }
    }
}
