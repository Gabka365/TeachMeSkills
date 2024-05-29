using System.ComponentModel;

namespace PortalAboutEverything.Enums
{
	public enum Tags
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
