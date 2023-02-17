using AutoMapper;
using PuppeteerSharp;
using PuppySharpPdf.NetFramework.Common.Renderers.Configurations;

namespace PuppySharpPdf.NetFramework.Common.Mapping.MappingProfiles;
public class RenderingOptionsMappingConfigs : Profile
{
    public RenderingOptionsMappingConfigs()
    {
        CreateMap<RendererOptions, LaunchOptions>()
            .ForMember(dest => dest.ExecutablePath, opt => opt.MapFrom(src => src.ChromeExecutablePath));


    }
}
