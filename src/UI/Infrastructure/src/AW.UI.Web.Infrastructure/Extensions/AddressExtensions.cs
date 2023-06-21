using Ardalis.GuardClauses;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetCountries;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetStatesProvinces;

namespace AW.UI.Web.Infrastructure.Extensions;

public static class AddressExtensions
{
    public static string? StateProvinceName(this string stateProvinceCode, List<StateProvince>? statesProvinces)
    {
        var stateProvince = statesProvinces?.Find(s => s.StateProvinceCode == stateProvinceCode);
        Guard.Against.Null(stateProvince);

        return stateProvince?.Name;
    }

    public static string? CountryRegionName(this string countryRegionCode, List<CountryRegion>? countries)
    {
        var country = countries?.Find(s => s.CountryRegionCode == countryRegionCode);
        Guard.Against.Null(country);

        return country?.Name;
    }
}
