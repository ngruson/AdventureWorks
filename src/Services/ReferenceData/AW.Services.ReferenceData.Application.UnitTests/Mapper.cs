using AutoMapper;
using AW.Services.ReferenceData.Application.Common;

namespace AW.Services.ReferenceData.Application.UnitTests
{
    public class Mapper
    {
        public static IConfigurationProvider MapperConfig()
        {
            return new MapperConfiguration(opts =>
            {
                opts.AddProfile<BaseMappingProfile>();
            });
        }

        public static IMapper CreateMapper()
        {
            return MapperConfig().CreateMapper();
        }
    }
}