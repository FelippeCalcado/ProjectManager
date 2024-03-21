using ProjectManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectManager.Data
{
	public class Initializer
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{

			using (var scope = serviceProvider.CreateScope())
			{

				var context = scope.ServiceProvider.GetRequiredService<ProjectManager_v00_DbContext>();

				context.Database.Migrate(); // sincronizar os modelos com a bd

				if (!context.Field.Any())
				{
					SeedField(context);
				}

				if (!context.Project.Any())
				{
					SeedProject(context);
				}

				if (!context.Job.Any())
				{
					SeedJob(context);
				}

				if (!context.Person.Any())
				{
					SeedPerson(context);
				}
				/*
				if (!context.WorkSection.Any())
				{
					SeedWorkSection(context);
				}
				*/
				if (!context.JobComponent.Any())
				{
					SeedJobComponent(context);
				}

				if (!context.JobRequirement.Any())
				{
					SeedJobRequirement(context);
				}
				/*
				if (!context.JobPerson.Any())
				{
					SeedJobPerson(context);
				}
				*/
			}

		}

		private static void SeedField(ProjectManager_v00_DbContext context)
		{

			var fields = new[]
			{
				new Field {
					FieldName = "[ Field 00 ]"
				},
				new Field {
					FieldName = "[ Field 01 ]"
				}
			};

			context.Field.AddRange(fields);

			context.SaveChanges();

		}

		private static void SeedProject(ProjectManager_v00_DbContext context)
		{

			var projects = new[]
			{
				new Project {
					ProjectName = "[ Project 00 ]",
					FieldID = 1,
				},
				new Project {
					ProjectName = "[ Project 01 ]",
					FieldID = 2,
				}
			};

			context.Project.AddRange(projects);

			context.SaveChanges();

		}

		private static void SeedJob(ProjectManager_v00_DbContext context)
		{

			var jobs = new[]
			{
				new Job {
					JobName = "[ Job 00 ]",
					TimeEstimation = 60000,
					ProjectID = 1,
				},
				new Job {
					JobName = "[ Job 01 ]",
					TimeEstimation = 120000,
					ProjectID = 2,
				},
				new Job {
					JobName = "[ Job 02 ]",
					TimeEstimation = 10000,
					ProjectID = 1,
				}
			};

			context.Job.AddRange(jobs);

			context.SaveChanges();

		}

		private static void SeedPerson(ProjectManager_v00_DbContext context)
		{
			var people = new[]
			{
				new Person
				{
					GivenNames = "Given1 Name1",
					FamilyNames = "Family1 Names1",
				},
				new Person
				{
					GivenNames = "Given2 Name2",
					FamilyNames = "Family2 Names2",
				}
			};

			context.Person.AddRange(people);

			context.SaveChanges();

		}

		private static void SeedWorkSection(ProjectManager_v00_DbContext context)
		{
			var workSection = new[]
			{
				new WorkSection
				{
					PersonID = 1,
					JobID = 1,
				},
				new WorkSection
				{
					PersonID = 2,
					JobID = 2,
				}
			};

			context.WorkSection.AddRange(workSection);

			context.SaveChanges();
		}

		private static void SeedJobRequirement(ProjectManager_v00_DbContext context)
		{
			var jobRequirement = new[]
			{
				new JobRequirement
				{
					JobID = 1,
					RequirementID = 2,
				},
				new JobRequirement
				{
					JobID = 1,
					RequirementID = 2,
				}
			};

			context.JobRequirement.AddRange(jobRequirement);

			context.SaveChanges();

		}


		private static void SeedJobComponent(ProjectManager_v00_DbContext context)
		{
			var jobComponent = new[]
			{
				new JobComponent
				{
					JobID = 1,
					ComponentID = 3,
				},
				new JobComponent
				{
					JobID = 2,
					ComponentID = 3,
				}
			};

			context.JobComponent.AddRange(jobComponent);

			context.SaveChanges();

		}

		private static void SeedJobPerson(ProjectManager_v00_DbContext context)
		{
			var jobPerson = new[]
			{
				new JobPerson
				{
					JobID = 1,
					PersonID = 1,
				},
				new JobPerson
				{
					JobID = 2,
					PersonID = 2,
				},
				new JobPerson
				{
					JobID = 3,
					PersonID = 2,
				}
			};

			context.JobPerson.AddRange(jobPerson);

			context.SaveChanges();

		}
	}
}
