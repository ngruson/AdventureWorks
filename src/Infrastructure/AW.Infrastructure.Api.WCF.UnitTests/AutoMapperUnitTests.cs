using AutoMapper;
using AW.Infrastructure.Api.WCF.AutoMapper;
using Xunit;

namespace AW.Infrastructure.Api.WCF.UnitTests
{
    public class AutoMapperUnitTests
    {
        [Fact]
        public void AutoMapper_AllProfiles_AreValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddMaps(
                typeof(CustomerProfile)
            ));
            config.AssertConfigurationIsValid();
        }

        //[Fact]
        //public void AutoMapper_AddressTypeProfile_IsValid()
        //{
        //    var config = new MapperConfiguration(cfg => cfg.AddProfile(
        //        typeof(AddressTypeProfile)
        //    ));
        //    config.AssertConfigurationIsValid();
        //}

        //[Fact]
        //public void AutoMapper_ContactTypeProfile_IsValid()
        //{
        //    var config = new MapperConfiguration(cfg => cfg.AddProfile(
        //        typeof(ContactTypeProfile)
        //    ));
        //    config.AssertConfigurationIsValid();
        //}

        //[Fact]
        //public void AutoMapper_CountryProfile_IsValid()
        //{
        //    var config = new MapperConfiguration(cfg => cfg.AddProfile(
        //        typeof(CountryProfile)
        //    ));
        //    config.AssertConfigurationIsValid();
        //}

        //[Fact]
        //public void AutoMapper_CustomerProfile_IsValid()
        //{
        //    var config = new MapperConfiguration(cfg => cfg.AddProfile(
        //        typeof(CustomerProfile)
        //    ));
        //    config.AssertConfigurationIsValid();
        //}

        //[Fact]
        //public void AutoMapper_ProductProfile_IsValid()
        //{
        //    var config = new MapperConfiguration(cfg => cfg.AddProfile(
        //        typeof(ProductProfile)
        //    ));
        //    config.AssertConfigurationIsValid();
        //}

        //[Fact]
        //public void AutoMapper_SalesOrderProfile_IsValid()
        //{
        //    var config = new MapperConfiguration(cfg => cfg.AddProfile(
        //        typeof(SalesOrderProfile)
        //    ));
        //    config.AssertConfigurationIsValid();
        //}

        //[Fact]
        //public void AutoMapper_SalesPersonProfile_IsValid()
        //{
        //    var config = new MapperConfiguration(cfg => cfg.AddProfile(
        //        typeof(SalesPersonProfile)
        //    ));
        //    config.AssertConfigurationIsValid();
        //}

        //[Fact]
        //public void AutoMapper_SalesTerritoryProfile_IsValid()
        //{
        //    var config = new MapperConfiguration(cfg => cfg.AddProfile(
        //        typeof(SalesTerritoryProfile)
        //    ));
        //    config.AssertConfigurationIsValid();
        //}

        //[Fact]
        //public void AutoMapper_StateProvinceProfile_IsValid()
        //{
        //    var config = new MapperConfiguration(cfg => cfg.AddProfile(
        //        typeof(StateProvinceProfile)
        //    ));
        //    config.AssertConfigurationIsValid();
        //}
    }
}