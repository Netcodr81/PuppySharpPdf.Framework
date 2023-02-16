using PuppySharpPdf.NetFramework.Common;
using Shouldly;

namespace PuppySharpPdf.NetFramework.Tests.Common.Renderers;
public class PuppyPdfRendererTests
{
    [Fact]
    public void PuppyPdfRenderer_DefaultConstructorCalled_ShouldReturnDefaultRenderingOptions()
    {

        // arrange and act
        var renderer = new PuppyPdfRenderer();

        // assert        
        renderer.RendererOptions.Headless.ShouldBe(true);
        renderer.RendererOptions.ChromeExecutablePath.ShouldBe(null);
        renderer.RendererOptions.Timeout.ShouldBe(30000);
        renderer.RendererOptions.Env.ShouldBe(new Dictionary<string, string>());
        renderer.RendererOptions.Args.ShouldBe(Array.Empty<string>());
    }

    [Fact]
    public void PuppyPdfRenderer_CustomOptionsConstructorCalled_ShouldReturnCustomRenderingOptions()
    {

        // arrange and act
        var renderer = new PuppyPdfRenderer(options =>
        {
            options.Headless = false;
            options.IgnoreHTTPSErrors = true;
            options.ChromeExecutablePath = "C:\\Test\\chrome.exe";
            options.Timeout = 500;
        });

        // assert        
        renderer.RendererOptions.Headless.ShouldBe(false);
        renderer.RendererOptions.ChromeExecutablePath.ShouldBe("C:\\Test\\chrome.exe");
        renderer.RendererOptions.Timeout.ShouldBe(500);
        renderer.RendererOptions.Env.ShouldBe(new Dictionary<string, string>());
        renderer.RendererOptions.Args.ShouldBe(Array.Empty<string>());
    }
}
