using PuppySharpPdf.NetFramework.Common;
using PuppySharpPdf.NetFramework.DemoProject.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PuppySharpPdf.NetFramework.DemoProject.Controllers
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

        public ActionResult GeneratePdfUsingHtml()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}