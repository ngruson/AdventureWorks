using AutoMapper;
using AW.Services.Product.Core.AutoMapper;
using Xunit;

namespace AW.Services.Product.Core.UnitTests
{
    public class AutoMapperUnitTests
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            config.AssertConfigurationIsValid();
        }
    }
}
