using AutoMapper;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests
{
    public class AutoMapperUnitTests
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            config.AssertConfigurationIsValid();
        }
    }
}