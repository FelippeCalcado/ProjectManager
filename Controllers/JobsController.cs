using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using NuGet.Versioning;
using ProjectManager.Data;
using ProjectManager.Interfaces;
using ProjectManager.JSON_Utils;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
	public class JobsController : Controller
	{
		private readonly ProjectManager_v00_DbContext _context;

		public JobsController(ProjectManager_v00_DbContext context)
		{
			_context = context;
		}

		// GET: Jobs
		public async Task<IActionResult> Index()
		{
			var activeJsonFile = "C:\\DEV2024\\ProjectManager_v00\\ProjectManager2\\JSON\\ActiveJob.json";

			int activeJobID = Convert.ToInt16(JsonFunctions.ReadJsonFile(activeJsonFile)["active"]);
			var actJob = _context.Job.Select(i => i.JobID == activeJobID);
			ViewBag.activeJob = activeJobID;

			var projectManager_v00_DbContext = _context.Job.Include(j => j.Project);
			return View(await projectManager_v00_DbContext.ToListAsync());
		}

		// GET: Jobs/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Job == null)
			{
				return NotFound();
			}

			var jobs = await _context.Job
				.Include(j => j.Project)
				.ToListAsync();

			var job = await _context.Job
				.Include(j => j.Project)
				.FirstOrDefaultAsync(m => m.JobID == id);

			if (job == null)
			{
				return NotFound();
			}

			Dictionary<int, string> jobReq = new Dictionary<int, string>();

			var requirements = _context.JobRequirement.Where(i => i.JobID == job.JobID);

			foreach (var r in requirements)
			{
				var j = jobs.Where(i => i.JobID == r.RequirementID).FirstOrDefault();
				var jobName = j.JobName;
				jobReq[j.JobID] = jobName;
			}
			ViewBag.requirements = requirements;
			ViewBag.reqNames = jobReq;

			var components = _context.JobComponent.Where(i => i.JobID == job.JobID).Include(j => j.Job);

			foreach (var c in components)
			{
				var j = jobs.Where(i => i.JobID == c.ComponentID).FirstOrDefault();
				var jobName = j.JobName;
				jobReq[j.JobID] = jobName;
			}
			ViewBag.components = components;

			return View(job);
		}

		// GET: Jobs/Create
		public IActionResult Create()
		{
			ViewData["ProjectID"] = new SelectList(_context.Project, "ProjectID", "ProjectID");
			return View();
		}

		// POST: Jobs/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("JobID,JobName,TimeEstimation,ProjectID")] Job job)
		{
			if (ModelState.IsValid)
			{
				_context.Add(job);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(job);
		}

		// GET: Jobs/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Job == null)
			{
				return NotFound();
			}

			var job = await _context.Job.FindAsync(id);
			if (job == null)
			{
				return NotFound();
			}
			ViewData["ProjectID"] = new SelectList(_context.Project, "ProjectID", "ProjectID", job.ProjectID);
			return View(job);
		}

		// POST: Jobs/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("JobID,JobName,TimeEstimation,ProjectID,Finished")] Job job)
		{
			if (id != job.JobID)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(job);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!JobExists(job.JobID))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
                }
                return RedirectToAction("Details", "Jobs", new { id });
            }
			return View(job);
		}

		// GET: Jobs/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Job == null)
			{
				return NotFound();
			}

			var job = await _context.Job
				.Include(j => j.Project)
				.FirstOrDefaultAsync(m => m.JobID == id);
			if (job == null)
			{
				return NotFound();
			}

			return View(job);
		}

		// POST: Jobs/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Job == null)
			{
				return Problem("Entity set 'ProjectManager_v00_DbContext.Job'  is null.");
			}
			var job = await _context.Job.FindAsync(id);
			int ProjectID = job.ProjectID;
			if (job != null)
			{
				_context.Job.Remove(job);
			}
			id = ProjectID;
			await _context.SaveChangesAsync();
			return RedirectToAction("Details", "Projects", new { id });
		}

		/* AddRequirements */


		// GET: Jobs/AddRequirements/5
		public async Task<IActionResult> AddRequirements(int? id)
		{
			if (id == null || _context.Job == null)
			{
				return NotFound();
			}

			var job = await _context.Job
				.FindAsync(id);

			var jobReq = await _context.JobRequirement
				.Where(i => i.JobID == job.JobID)
				.ToListAsync();


			var allJobs = await _context.Job
				.Where(i => i.JobID != job.JobID)
				.ToListAsync();

			HashSet<Job> reqOptions = new HashSet<Job>();
			foreach (var j in allJobs)
			{
				reqOptions.Add(j);
			};

			foreach (var j in reqOptions)
			{
				foreach (var i in jobReq)
				{
					if (i.RequirementID == j.JobID)
					{
						reqOptions.Remove(j);
					}

				}
			}



			if (job == null || reqOptions == null)
			{
				return NotFound();
			}

			List<JobRequirement> reqList = new List<JobRequirement>();
			List<JobRequirement> nonList = new List<JobRequirement>();

			foreach (var r in job.JobRequirements)
			{
				reqList.Add(r);
			};

			ViewBag.Requirements = reqList;

			foreach (var r in job.JobRequirements)
			{
				reqList.Add(r);
			};

			ViewBag.nonRequirements = reqOptions;

			return View(job);
		}

		// POST: Jobs/AddRequirements/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddRequirements(int id, int RequirementID)
		{
			if (ModelState.IsValid)
			{
				Job jReq = await _context.Job.FindAsync(RequirementID);
				JobRequirement req = new JobRequirement()
				{
					JobID = id,
					RequirementID = RequirementID
				};
				_context.JobRequirement.Add(req);

				await _context.SaveChangesAsync();
			}
			return RedirectToAction("AddRequirements", "Jobs", new { id });
		}

		/* End AddRequirements */
		/* Remove Requirements */


		// GET: Jobs/AddRequirements/5
		public async Task<IActionResult> RemoveRequirements(int? id)
		{
			if (id == null || _context.Job == null)
			{
				return NotFound();
			}

			var job = await _context.Job.FindAsync(id);

			var jobRequirements = job.JobRequirements;



			return View(job);
		}



		// POST: Jobs/RemoveRequirements/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> RemoveRequirements(int id, int JobRequirementID)
		{

			if (ModelState.IsValid)
			{
				JobRequirement jobReqToRemove = await _context.JobRequirement.FindAsync(JobRequirementID);


				//job.JobRequirements.Remove(jobReqToRemove);
				_context.JobRequirement.Remove(jobReqToRemove);

				await _context.SaveChangesAsync();
			}
			return RedirectToAction("AddRequirements", "Jobs", new { id });
		}
		/* End AddRequirements */
		/* AddComponents */


		// GET: Jobs/AddRequirements/5
		public async Task<IActionResult> AddComponents(int? id)
		{
			if (id == null || _context.Job == null)
			{
				return NotFound();
			}

			var job = await _context.Job
				.FindAsync(id);

			var jobReq = await _context.JobComponent
				.Where(i => i.JobID == job.JobID)
				.ToListAsync();


			var allJobs = await _context.Job
				.Where(i => i.JobID != job.JobID)
				.ToListAsync();

			HashSet<Job> comOptions = new HashSet<Job>();
			foreach (var j in allJobs)
			{
				comOptions.Add(j);
			};

			foreach (var j in comOptions)
			{
				foreach (var i in jobReq)
				{
					if (i.ComponentID == j.JobID)
					{
						comOptions.Remove(j);
					}

				}
			}



			if (job == null || comOptions == null)
			{
				return NotFound();
			}

			List<JobComponent> comList = new List<JobComponent>();
			List<JobComponent> nonList = new List<JobComponent>();

			foreach (var r in job.JobComponents)
			{
				comList.Add(r);
			};

			ViewBag.Components = comList;

			foreach (var r in job.JobComponents)
			{
				comList.Add(r);
			};

			ViewBag.nonComponents = comOptions;

			return View(job);
		}

		// POST: Jobs/AddRequirements/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddComponents(int id, int componentID)
		{
			if (ModelState.IsValid)
			{
				Job jcom = await _context.Job.FindAsync(componentID);
				JobComponent com = new JobComponent()
				{
					JobID = id,
					ComponentID = componentID
				};
				_context.JobComponent.Add(com);

				await _context.SaveChangesAsync();
			}
			return RedirectToAction("AddComponents", "Jobs", new { id });
		}

		/* End AddRequirements */
		/* Remove Requirements */


		// GET: Jobs/AddRequirements/5
		public async Task<IActionResult> RemoveComponents(int? id)
		{
			if (id == null || _context.Job == null)
			{
				return NotFound();
			}

			var job = await _context.Job.FindAsync(id);

			var jobComponents = job.JobComponents;



			return View(job);
		}



		// POST: Jobs/RemoveRequirements/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> RemoveComponents(int id, int JobComponentID)
		{

			if (ModelState.IsValid)
			{
				JobComponent jobComToRemove = await _context.JobComponent.FindAsync(JobComponentID);


				_context.JobComponent.Remove(jobComToRemove);

				await _context.SaveChangesAsync();
			}
			return RedirectToAction("AddComponents", "Jobs", new { id });
		}
		/* End AddRequirements */

		/* Finished */

		// GET: Jobs/AddRequirements/5
		public async Task<IActionResult> Finished(int? id)
		{
			if (id == null || _context.Job == null)
			{
				return NotFound();
			}

			var job = await _context.Job.FindAsync(id);
			ViewBag.tst1 = "tst1";

			return View(job);
		}



		// POST: Jobs/RemoveRequirements/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Finished(int id, bool finished)
		{
			var job = await _context.Job.FindAsync(id);

			if (ModelState.IsValid)
			{
				if(job.Finished == true)
				{
					job.Finished = false;
				}
				else
				{
					job.Finished = true;
				}
				await _context.SaveChangesAsync();
				
			}
			id = job.ProjectID;
			return RedirectToAction("Details", "Projects", new { id });
		}

		/* End Finished */







		/*Activate*/

		// GET: WorkSections/Activate/5
		public async Task<IActionResult> Activate(int? id)
		{
			var activeJsonFile = "C:\\DEV2024\\ProjectManager_v00\\ProjectManager2\\JSON\\ActiveJob.json";
			if (id == null || _context.Job == null)
			{
				return NotFound();
			}

			string activeJobJson;

			if (!JsonFunctions.ExistJsonFile(activeJsonFile))
			{
				JsonFunctions.CreateJsonFile(activeJsonFile);
				activeJobJson = JsonFunctions.ReadJsonFile(activeJsonFile)["active"];
			}
			else
			{

				activeJobJson = JsonFunctions.ReadJsonFile(activeJsonFile)["active"];
			}
			var activePerson = 3;

			var PreviousWS = await _context.WorkSection
				.Include(w => w.Job)
				.Include(w => w.Person)
				.OrderByDescending(h => h.Start)
				.FirstOrDefaultAsync();

			PreviousWS.End = DateTime.Now;
			JsonFunctions.ChangeActive(activeJsonFile, (int)id);

			WorkSection workSection1 = new WorkSection()
			{
				JobID = (int)id,
				PersonID = activePerson,
				Start = DateTime.Now,
			};

			var wsCtx = _context.WorkSection;

			wsCtx.Add(workSection1);
			await _context.SaveChangesAsync();


			ViewBag.WSID = (int)id;
			ViewBag.activeJob = (int)id;

			return RedirectToAction("Index", "Jobs");
		}

		// GET: WorkSections/Activate/5
		public async Task<IActionResult> Deactivate()
		{
			var activeJsonFile = "C:\\DEV2024\\ProjectManager_v00\\ProjectManager2\\JSON\\activeJob.json";

			var PreviousWS = await _context.WorkSection
				.Include(w => w.Job)
				.Include(w => w.Person)
				.OrderByDescending(h => h.Start)
				.FirstOrDefaultAsync();

			PreviousWS.End = DateTime.Now;
			JsonFunctions.ChangeActive(activeJsonFile, 0);

			var wsCtx = _context.WorkSection;
			await _context.SaveChangesAsync();

			ViewBag.activeJob = 0;

			return RedirectToAction("Index", "Jobs");
		}

		/*End of Activate*/


		private bool JobExists(int id)
		{
			return (_context.Job?.Any(e => e.JobID == id)).GetValueOrDefault();
		}
	}
}

