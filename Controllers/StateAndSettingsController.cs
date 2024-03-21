using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Data;
using ProjectManager.JSON_Utils;
using ProjectManager.Models;
using ProjectManager.Interfaces;

namespace ProjectManager.Controllers
{
	public class StateAndSettingsController : Controller
	{
		private readonly ProjectManager_v00_DbContext _context;
		private readonly string JsonFile = "C:\\DEV2024\\ProjectManager_v00\\ProjectManager2\\JSON\\StateAndSettings.json";

		public StateAndSettingsController(ProjectManager_v00_DbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var modFromJason = JsonFunctions.ModReadJson(JsonFile);
			var jobs = _context.Job;
			var person = _context.Person;
			ViewBag.stateAndSections = modFromJason;
			ViewBag.ActiveJob = jobs.Where(p => p.JobID == modFromJason.ActiveJob).FirstOrDefault();
			ViewBag.ActivePerson = person.Where(p => p.PersonID == modFromJason.ActivePerson).FirstOrDefault();
			return View();
        }

        // GET: StateAndSettings/Create
        public IActionResult Create()
		{
            ViewBag.Jobs = new SelectList(_context.Job, "JobID", "JobName");

			ViewBag.People = new SelectList(_context.Person, "PersonID", "PersonID");
			return View();
        }

        // POST: StateAndSettings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActiveJob, ActivePerson")] StateAndSettings stateAndSettings)
		{
			if (ModelState.IsValid)
			{
				var js = JsonFile;
				JsonFunctions.ModSaveJson(JsonFile, stateAndSettings);
				return RedirectToAction(nameof(Index));
			}

			return View(stateAndSettings);
        }
    }
}
