using PuppeteerSharp;
using PuppySharpPdf.Framework.Renderers.Configurations;

namespace PuppySharpPdf.Framework.Common.Mapping
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
