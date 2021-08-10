using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // GET: WikiPage
        public async Task<IActionResult> Index()
        {
            return View(await _context.WikiPages.ToListAsync());
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
            return View();
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

        private bool WikiPagesExists(int id)
        {
            return _context.WikiPages.Any(e => e.ID == id);
        }
    }
}
