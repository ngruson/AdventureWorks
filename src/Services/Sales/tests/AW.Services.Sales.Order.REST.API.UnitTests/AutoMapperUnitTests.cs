using AutoMapper;
using AW.Services.Sales.Core.Models;
using Xunit;

namespace AW.Services.Sales.Order.REST.API.UnitTests
{
    public class AutoMapperUnitTests
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            config.AssertConfigurationIsValid();
        }

        [Fact]
        public void AutoMapper_Mapping_Address_IsValid()
        {
            var profile = new TestProfile();
            new Address().Mapping(profile);

            var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            config.AssertConfigurationIsValid();
        }
    }
}