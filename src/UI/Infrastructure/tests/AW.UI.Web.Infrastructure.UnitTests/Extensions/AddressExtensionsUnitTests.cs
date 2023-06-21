using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetCountries;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetStatesProvinces;
using AW.UI.Web.Infrastructure.Extensions;
using FluentAssertions;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Extensions;

public class AddressExtensionsUnitTests
{
    [Theory, AutoMoqData]
    public void StateProvinceName_StateProvinceFound_ReturnsName(
        List<StateProvince> statesProvinces
    )
    {
        //Act
        var result = statesProvinces[0].StateProvinceCode!.StateProvinceName(
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
        Func<string?> func = () => stateProvince.StateProvinceCode!.StateProvinceName(
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
        var result = list[0].CountryRegionCode!.CountryRegionName(
list);

        //Assert
        result.Should().Be(list[0].Name);
    }

    [Theory, AutoMoqData]
    public void CountryRegionName_CountryRegionNotFound_ThrowArgumentNullException(
        CountryRegion countryRegion
        )
    {
        //Act
        Func<string?> func = () => countryRegion.CountryRegionCode!.CountryRegionName(
            new List<CountryRegion>()
        );

        //Assert
        func.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'country')");
    }
}
