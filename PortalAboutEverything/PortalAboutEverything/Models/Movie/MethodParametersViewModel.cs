using PortalAboutEverything.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PortalAboutEverything.Models.Movie
{
	public class MethodParametersViewModel
    {
		public string ParameterName { get; set; }
		public string ParameterType { get; set; }
	}
}
