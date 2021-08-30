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
        //private readonly ApplicationDataContext _wikiPageService;
        private readonly IWikiPageService _wikiPageService;

        public WikiPageController(IWikiPageService wikiPageService)
        {
            _wikiPageService = wikiPageService;
        }

       

        // GET: WikiPage/Details/5
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

        // GET: WikiPage/Create
        public IActionResult Create()
        {
            return View("Create");
        }

        // POST: WikiPage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: WikiPage/Edit/5
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

        // POST: WikiPage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: WikiPage/Delete/5
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

        // POST: WikiPage/Delete/5
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

        //[HttpPost]
        //public JsonResult UploadFile(IFormFile file)
        //{
        //    using var ms = new MemoryStream();
        //    if (file == null) return Json("");
        //    file.CopyTo(ms);
        //    var fileBytes = ms.ToArray();
        //    var image = new Images
        //    {
        //        ImageData = fileBytes,
        //    };
        //    _wikiPageService.ImagesList.Add(image);
        //    _wikiPageService.SaveChanges();
        //    return Json($"<img src='/WikiPosts/GetFile/{image.Id}'></img>");
        //}

        //public FileResult GetFile(int id)
        //{
        //    var imageData = _wikiPageService.ImagesList.FirstOrDefault(x => x.Id == id)?.ImageData;
        //    if (imageData == null) return null;
        //    var ms = new MemoryStream(imageData);
        //    return File(ms, "image/jpeg");
        //}

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
            var wikiPages = from m in _wikiPageService.GetWikiPages()
                select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                wikiPages = wikiPages.Where(s => s.Title.Contains(searchString));
            }

            return View(wikiPages);
        }
    }
}
