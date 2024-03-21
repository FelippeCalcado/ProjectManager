using ProjectManager.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace ProjectManager.Models
{
	public class WorkSection : IWorkSection
	{
		public int WorkSectionID { get; set; }
		public int PersonID { get; set; }
		public virtual Person? Person { get; set; }
		public int JobID { get; set; }
		public virtual Job? Job { get; set; }
		public DateTime Start { get; set; }
		public DateTime? End { get; set; }
	}
}
