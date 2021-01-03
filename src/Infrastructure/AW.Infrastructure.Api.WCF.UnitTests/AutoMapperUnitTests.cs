using AutoMapper;
using AW.Infrastructure.Api.WCF.AutoMapper;
using Xunit;

namespace AW.Infrastructure.Api.WCF.UnitTests
{
    public class AutoMapperUnitTests
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddMaps(
                typeof(CustomerProfile)
            ));
            config.AssertConfigurationIsValid();
        }
    }
}