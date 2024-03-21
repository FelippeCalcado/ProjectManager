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
    public class JobComponentsController : Controller
    {
        private readonly ProjectManager_v00_DbContext _context;

        public JobComponentsController(ProjectManager_v00_DbContext context)
        {
            _context = context;
        }

        // GET: JobComponents
        public async Task<IActionResult> Index()
        {
            var projectManager_v00_DbContext = _context.JobComponent.Include(j => j.Component).Include(j => j.Job);
            return View(await projectManager_v00_DbContext.ToListAsync());
        }

        // GET: JobComponents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.JobComponent == null)
            {
                return NotFound();
            }

            var jobComponent = await _context.JobComponent
                .Include(j => j.Component)
                .Include(j => j.Job)
                .FirstOrDefaultAsync(m => m.JobComponentID == id);
            if (jobComponent == null)
            {
                return NotFound();
            }

            return View(jobComponent);
        }

        // GET: JobComponents/Create
        public IActionResult Create()
        {
            ViewData["ComponentID"] = new SelectList(_context.Job, "JobID", "JobID");
            ViewData["JobID"] = new SelectList(_context.Job, "JobID", "JobID");
            return View();
        }

        // POST: JobComponents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobComponentID,JobID,ComponentID")] JobComponent jobComponent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobComponent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentID"] = new SelectList(_context.Job, "JobID", "JobID", jobComponent.ComponentID);
            ViewData["JobID"] = new SelectList(_context.Job, "JobID", "JobID", jobComponent.JobID);
            return View(jobComponent);
        }

        // GET: JobComponents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.JobComponent == null)
            {
                return NotFound();
            }

            var jobComponent = await _context.JobComponent.FindAsync(id);
            if (jobComponent == null)
            {
                return NotFound();
            }
            ViewData["ComponentID"] = new SelectList(_context.Job, "JobID", "JobID", jobComponent.ComponentID);
            ViewData["JobID"] = new SelectList(_context.Job, "JobID", "JobID", jobComponent.JobID);
            return View(jobComponent);
        }

        // POST: JobComponents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobComponentID,JobID,ComponentID")] JobComponent jobComponent)
        {
            if (id != jobComponent.JobComponentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobComponent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobComponentExists(jobComponent.JobComponentID))
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
            ViewData["ComponentID"] = new SelectList(_context.Job, "JobID", "JobID", jobComponent.ComponentID);
            ViewData["JobID"] = new SelectList(_context.Job, "JobID", "JobID", jobComponent.JobID);
            return View(jobComponent);
        }

        // GET: JobComponents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.JobComponent == null)
            {
                return NotFound();
            }

            var jobComponent = await _context.JobComponent
                .Include(j => j.Component)
                .Include(j => j.Job)
                .FirstOrDefaultAsync(m => m.JobComponentID == id);
            if (jobComponent == null)
            {
                return NotFound();
            }

            return View(jobComponent);
        }

        // POST: JobComponents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.JobComponent == null)
            {
                return Problem("Entity set 'ProjectManager_v00_DbContext.JobComponent'  is null.");
            }
            var jobComponent = await _context.JobComponent.FindAsync(id);
            if (jobComponent != null)
            {
                _context.JobComponent.Remove(jobComponent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobComponentExists(int id)
        {
          return (_context.JobComponent?.Any(e => e.JobComponentID == id)).GetValueOrDefault();
        }
    }
}
