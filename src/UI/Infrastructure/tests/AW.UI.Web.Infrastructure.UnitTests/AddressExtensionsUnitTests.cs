using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Extensions;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetCountries;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetStatesProvinces;
using FluentAssertions;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests
{
    public class AddressExtensionsUnitTests
    {
        [Theory, AutoMoqData]
        public void StateProvinceName_StateProvinceFound_ReturnsName(
            List<StateProvince> statesProvinces
        )
        {
            //Act
            var result = AddressExtensions.StateProvinceName(
                statesProvinces[0].StateProvinceCode!,
                statesProvinces
            );

            //Assert
            result.Should().Be(statesProvinces[0].Name);
        }

        [Theory, AutoMoqData]
        public void StateProvinceName_StateProvinceNotFound_ThrowArgumentNullException(
            StateProvince stateProvince
            )
        {
            //Act
            Func<string?> func = () => AddressExtensions.StateProvinceName(
                stateProvince.StateProvinceCode!,
                new List<StateProvince>()
            );

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'stateProvince')");
        }

        [Theory, AutoMoqData]
        public void CountryRegionName_CountryRegionFound_ReturnsName(
            List<CountryRegion> list
        )
        {
            //Act
            var result = AddressExtensions.CountryRegionName(
                list[0].CountryRegionCode!, list);

            //Assert
            result.Should().Be(list[0].Name);
        }

        [Theory, AutoMoqData]
        public void CountryRegionName_CountryRegionNotFound_ThrowArgumentNullException(
            CountryRegion countryRegion
            )
        {
            //Act
            Func<string?> func = () => AddressExtensions.CountryRegionName(
                countryRegion.CountryRegionCode!,
                new List<CountryRegion>()
            );

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'country')");
        }
    }
}