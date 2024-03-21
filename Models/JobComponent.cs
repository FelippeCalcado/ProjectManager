namespace ProjectManager.Models
{
	public class JobComponent
	{
		public int JobComponentID { get; set; }
		public int JobID { get; set; }
		public Job? Job { get; set; }
		public int ComponentID { get; set; }
		public Job? Component { get; set; }
	}
}
