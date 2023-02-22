using PuppeteerSharp;
using PuppeteerSharp.Media;
using PuppySharpPdf.Framework.Common.Mapping;
using PuppySharpPdf.Framework.Renderers.Configurations;
using PuppySharpPdf.Framework.Utils;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PuppySharpPdf.Framework.Renderers
{
    public class PuppyPdfRenderer
    {
        public RendererOptions RendererOptions { get; }

        public PuppyPdfRenderer()
        {

            RendererOptions = new RendererOptions();
        }
        public PuppyPdfRenderer(Action<RendererOptions> options)
        {

            var rendererOptions = new RendererOptions();
            options?.Invoke(rendererOptions); ;

            RendererOptions = rendererOptions;
        }

        public async Task<byte[]> GeneratePdfFromUrlAsync(string url)
        {
            if (url is null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            var urlValidator = new Regex("^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$");
            if (!urlValidator.IsMatch(url))
            {
                url = $"https://{url}";
            }

            if (string.IsNullOrEmpty(RendererOptions.ChromeExecutablePath))
            {
                var browserFetcher = new BrowserFetcher();
                await browserFetcher.DownloadAsync();
            }


            await using var browser = await Puppeteer.LaunchAsync(options: PuppyMapper.MapToLaunchOptions(RendererOptions));
            await using var page = await browser.NewPageAsync();
            await page.GoToAsync(url);


            var result = await page.PdfDataAsync();
            await page.CloseAsync();

            return result;
        }

        public async Task<byte[]> GeneratePdfFromUrlAsync(string url, Action<Framework.Renderers.Configurations.PdfOptions> pdfOptions)
        {
            if (url is null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            var urlValidator = new Regex("^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$");
            if (!urlValidator.IsMatch(url))
            {
                url = $"https://{url}";
            }

            var customPdfOptions = new Framework.Renderers.Configurations.PdfOptions();
            pdfOptions?.Invoke(customPdfOptions);

            if (string.IsNullOrEmpty(RendererOptions.ChromeExecutablePath))
            {
                var browserFetcher = new BrowserFetcher();
                await browserFetcher.DownloadAsync();
            }


            await using var browser = await Puppeteer.LaunchAsync(options: PuppyMapper.MapToLaunchOptions(RendererOptions));
            await using var page = await browser.NewPageAsync();
            await page.GoToAsync(url);

            var result = await page.PdfDataAsync(customPdfOptions.MappedPdfOptions);

            await page.CloseAsync();

            return result;
        }

        public async Task<byte[]> GeneratePdfFromHtmlAsync(string html)
        {
            if (html is null)
            {
                throw new ArgumentNullException(nameof(html));
            }

            if (string.IsNullOrEmpty(RendererOptions.ChromeExecutablePath))
            {
                var browserFetcher = new BrowserFetcher();
                await browserFetcher.DownloadAsync();
            }

            await using var browser = await Puppeteer.LaunchAsync(options: PuppyMapper.MapToLaunchOptions(RendererOptions));

            using var page = await browser.NewPageAsync();
            await page.AddStyleTagAsync(new AddTagOptions { Content = @"#header, #footer { -webkit-print-color-adjust:exact;padding: 0 !important;height: 100% !important;}" });
            await page.EmulateMediaTypeAsync(MediaType.Screen);

            html = HtmlUtils.NormalizeHtmlString(html);

            await page.SetContentAsync(html);

            await page.ImportCssStyles(html);

            var result = await page.PdfDataAsync(new Framework.Renderers.Configurations.PdfOptions().MappedPdfOptions);

            await page.CloseAsync();

            return result;

        }

        public async Task<byte[]> GeneratePdfFromHtmlAsync(string html, Action<Framework.Renderers.Configurations.PdfOptions> options)
        {
            if (html is null)
            {
                throw new ArgumentNullException(nameof(html));
            }

            if (string.IsNullOrEmpty(RendererOptions.ChromeExecutablePath))
            {
                var browserFetcher = new BrowserFetcher();
                await browserFetcher.DownloadAsync();
            }

            var pdfOptions = new Framework.Renderers.Configurations.PdfOptions();
            options?.Invoke(pdfOptions);

            await using var browser = await Puppeteer.LaunchAsync(options: PuppyMapper.MapToLaunchOptions(RendererOptions));

            using var page = await browser.NewPageAsync();
            await page.AddStyleTagAsync(new AddTagOptions { Content = @"#header, #footer { -webkit-print-color-adjust:exact;padding: 0 !important;height: 100% !important;}" });
            await page.EmulateMediaTypeAsync(MediaType.Screen);

            html = HtmlUtils.NormalizeHtmlString(html);

            await page.SetContentAsync(html);

            await page.ImportCssStyles(html);

            var result = await page.PdfDataAsync(pdfOptions.MappedPdfOptions);

            await page.CloseAsync();

            return result;

        }

        public async Task<byte[]> GeneratePdfFromHtmlAsync(string html, Framework.Renderers.Configurations.PdfOptions pdfOptions)
        {
            if (html is null)
            {
                throw new ArgumentNullException(nameof(html));
            }

            if (string.IsNullOrEmpty(RendererOptions.ChromeExecutablePath))
            {
                var browserFetcher = new BrowserFetcher();
                await browserFetcher.DownloadAsync();
            }


            await using var browser = await Puppeteer.LaunchAsync(options: PuppyMapper.MapToLaunchOptions(RendererOptions));

            using var page = await browser.NewPageAsync();
            await page.AddStyleTagAsync(new AddTagOptions { Content = @"#header, #footer { -webkit-print-color-adjust:exact;padding: 0 !important;height: 100% !important;}" });
            await page.EmulateMediaTypeAsync(MediaType.Screen);

            html = HtmlUtils.NormalizeHtmlString(html);

            await page.SetContentAsync(html);

            await page.ImportCssStyles(html);

            var result = await page.PdfDataAsync(pdfOptions.MappedPdfOptions);

            await page.CloseAsync();

            return result;

        }
    }
}
