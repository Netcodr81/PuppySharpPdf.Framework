using PuppySharpPdf.Framework.DemoProject.Models;
using PuppySharpPdf.Framework.Renderers;
using PuppySharpPdf.Framework.Renderers.Configurations;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Westwind.Web.Mvc;

namespace PuppySharpPdf.Framework.DemoProject.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult GeneratePdfByUrl()
		{
			ViewBag.Message = "Your application description page.";

			return View(new PdfGenerationByUrlRequest());
		}

		[HttpPost]
		public async Task<ActionResult> GeneratePdfByUrl(PdfGenerationByUrlRequest request)
		{
			if (!ModelState.IsValid)
			{
				return View(request);
			}

			if (request.UseLocalExe)
			{
				var pdfRendererLocal = new PuppyPdfRenderer(options =>
				{
					options.ChromeExecutablePath = Server.MapPath("~/Content/ChromeBrowser/chrome-win/chrome.exe");

				});


				if (request.DisplayHeaderFooter)
				{
					var pdfOptions = new PdfOptions()
					{
						DisplayHeaderFooter = true,

					};

					var resultLocal = await pdfRendererLocal.GeneratePdfFromUrlAsync(request.Url, pdfOptions);
					return File(resultLocal.Value, "application/pdf", "PdfFromUrl.pdf");
				}
				else
				{
					var resultLocal = await pdfRendererLocal.GeneratePdfFromUrlAsync(request.Url);
					return File(resultLocal.Value, "application/pdf", "PdfFromUrl.pdf");
				}



			}

			var pdfRenderer = new PuppyPdfRenderer();
			var result = await pdfRenderer.GeneratePdfFromUrlAsync(request.Url);

			return File(result.Value, "application/pdf", "PdfFromUrl.pdf");
		}

		public ActionResult GeneratePdfByUrlWithCustomOptions()
		{
			return View(new PdfGenerationByUrlRequest() { PdfOptions = new PdfOptionsViewModel() });
		}

		[HttpPost]
		public async Task<ActionResult> GeneratePdfByUrlWithCustomOptions(PdfGenerationByUrlRequest request)
		{
			if (!ModelState.IsValid)
			{
				return View(request);
			}

			if (request.UseLocalExe)
			{
				var pdfRendererLocal = new PuppyPdfRenderer(options =>
				{
					options.ChromeExecutablePath = Server.MapPath("~/Content/ChromeBrowser/chrome-win/chrome.exe");

				});

				var resultLocal = await pdfRendererLocal.GeneratePdfFromUrlAsync(request.Url, options =>
				{
					options.PrintBackground = request.PdfOptions.PrintBackground;
					options.Format = request.PdfOptions.GetPaperFormatType();
					options.Scale = request.PdfOptions.Scale;
					options.DisplayHeaderFooter = request.PdfOptions.DisplayHeaderFooter;
					options.HeaderTemplate = request.PdfOptions.HeaderTemplate;
					options.FooterTemplate = request.PdfOptions.FooterTemplate;
					options.Landscape = request.PdfOptions.Landscape;
					options.PageRanges = request.PdfOptions.PageRanges;
					options.Width = request.PdfOptions.Width;
					options.Height = request.PdfOptions.Height;
					options.MarginOptions = request.PdfOptions.MarginOptions;
					options.PreferCSSPageSize = request.PdfOptions.PreferCSSPageSize;
					options.OmitBackground = request.PdfOptions.OmitBackground;
				});

				return File(resultLocal.Value, "application/pdf", "PdfFromUrlWithCustomOptions.pdf");
			}

			var pdfRenderer = new PuppyPdfRenderer();
			var result = await pdfRenderer.GeneratePdfFromUrlAsync(request.Url, options =>
			{
				options.PrintBackground = request.PdfOptions.PrintBackground;
				options.Format = request.PdfOptions.GetPaperFormatType();
				options.Scale = request.PdfOptions.Scale;
				options.DisplayHeaderFooter = request.PdfOptions.DisplayHeaderFooter;
				options.HeaderTemplate = request.PdfOptions.HeaderTemplate;
				options.FooterTemplate = request.PdfOptions.FooterTemplate;
				options.Landscape = request.PdfOptions.Landscape;
				options.PageRanges = request.PdfOptions.PageRanges;
				options.Width = request.PdfOptions.Width;
				options.Height = request.PdfOptions.Height;
				options.MarginOptions = request.PdfOptions.MarginOptions;
				options.PreferCSSPageSize = request.PdfOptions.PreferCSSPageSize;
				options.OmitBackground = request.PdfOptions.OmitBackground;
			});

			return File(result.Value, "application/pdf", "PdfFromUrlWithCustomOptions.pdf");
		}


		public ActionResult GeneratePdfUsingHtml()
		{


			return View(new DemoTemplateViewModel());
		}

		[HttpPost]
		public async Task<ActionResult> GeneratePdfUsingRazorTemplate(DemoTemplateViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var html = ViewRenderer.RenderPartialView("~/Views/Shared/Templates/Demo.cshtml", model);

			if (model.UseLocalExe)
			{
				var pdfRendererLocal = new PuppyPdfRenderer(options =>
				{
					options.ChromeExecutablePath = Server.MapPath("~/Content/ChromeBrowser/chrome-win/chrome.exe");

				});

				var resultLocal = await pdfRendererLocal.GeneratePdfFromHtmlAsync(html);

				return File(resultLocal.Value, "application/pdf", "PdfFromUrl.pdf");
			}

			var pdfRenderer = new PuppyPdfRenderer();
			var result = await pdfRenderer.GeneratePdfFromHtmlAsync(html);

			return File(result.Value, "application/pdf", "GeneratedFromRazorFile.pdf");
		}

		[HttpPost]
		public async Task<ActionResult> GeneratePdfUsingHtmlTemplate(DemoTemplateViewModel model)
		{


			var html = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "Views/Shared/Templates/Demo.html");

			if (model.IncludeFooter || model.IncludeHeader)
			{
				var pdfOptions = new PdfOptions
				{
					DisplayHeaderFooter = true,
					HeaderTemplate = model.IncludeHeader ? ViewRenderer.RenderPartialView("~/Views/Shared/Templates/Header.cshtml") : string.Empty,
					FooterTemplate = model.IncludeFooter ? "<h1>This is the footer!</h1>" : string.Empty,
					MarginOptions = new MarginOptions
					{
						Top = "160px",
						Bottom = "160px",
						Left = "0px",
						Right = "0px"
					}
				};

				var renderer = new PuppyPdfRenderer();
				var results = await renderer.GeneratePdfFromHtmlAsync(html, pdfOptions);

				return File(results.Value, "application/pdf", "GeneratedFromHtmlFile.pdf");
			}

			var pdfRenderer = new PuppyPdfRenderer();
			var result = await pdfRenderer.GeneratePdfFromHtmlAsync(html);

			return File(result.Value, "application/pdf", "GeneratedFromHtmlFile.pdf");
		}

		public ActionResult GeneratePdfUsingHtmlWithCustomOptions()
		{
			return View();
		}
	}
}