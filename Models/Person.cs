using ProjectManager.Interfaces;

namespace ProjectManager.Models
{
	public class Person : IPerson
	{
		public int PersonID { get; set; }

		public string GivenNames { get; set; }

		public string FamilyNames { get; set; }

		/* Human 1-N WorkSections */
		public virtual ICollection<WorkSection>? WorkSections { get; set; } = new List<WorkSection>();

		/* Human N-N JobHuman (Job) */
		public virtual ICollection<JobPerson>? JobPeople { get; set; } = new List<JobPerson>();

		/* Human 1-N WorkSections */
		public Person()
		{
			WorkSections = new HashSet<WorkSection>();
		}
	}
}
