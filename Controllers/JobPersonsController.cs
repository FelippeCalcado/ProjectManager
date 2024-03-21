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
    public class JobPersonsController : Controller
    {
        private readonly ProjectManager_v00_DbContext _context;

        public JobPersonsController(ProjectManager_v00_DbContext context)
        {
            _context = context;
        }

        // GET: JobPersons
        public async Task<IActionResult> Index()
        {
            var projectManager_v00_DbContext = _context.JobPerson.Include(j => j.Job).Include(j => j.Person);
            return View(await projectManager_v00_DbContext.ToListAsync());
        }

        // GET: JobPersons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.JobPerson == null)
            {
                return NotFound();
            }

            var jobPerson = await _context.JobPerson
                .Include(j => j.Job)
                .Include(j => j.Person)
                .FirstOrDefaultAsync(m => m.JobPersonID == id);
            if (jobPerson == null)
            {
                return NotFound();
            }

            return View(jobPerson);
        }

        // GET: JobPersons/Create
        public IActionResult Create()
        {
            ViewData["JobID"] = new SelectList(_context.Job, "JobID", "JobID");
            ViewData["PersonID"] = new SelectList(_context.Person, "PersonID", "PersonID");
            return View();
        }

        // POST: JobPersons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobPersonID,JobID,PersonID")] JobPerson jobPerson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobPerson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobID"] = new SelectList(_context.Job, "JobID", "JobID", jobPerson.JobID);
            ViewData["PersonID"] = new SelectList(_context.Person, "PersonID", "PersonID", jobPerson.PersonID);
            return View(jobPerson);
        }

        // GET: JobPersons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.JobPerson == null)
            {
                return NotFound();
            }

            var jobPerson = await _context.JobPerson.FindAsync(id);
            if (jobPerson == null)
            {
                return NotFound();
            }
            ViewData["JobID"] = new SelectList(_context.Job, "JobID", "JobID", jobPerson.JobID);
            ViewData["PersonID"] = new SelectList(_context.Person, "PersonID", "PersonID", jobPerson.PersonID);
            return View(jobPerson);
        }

        // POST: JobPersons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobPersonID,JobID,PersonID")] JobPerson jobPerson)
        {
            if (id != jobPerson.JobPersonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobPerson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobPersonExists(jobPerson.JobPersonID))
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
            ViewData["JobID"] = new SelectList(_context.Job, "JobID", "JobID", jobPerson.JobID);
            ViewData["PersonID"] = new SelectList(_context.Person, "PersonID", "PersonID", jobPerson.PersonID);
            return View(jobPerson);
        }

        // GET: JobPersons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.JobPerson == null)
            {
                return NotFound();
            }

            var jobPerson = await _context.JobPerson
                .Include(j => j.Job)
                .Include(j => j.Person)
                .FirstOrDefaultAsync(m => m.JobPersonID == id);
            if (jobPerson == null)
            {
                return NotFound();
            }

            return View(jobPerson);
        }

        // POST: JobPersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.JobPerson == null)
            {
                return Problem("Entity set 'ProjectManager_v00_DbContext.JobPerson'  is null.");
            }
            var jobPerson = await _context.JobPerson.FindAsync(id);
            if (jobPerson != null)
            {
                _context.JobPerson.Remove(jobPerson);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobPersonExists(int id)
        {
          return (_context.JobPerson?.Any(e => e.JobPersonID == id)).GetValueOrDefault();
        }
    }
}
