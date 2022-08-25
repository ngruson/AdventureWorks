using AutoMapper;
using AW.Services.HumanResources.Core.AutoMapper;

namespace AW.Services.HumanResources.Core.UnitTests
{
    public class AutoMapperUnitTests
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            config.AssertConfigurationIsValid();
        }
    }
}