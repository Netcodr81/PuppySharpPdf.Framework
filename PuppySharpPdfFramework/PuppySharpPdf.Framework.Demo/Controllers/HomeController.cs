using PuppySharpPdf.Framework.DemoProject.Models;
using PuppySharpPdf.Framework.Renderers;
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

                var resultLocal = await pdfRendererLocal.GeneratePdfFromUrlAsync(request.Url);

                return File(resultLocal, "application/pdf", "PdfFromUrl.pdf");
            }

            var pdfRenderer = new PuppyPdfRenderer();
            var result = await pdfRenderer.GeneratePdfFromUrlAsync(request.Url);

            return File(result, "application/pdf", "PdfFromUrl.pdf");
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

                return File(resultLocal, "application/pdf", "PdfFromUrlWithCustomOptions.pdf");
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

            return File(result, "application/pdf", "PdfFromUrlWithCustomOptions.pdf");
        }


        public ActionResult GeneratePdfUsingHtml()
        {


            return View(new DemoTemplateViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> GeneratePdfUsingHtml(DemoTemplateViewModel model)
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

                return File(resultLocal, "application/pdf", "PdfFromUrl.pdf");
            }

            var pdfRenderer = new PuppyPdfRenderer();
            var result = await pdfRenderer.GeneratePdfFromHtmlAsync(html);

            return File(result, "application/pdf", "GeneratedFromHtmlString.pdf");
        }

        public ActionResult GeneratePdfUsingHtmlWithCustomOptions()
        {
            return View();
        }
    }
}