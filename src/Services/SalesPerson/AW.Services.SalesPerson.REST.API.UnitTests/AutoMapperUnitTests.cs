using AutoMapper;
using AW.Services.SalesPerson.REST.API;
using Xunit;

namespace AW.Services.Customer.WCF.UnitTests
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