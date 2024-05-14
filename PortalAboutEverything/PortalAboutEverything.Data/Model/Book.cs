using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalAboutEverything.Data.Model
{
    public class Book
    {
        public int Id { get; set; }
        public required string BookAuthor { get; set; }
        public required string BookTitle { get; set; }
		public required List<Subject> SubjectsOfBook { get; set; }
		public required string SummaryOfBook { get; set; }
        public int YearOfPublication { get; set; }
    }

	public enum Subject
	{
		[Description("C#")]
		CSharp,
		[Description("C++")]
		CPlusPlus,
		Linux,
		[Description("Алгоритмы")]
		Algorithms,
		[Description("БД")]
		Database,
		[Description("Безопасность")]
		Safety,
		[Description("Большие Cистемы")]
		LargeSystems,
		[Description("Фронтенд")]
		Frontend,
		[Description("Rust, Unix, тестирование,\r\nтипизирование, git")]
		AnotherInteresting,
		[Description("Софт-скилы")]
		SoftSkills

	}

}
