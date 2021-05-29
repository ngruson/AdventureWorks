using AutoMapper;

namespace AW.Services.Customer.Application.UnitTests
{
    public class Mapper
    {
        public static IConfigurationProvider MapperConfig()
        {
            return new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            });
        }

        public static IMapper CreateMapper()
        {
            return MapperConfig().CreateMapper();
        }
    }
}