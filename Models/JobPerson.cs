namespace ProjectManager.Models
{
	public class JobPerson
	{
		public int JobPersonID { get; set; }
		public int JobID { get; set; }
		public Job? Job { get; set; }
		public int PersonID { get; set; }
		public Person? Person { get; set; }
	}
}
