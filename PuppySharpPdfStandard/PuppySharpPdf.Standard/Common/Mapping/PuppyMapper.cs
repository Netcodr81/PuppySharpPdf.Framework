using PuppeteerSharp;
using PuppySharpPdf.Standard.Renderers.Configurations;

namespace PuppySharpPdf.Standard.Common.Mapping
{
    public static class PuppyMapper
    {
        public static LaunchOptions MapToLaunchOptions(RendererOptions options)
        {
            return new LaunchOptions
            {
                IgnoreHTTPSErrors = options.IgnoreHTTPSErrors,
                Headless = options.Headless,
                ExecutablePath = options.ChromeExecutablePath,
                Args = options.Args,
                Timeout = options.Timeout
            };
        }

    }
}
