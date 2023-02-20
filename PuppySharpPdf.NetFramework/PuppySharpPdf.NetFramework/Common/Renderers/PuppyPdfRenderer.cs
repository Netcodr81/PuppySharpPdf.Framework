using AutoMapper;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using PuppySharpPdf.NetFramework.Common.Mapping;
using PuppySharpPdf.NetFramework.Common.Renderers.Configurations;
using PuppySharpPdf.NetFramework.Common.Utils;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PuppySharpPdf.NetFramework.Common;
public class PuppyPdfRenderer
{
    private readonly IMapper _mapper;
    public RendererOptions RendererOptions { get; }

    public PuppyPdfRenderer()
    {
        _mapper = new MapperConfig().Mapper;
        RendererOptions = new RendererOptions();
    }
    public PuppyPdfRenderer(Action<RendererOptions> options)
    {
        _mapper = new MapperConfig().Mapper;
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

        var options = _mapper.Map<LaunchOptions>(RendererOptions);
        await using var browser = await Puppeteer.LaunchAsync(options: _mapper.Map<LaunchOptions>(RendererOptions));
        await using var page = await browser.NewPageAsync();
        await page.GoToAsync(url);


        var result = await page.PdfDataAsync();
        await page.CloseAsync();

        return result;
    }

    public async Task<byte[]> GeneratePdfFromUrlAsync(string url, Action<Renderers.Configurations.PdfOptions> pdfOptions)
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

        var customPdfOptions = new Renderers.Configurations.PdfOptions();
        pdfOptions?.Invoke(customPdfOptions);

        if (string.IsNullOrEmpty(RendererOptions.ChromeExecutablePath))
        {
            var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync();
        }

        var options = _mapper.Map<LaunchOptions>(RendererOptions);
        await using var browser = await Puppeteer.LaunchAsync(options: _mapper.Map<LaunchOptions>(RendererOptions));
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

        await using var browser = await Puppeteer.LaunchAsync(options: _mapper.Map<LaunchOptions>(RendererOptions));

        using var page = await browser.NewPageAsync();
        await page.AddStyleTagAsync(new AddTagOptions { Content = @"#header, #footer { -webkit-print-color-adjust:exact;padding: 0 !important;height: 100% !important;}" });
        await page.EmulateMediaTypeAsync(MediaType.Screen);

        html = HtmlUtils.NormalizeHtmlString(html);

        await page.SetContentAsync(html);

        await page.ImportCssStyles(html);

        var result = await page.PdfDataAsync(new Renderers.Configurations.PdfOptions().MappedPdfOptions);

        await page.CloseAsync();

        return result;

    }

    public async Task<byte[]> GeneratePdfFromHtmlAsync(string html, Action<Renderers.Configurations.PdfOptions> options)
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

        var pdfOptions = new Renderers.Configurations.PdfOptions();
        options?.Invoke(pdfOptions);

        await using var browser = await Puppeteer.LaunchAsync(options: _mapper.Map<LaunchOptions>(RendererOptions));

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

    public async Task<byte[]> GeneratePdfFromHtmlAsync(string html, Renderers.Configurations.PdfOptions pdfOptions)
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


        await using var browser = await Puppeteer.LaunchAsync(options: _mapper.Map<LaunchOptions>(RendererOptions));

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
