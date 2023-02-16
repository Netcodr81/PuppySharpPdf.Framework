using AutoMapper;
using PuppySharpPdf.NetFramework.Common.Mapping.MappingProfiles;

namespace PuppySharpPdf.NetFramework.Common.Mapping;

public class MapperConfig
{

    private IMapper _mapper;

    public MapperConfig()
    {
        MapperConfiguration configuration = new MapperConfiguration(config => config.AddMaps(typeof(RenderingOptionsMappingConfigs).Assembly));
        _mapper = configuration.CreateMapper();
    }

    public IMapper Mapper => _mapper;
}
