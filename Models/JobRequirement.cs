namespace ProjectManager.Models
{
	public class JobRequirement
	{
		public int JobRequirementID { get; set; }
		public int JobID { get; set; }
		public Job? Job { get; set; }
		public int RequirementID { get; set; }
		public Job? Requirement { get; set; }
	}
}
