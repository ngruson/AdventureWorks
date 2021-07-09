using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetCountries;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetStateProvinces;
using AW.UI.Web.Infrastructure.Extensions;
using AW.UI.Web.Infrastructure.UnitTests.TestBuilders.GetCountries;
using AW.UI.Web.Infrastructure.UnitTests.TestBuilders.GetStateProvinces;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests
{
    public class AddressExtensionsUnitTests
    {
        [Fact]
        public void StateProvinceName_StateProvinceFound_ReturnsName()
        {
            //Arrange
            var list = new List<StateProvince>
            {
                new StateProvinceBuilder()
                    .StateProvinceCode("CA")
                    .Name("California")
                    .Build(),

                new StateProvinceBuilder()
                    .StateProvinceCode("TX")
                    .Name("Texas")
                    .Build()
            };

            //Act
            string name = AddressExtensions.StateProvinceName("TX", list);

            //Assert
            name.Should().Be("Texas");
        }

        [Fact]
        public void StateProvinceName_StateProvinceNotFound_ThrowArgumentNullException()
        {
            //Arrange
            var list = new List<StateProvince>();

            //Act
            Func<string> func = () => AddressExtensions.StateProvinceName("TX", list);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'stateProvince')");
        }

        [Fact]
        public void CountryRegionName_CountryRegionFound_ReturnsName()
        {
            //Arrange
            var list = new List<CountryRegion>
            {
                new CountryBuilder()
                    .CountryRegionCode("US")
                    .Name("United States")
                    .Build(),

                new CountryBuilder()
                    .CountryRegionCode("GB")
                    .Name("United Kingdom")
                    .Build()
            };

            //Act
            string name = AddressExtensions.CountryRegionName("US", list);

            //Assert
            name.Should().Be("United States");
        }

        [Fact]
        public void CountryRegionName_CountryRegionNotFound_ThrowArgumentNullException()
        {
            //Arrange
            var list = new List<CountryRegion>();

            //Act
            Func<string> func = () => AddressExtensions.CountryRegionName("US", list);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'country')");
        }
    }
}