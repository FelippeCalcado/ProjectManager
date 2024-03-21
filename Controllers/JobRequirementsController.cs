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
    public class JobRequirementsController : Controller
    {
        private readonly ProjectManager_v00_DbContext _context;

        public JobRequirementsController(ProjectManager_v00_DbContext context)
        {
            _context = context;
        }

        // GET: JobRequirements
        public async Task<IActionResult> Index()
        {
            var projectManager_v00_DbContext = _context.JobRequirement.Include(j => j.Job).Include(j => j.Requirement);
            return View(await projectManager_v00_DbContext.ToListAsync());
        }

        // GET: JobRequirements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.JobRequirement == null)
            {
                return NotFound();
            }

            var jobRequirement = await _context.JobRequirement
                .Include(j => j.Job)
                .Include(j => j.Requirement)
                .FirstOrDefaultAsync(m => m.JobRequirementID == id);

            if (jobRequirement == null)
            {
                return NotFound();
            }

            return View(jobRequirement);
        }

        // GET: JobRequirements/Create
        public IActionResult Create(int? id)
        {
            ViewData["JobID"] = new SelectList(_context.Job, "JobID", "JobID");
            return View();
        }

        // POST: JobRequirements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int RequirementID, int id)
        {
            if (ModelState.IsValid)
            {
                var jobRequirement = new JobRequirement
                {
                    JobID = id,
                    RequirementID = RequirementID
                };

                _context.Add(jobRequirement);
                await _context.SaveChangesAsync();
				return RedirectToAction("AddRequirements", "Jobs", new { id });
			}
			var job = _context.Job.FindAsync(id);

			return RedirectToAction("AddRequirements", "Jobs", new { id });
		}

        // GET: JobRequirements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.JobRequirement == null)
            {
                return NotFound();
            }

            var jobRequirement = await _context.JobRequirement.FindAsync(id);
            if (jobRequirement == null)
            {
                return NotFound();
            }
            ViewData["JobID"] = new SelectList(_context.Job, "JobID", "JobID", jobRequirement.JobID);
            ViewData["RequirementID"] = new SelectList(_context.Job, "JobID", "JobID", jobRequirement.RequirementID);
            return View(jobRequirement);
        }

        // POST: JobRequirements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobRequirementID,JobID,RequirementID")] JobRequirement jobRequirement)
        {
            if (id != jobRequirement.JobRequirementID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobRequirement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobRequirementExists(jobRequirement.JobRequirementID))
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
            ViewData["JobID"] = new SelectList(_context.Job, "JobID", "JobID", jobRequirement.JobID);
            ViewData["RequirementID"] = new SelectList(_context.Job, "JobID", "JobID", jobRequirement.RequirementID);
            return View(jobRequirement);
        }

        // GET: JobRequirements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.JobRequirement == null)
            {
                return NotFound();
            }

            var jobRequirement = await _context.JobRequirement
                .Include(j => j.Job)
                .Include(j => j.Requirement)
                .FirstOrDefaultAsync(m => m.JobRequirementID == id);

            if (jobRequirement == null)
            {
                return NotFound();
            }

            return View(jobRequirement);
        }

        // POST: JobRequirements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.JobRequirement == null)
            {
                return Problem("Entity set 'ProjectManager_v00_DbContext.JobRequirement'  is null.");
            }
            var jobRequirement = await _context.JobRequirement.FindAsync(id);
            int jobId = jobRequirement.JobID;

			if (jobRequirement != null)
            {
                _context.JobRequirement.Remove(jobRequirement);
            }
            
            await _context.SaveChangesAsync();
            id = jobId;

			return RedirectToAction("AddRequirements", "Jobs", new { id });

		}

        private bool JobRequirementExists(int id)
        {
          return (_context.JobRequirement?.Any(e => e.JobRequirementID == id)).GetValueOrDefault();
        }
    }
}
