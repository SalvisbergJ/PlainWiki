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

namespace PlainWiki.Controllers
{
    
    public class WikiPageController : Controller
    {
        private readonly ApplicationDataContext _context;

        public WikiPageController(ApplicationDataContext context)
        {
            _context = context;
        }

       

        // GET: WikiPage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wikiPages = await _context.WikiPages
                .FirstOrDefaultAsync(m => m.ID == id);
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
                _context.Add(wikiPages);
                await _context.SaveChangesAsync();
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

            var wikiPages = await _context.WikiPages.FindAsync(id);
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
                    _context.Update(wikiPages);
                    await _context.SaveChangesAsync();
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

            var wikiPages = await _context.WikiPages
                .FirstOrDefaultAsync(m => m.ID == id);
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
            var wikiPages = await _context.WikiPages.FindAsync(id);
            _context.WikiPages.Remove(wikiPages);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public bool WikiPagesExists(int id)
        {
            return _context.WikiPages.Any(e => e.ID == id);
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
        //    _context.ImagesList.Add(image);
        //    _context.SaveChanges();
        //    return Json($"<img src='/WikiPosts/GetFile/{image.Id}'></img>");
        //}

        //public FileResult GetFile(int id)
        //{
        //    var imageData = _context.ImagesList.FirstOrDefault(x => x.Id == id)?.ImageData;
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

            var wikiPages = await _context.WikiPages
                .FirstOrDefaultAsync(m => m.ID == id);
            if (wikiPages == null)
            {
                return NotFound();
            }

            return View(wikiPages);
        }
        public async Task<IActionResult> Index(string searchString)
        {
            var wikiPages = from m in _context.WikiPages
                select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                wikiPages = wikiPages.Where(s => s.Title.Contains(searchString));
            }

            return View(await wikiPages.ToListAsync());
        }
    }
}
