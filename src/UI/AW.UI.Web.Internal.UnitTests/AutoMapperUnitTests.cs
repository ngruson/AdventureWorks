using AutoMapper;
using AW.UI.Web.Internal.UnitTests.AutoMapper;
using Customer = AW.UI.Web.Internal.ViewModels.Customer;
using SalesOrder = AW.UI.Web.Internal.ViewModels.SalesOrder;
using Xunit;

namespace AW.UI.Web.Internal.UnitTests
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
        public void AutoMapper_Mapping_CustomerAddressViewModel_IsValid()
        {
            var profile = new TestProfile();
            new Customer.AddressViewModel().Mapping(profile);
            new Customer.StateProvinceViewModel().Mapping(profile);
            new Customer.CountryRegionViewModel().Mapping(profile);

            var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            config.AssertConfigurationIsValid();
        }

        [Fact]
        public void AutoMapper_Mapping_SalesOrderAddressViewModel_IsValid()
        {
            var profile = new TestProfile();
            new SalesOrder.AddressViewModel().Mapping(profile);

            var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            config.AssertConfigurationIsValid();
        }
    }
}