using AutoMapper;
using Xunit;

namespace AW.Services.Customer.REST.API.UnitTests
{
    public class AutoMapperUnitTests
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
                cfg.AddProfile<Core.MappingProfile>();
            });
            config.AssertConfigurationIsValid();
        }

        [Fact]
        public void AutoMapper_Mapping_UpdateCustomerAddress_IsValid()
        {
            var profile = new TestProfile();
            new Core.Models.UpdateCustomer.Address().Mapping(profile);

            var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            config.AssertConfigurationIsValid();
        }
    }
}