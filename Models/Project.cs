using ProjectManager.Interfaces;

namespace ProjectManager.Models
{
	public class Project : IProject
	{
		public int ProjectID { get; set; }
		public string ProjectName { get; set; }

		/* Project 1-n Job */
		public virtual ICollection<Job>? Jobs { get; set; }

		/* Field 1-n Project */
		public int? FieldID { get; set; }
		public virtual Field? Field { get; set; }

		public Project()
		{
			Jobs = new HashSet<Job>();
		}
	}
}
