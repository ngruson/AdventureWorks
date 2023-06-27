using AW.Services.SharedKernel.Interfaces;

namespace AW.Services.ReferenceData.Core.Entities;

public class CountryRegion : IAggregateRoot
{
    public CountryRegion(string countryRegionCode)
    {
        CountryRegionCode = countryRegionCode;
    }
    public string CountryRegionCode { get; private set; }
    public Guid ObjectId { get; private set; }
    public string? Name { get; private set; }

    public List<StateProvince> StatesProvinces { get; internal set; } = new();
}
