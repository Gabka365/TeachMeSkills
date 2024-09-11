using PortalAboutEverything.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PortalAboutEverything.Models.Movie
{
	public class MethodViewModel
    {
		public string MethodName { get; set; }
		public string MethodType { get; set; }
		public List <MethodParametersViewModel> ParametersInfo { get; set; }
}
}
