using AutoMapper;
using PuppeteerSharp;
using PuppySharpPdf.NetFramework.Common.Mapping;
using PuppySharpPdf.NetFramework.Common.Renderers.Configurations;
using System;
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

        if (!url.ToLower().Contains("http") || !url.ToLower().Contains("https"))
        {
            url = $"https://{url}";
        }

        var browserFetcher = new BrowserFetcher();
        await browserFetcher.DownloadAsync();
        var options = _mapper.Map<LaunchOptions>(RendererOptions);
        await using var browser = await Puppeteer.LaunchAsync(options: _mapper.Map<LaunchOptions>(RendererOptions));
        await using var page = await browser.NewPageAsync();
        await page.GoToAsync(url);

        var result = await page.PdfDataAsync();

        return result;
    }


}
