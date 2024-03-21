using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using ProjectManager.Data;
using ProjectManager.JSON_Utils;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    public class FieldsController : Controller
    {
        private readonly ProjectManager_v00_DbContext _context;

        public FieldsController(ProjectManager_v00_DbContext context)
        {
            _context = context;
        }

        // GET: Fields
        public async Task<IActionResult> Index()
        {
            Dictionary<int,string> fieldsColor = new Dictionary<int,string>();
            foreach(var cf in _context.CardFormat)
            {
                fieldsColor[cf.ItemID] = cf.Color;
            }
            ViewData["CardFormats"] = fieldsColor;
            ViewBag.CardFormats = fieldsColor;

            string settingsFile = "C:\\DEV2024\\ProjectManager_v00\\ProjectManager2\\JSON\\Settings.json";
            var jtst = JsonFunctions.ReadJsonFile(settingsFile)["ActiveJobID"];
            var fd = _context.Field.Find(Convert.ToInt32(jtst));

            ViewBag.tstjson = fd.Projects;
            ViewBag.settingsFile = settingsFile;

            return _context.Field != null ? 
                          View(await _context.Field.Include(p => p.Projects).OrderBy(n => n.FieldName).ToListAsync()) :
                          Problem("Entity set 'ProjectManager_v00_DbContext.Field'  is null.");
        }

        // GET: Fields/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Field == null)
            {
                return NotFound();
            }

            var @field = await _context.Field
                .Include(p => p.Projects)
                .FirstOrDefaultAsync(m => m.FieldID == id);

            if (@field == null)
            {
                return NotFound();
            }

            List<Project> projectList = new List<Project>();
            foreach(Project proj in @field.Projects)
            {
                projectList.Add(proj);
            }
            /*ViewBag.projects = projectList.OrderBy(p=>p.ProjectName).ToList();*/

            return View(@field);
        }

        // GET: Fields/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fields/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FieldID,FieldName")] Field @field)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@field);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@field);
        }

        // GET: Fields/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Field == null)
            {
                return NotFound();
            }

            var @field = await _context.Field
                .Include(p => p.Projects)
                .Where(i => i.FieldID == id)
				.FirstOrDefaultAsync();

            if (@field == null)
            {
                return NotFound();
            }
            ViewData["CardFormats"] = new SelectList(_context.CardFormat, "CardFormatID", "CardFormatID", @field.CardFormatID);
            return View(@field);
        }

        // POST: Fields/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FieldID,FieldName,CardFormatID")] Field @field)
        {
            if (id != @field.FieldID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    @field.CardFormat = await _context.CardFormat.FirstOrDefaultAsync(m => field.CardFormatID == @field.CardFormatID);
                    _context.Update(@field);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FieldExists(@field.FieldID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Fields", new { id = id });
            }
            ViewData["CardFormats"] = new SelectList(_context.CardFormat, "CardFormatID", "CardFormatID", @field.CardFormatID);
            return View(@field);
        }

        // GET: Fields/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Field == null)
            {
                return NotFound();
            }

            var @field = await _context.Field
                .Include(p => p.Projects)
                .FirstOrDefaultAsync(m => m.FieldID == id);
            if (@field == null)
            {
                return NotFound();
            }

            return View(@field);
        }

        // POST: Fields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Field == null)
            {
                return Problem("Entity set 'ProjectManager_v00_DbContext.Field'  is null.");
            }
            var @field = await _context.Field.FindAsync(id);
            if (@field != null)
            {
                _context.Field.Remove(@field);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }





		// GET: Fields/AddProject/5
		public async Task<IActionResult> AddProject(int? id)
		{
			if (id == null || _context.Field == null)
			{
				return NotFound();
			}

			var field = await _context.Field.FindAsync(id);

			if (field == null)
			{
				return NotFound();
			}
			
			return View(field);
		}

		// POST: Fields/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddProject(string projectName, int id)
		{
			if (ModelState.IsValid)
			{
                Project newProject = new Project()
                {
                    ProjectName = projectName,
                    FieldID = id
                };
				_context.Add(newProject);
				await _context.SaveChangesAsync();
			}
				return RedirectToAction("Details", "Fields", new { id = id });
		}










		private bool FieldExists(int id)
        {
          return (_context.Field?.Any(e => e.FieldID == id)).GetValueOrDefault();
        }
    }
}
