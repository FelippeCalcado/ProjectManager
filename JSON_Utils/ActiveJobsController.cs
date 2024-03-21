using System;
using System.Collections.Generic;
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
    public class ActiveJobsController : Controller
    {
        private readonly ProjectManager_v00_DbContext _context;

        public ActiveJobsController(ProjectManager_v00_DbContext context)
        {
            _context = context;
        }
        /*
        // GET: ActiveJobs
        public async Task<IActionResult> Index()
        {
            List<Job> listJob = new List<Job>();
            var jobTst = _context.Job.Where(j => j.JobID == 1).FirstOrDefault();
            listJob.Add(jobTst);
            JsonFunctions.SaveActiveJobList(listJob);
            var list = JsonFunctions.ReadActiveJobList();

            if (list == null)
            {
                jobTst = _context.Job.Where(j => j.JobID == 1).FirstOrDefault();
                listJob.Add(jobTst);
                JsonFunctions.SaveActiveJobList(listJob);
            }
            list = JsonFunctions.ReadActiveJobList();
            return View(list);
        }
        */



        /*
        // GET: ActiveJobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ActiveJobs == null)
            {
                return NotFound();
            }

            var activeJobs = await _context.ActiveJobs
                .FirstOrDefaultAsync(m => m.ActiveJobsID == id);
            if (activeJobs == null)
            {
                return NotFound();
            }

            return View(activeJobs);
        }

        // GET: ActiveJobs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ActiveJobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActiveJobsID")] ActiveJobs activeJobs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(activeJobs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(activeJobs);
        }

        // GET: ActiveJobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ActiveJobs == null)
            {
                return NotFound();
            }

            var activeJobs = await _context.ActiveJobs.FindAsync(id);
            if (activeJobs == null)
            {
                return NotFound();
            }
            return View(activeJobs);
        }

        // POST: ActiveJobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActiveJobsID")] ActiveJobs activeJobs)
        {
            if (id != activeJobs.ActiveJobsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activeJobs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActiveJobsExists(activeJobs.ActiveJobsID))
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
            return View(activeJobs);
        }

        // GET: ActiveJobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ActiveJobs == null)
            {
                return NotFound();
            }

            var activeJobs = await _context.ActiveJobs
                .FirstOrDefaultAsync(m => m.ActiveJobsID == id);
            if (activeJobs == null)
            {
                return NotFound();
            }

            return View(activeJobs);
        }

        // POST: ActiveJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ActiveJobs == null)
            {
                return Problem("Entity set 'ProjectManager_v00_DbContext.ActiveJobs'  is null.");
            }
            var activeJobs = await _context.ActiveJobs.FindAsync(id);
            if (activeJobs != null)
            {
                _context.ActiveJobs.Remove(activeJobs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActiveJobsExists(int id)
        {
          return (_context.ActiveJobs?.Any(e => e.ActiveJobsID == id)).GetValueOrDefault();
        }
        */
    }
}
