using ProjectManager.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.Models
{
	public class Job : IJob
	{
		public int JobID { get; set; }
		public string JobName { get; set; }
		public double TimeEstimation { get; set; }
		public bool? Finished { get; set; }

		/* Project 1-N Job */
		public int ProjectID { get; set; }
		public virtual Project? Project { get; set; }

		/* Job 1-N WorkSections */
		[NotMapped]
		public virtual ICollection<WorkSection>? WorkSections { get; set; }

		/* Job N-N WorkSections Requirements, Components & Humans */
		[NotMapped]
		public virtual ICollection<JobRequirement>? JobRequirements { get; set; } = new List<JobRequirement>();
		[NotMapped]
		public virtual ICollection<JobComponent>? JobComponents { get; set; } = new List<JobComponent>();
		[NotMapped]
		public virtual ICollection<JobPerson>? JobPeople { get; set; } = new List<JobPerson>();

		public Job()
		{
			WorkSections = new HashSet<WorkSection>();
		}
	}
}
