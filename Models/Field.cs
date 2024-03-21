using ProjectManager.Interfaces;

namespace ProjectManager.Models
{
	public class Field : IField
	{
		public int FieldID { get; set; }

		public string FieldName { get; set; }
		
		/* Field 1-n Project */
		public virtual ICollection<Project> Projects { get; set; }
        public int? CardFormatID { get; set; }
        public CardFormat? CardFormat { get; set; }


        /* Field 1-n Project */
        public Field()
		{
			Projects = new HashSet<Project>();
		}
	}
}
