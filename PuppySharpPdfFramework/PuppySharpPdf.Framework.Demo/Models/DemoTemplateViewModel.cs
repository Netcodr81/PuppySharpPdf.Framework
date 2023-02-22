using System.ComponentModel.DataAnnotations;

namespace PuppySharpPdf.Framework.DemoProject.Models
{
	public class DemoTemplateViewModel
	{
		[Display(Name = "First Name")]
		public string FirstName { get; set; }
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Display(Name = "Use local Chrome EXE")]
		public bool UseLocalExe { get; set; }

		[Display(Name = "Include Header")]
		public bool IncludeHeader { get; set; }

		[Display(Name = "Include Footer")]
		public bool IncludeFooter { get; set; }

		public PdfOptionsViewModel PdfOptions { get; set; }
	}
}
