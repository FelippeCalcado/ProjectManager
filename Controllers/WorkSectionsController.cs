using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Data;
using ProjectManager.JSON_Utils;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    public class WorkSectionsController : Controller
    {
        private readonly ProjectManager_v00_DbContext _context;

        public WorkSectionsController(ProjectManager_v00_DbContext context)
        {
            _context = context;
        }

        // GET: WorkSections
        public async Task<IActionResult> Index()
        {
            var projectManager_v00_DbContext = _context.WorkSection.Include(w => w.Job).Include(w => w.Person);
            return View(await projectManager_v00_DbContext.ToListAsync());
        }

        // GET: WorkSections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WorkSection == null)
            {
                return NotFound();
            }

            var workSection = await _context.WorkSection
                .Include(w => w.Job)
                .Include(w => w.Person)
                .FirstOrDefaultAsync(m => m.WorkSectionID == id);

            if (workSection == null)
            {
                return NotFound();
            }

            return View(workSection);
        }

        // GET: WorkSections/Create
        public IActionResult Create()
        {
            ViewData["JobID"] = new SelectList(_context.Job, "JobID", "JobID");
            ViewData["PersonID"] = new SelectList(_context.Person, "PersonID", "PersonID");
            return View();
        }

        // POST: WorkSections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkSectionID,PersonID,JobID,Start,End")] WorkSection workSection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workSection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobID"] = new SelectList(_context.Job, "JobID", "JobID", workSection.JobID);
            ViewData["PersonID"] = new SelectList(_context.Person, "PersonID", "PersonID", workSection.PersonID);
            return View(workSection);
        }

        // GET: WorkSections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WorkSection == null)
            {
                return NotFound();
            }

            var workSection = await _context.WorkSection.FindAsync(id);
            if (workSection == null)
            {
                return NotFound();
            }
            ViewData["JobID"] = new SelectList(_context.Job, "JobID", "JobID", workSection.JobID);
            ViewData["PersonID"] = new SelectList(_context.Person, "PersonID", "PersonID", workSection.PersonID);
            return View(workSection);
        }

        // POST: WorkSections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkSectionID,PersonID,JobID,Start,End")] WorkSection workSection)
        {
            if (id != workSection.WorkSectionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workSection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkSectionExists(workSection.WorkSectionID))
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
            ViewData["JobID"] = new SelectList(_context.Job, "JobID", "JobID", workSection.JobID);
            ViewData["PersonID"] = new SelectList(_context.Person, "PersonID", "PersonID", workSection.PersonID);
            return View(workSection);
        }

        // GET: WorkSections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WorkSection == null)
            {
                return NotFound();
            }

            var workSection = await _context.WorkSection
                .Include(w => w.Job)
                .Include(w => w.Person)
                .FirstOrDefaultAsync(m => m.WorkSectionID == id);
            if (workSection == null)
            {
                return NotFound();
            }

            return View(workSection);
        }

        // POST: WorkSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WorkSection == null)
            {
                return Problem("Entity set 'ProjectManager_v00_DbContext.WorkSection'  is null.");
            }
            var workSection = await _context.WorkSection.FindAsync(id);
            if (workSection != null)
            {
                _context.WorkSection.Remove(workSection);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        // GET: WorkSections
        public async Task<IActionResult> IndexToday()
        {
            var WorkSections = _context.WorkSection.Include(w => w.Job).Include(w => w.Person);
            var today = DateTime.Today;
            var TodaySections = WorkSections.Where(d => d.Start >= today);

            Dictionary<int, TimeOnly> starts = new Dictionary<int, TimeOnly>();
            Dictionary<int, string> ends = new Dictionary<int, string>();
            foreach (var ws in WorkSections)
            {
                if (ws.Start != null)
                {
                    starts[ws.WorkSectionID] = TimeOnly.FromDateTime(ws.Start);

				}
				if (ws.End.HasValue)
				{
					ends[ws.WorkSectionID] = ws.End.ToString();

				}
			}
            
            ViewBag.ends = ends;
			return View(await TodaySections.ToListAsync());
        }




        private bool WorkSectionExists(int id)
        {
            return (_context.WorkSection?.Any(e => e.WorkSectionID == id)).GetValueOrDefault();
        }
    }
}
