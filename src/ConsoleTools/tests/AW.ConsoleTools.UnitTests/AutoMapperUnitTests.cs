using AutoMapper;
using AW.ConsoleTools.AutoMapper;
using Xunit;

namespace AW.ConsoleTools.UnitTests
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