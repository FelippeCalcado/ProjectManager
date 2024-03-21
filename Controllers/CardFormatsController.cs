using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Data;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    public class CardFormatsController : Controller
    {
        private readonly ProjectManager_v00_DbContext _context;

        public CardFormatsController(ProjectManager_v00_DbContext context)
        {
            _context = context;
        }

        // GET: CardFormats
        public async Task<IActionResult> Index()
        {
            var fields = _context.Field.ToList();
            Dictionary<int, Field> fieldDic = new Dictionary<int, Field>();
            foreach (var f in fields)
            {
                fieldDic[f.FieldID] = f;
            }
            ViewBag.Fields = fieldDic;
              return _context.CardFormat != null ? 
                          View(await _context.CardFormat.ToListAsync()) :
                          Problem("Entity set 'ProjectManager_v00_DbContext.CardFormat'  is null.");
        }

        // GET: CardFormats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CardFormat == null)
            {
                return NotFound();
            }

            var cardFormat = await _context.CardFormat
                .FirstOrDefaultAsync(m => m.CardFormatID == id);
            if (cardFormat == null)
            {
                return NotFound();
            }

            return View(cardFormat);
        }

        // GET: CardFormats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CardFormats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CardFormatID,ItemID,Color")] CardFormat cardFormat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cardFormat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cardFormat);
        }

        // GET: CardFormats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CardFormat == null)
            {
                return NotFound();
            }

            var cardFormat = await _context.CardFormat.FindAsync(id);
            if (cardFormat == null)
            {
                return NotFound();
            }
            return View(cardFormat);
        }

        // POST: CardFormats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CardFormatID,ItemID,Color")] CardFormat cardFormat)
        {
            if (id != cardFormat.CardFormatID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cardFormat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardFormatExists(cardFormat.CardFormatID))
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
            return View(cardFormat);
        }

        // GET: CardFormats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CardFormat == null)
            {
                return NotFound();
            }

            var cardFormat = await _context.CardFormat
                .FirstOrDefaultAsync(m => m.CardFormatID == id);
            if (cardFormat == null)
            {
                return NotFound();
            }

            return View(cardFormat);
        }

        // POST: CardFormats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CardFormat == null)
            {
                return Problem("Entity set 'ProjectManager_v00_DbContext.CardFormat'  is null.");
            }
            var cardFormat = await _context.CardFormat.FindAsync(id);
            if (cardFormat != null)
            {
                _context.CardFormat.Remove(cardFormat);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardFormatExists(int id)
        {
          return (_context.CardFormat?.Any(e => e.CardFormatID == id)).GetValueOrDefault();
        }
    }
}
