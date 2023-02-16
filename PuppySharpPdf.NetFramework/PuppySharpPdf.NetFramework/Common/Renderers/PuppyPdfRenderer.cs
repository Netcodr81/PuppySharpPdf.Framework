using PuppySharpPdf.NetFramework.Common.Renderers.Configurations;
using System;

namespace PuppySharpPdf.NetFramework.Common;
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



}
