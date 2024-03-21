using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjectManager.Data;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ProjectManager_v00_DbContext _context;

        public ProjectsController(ProjectManager_v00_DbContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var projectManager_v00_DbContext = _context.Project.Include(p => p.Field);
            return View(await projectManager_v00_DbContext.ToListAsync());
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Project == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .Include(p => p.Field)
                .Include(j => j.Jobs)
                .FirstOrDefaultAsync(m => m.ProjectID == id);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            ViewData["FieldID"] = new SelectList(_context.Field, "FieldID", "FieldName");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectID,ProjectName,FieldID")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FieldID"] = new SelectList(_context.Field, "FieldID", "FieldID", project.FieldID);
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Project == null)
            {
                return NotFound();
            }

            var project = await _context.Project.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["FieldID"] = new SelectList(_context.Field.OrderBy(n=>n.FieldName), "FieldID", "FieldName", project.FieldID);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectID,ProjectName,FieldID")] Project project)
        {
            if (id != project.ProjectID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Projects", new { id = id });
            }
            ViewData["FieldID"] = new SelectList(_context.Field, "FieldID", "FieldID", project.FieldID);
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Project == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .Include(p => p.Field)
                .FirstOrDefaultAsync(m => m.ProjectID == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Project == null)
            {
                return Problem("Entity set 'ProjectManager_v00_DbContext.Project'  is null.");
            }
            var project = await _context.Project.FindAsync(id);
            if (project != null)
            {
                _context.Project.Remove(project);
            }
            var fieldId = project.FieldID;

			await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Fields", new { id = fieldId });
		}



		/* AddJob */

		// GET: Project/AddJob/5
		public async Task<IActionResult> AddJob(int? id)
		{
			if (id == null || _context.Project == null)
			{
				return NotFound();
			}

			var project = await _context.Project.FindAsync(id);

			if (project == null)
			{
				return NotFound();
			}

			return View(project);
		}

		// POST: Fields/AddJob/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddJob(string jobName, double timeEstimation, int id)
		{
			if (ModelState.IsValid)
			{
				Job newJob = new Job()
				{
					JobName = jobName,
					TimeEstimation = timeEstimation,
					ProjectID = id,
					Finished = false
				};

				_context.Add(newJob);
				await _context.SaveChangesAsync();
				return RedirectToAction("Details", "Projects", new { id = id });
			}
                var project = _context.Project.FindAsync(id);
			return View(project);
		}

		/*End of AddJob */









		private bool ProjectExists(int id)
        {
          return (_context.Project?.Any(e => e.ProjectID == id)).GetValueOrDefault();
        }
    }
}
