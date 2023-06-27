using AW.SharedKernel.AutoMapper;

namespace AW.Services.ReferenceData.Core.Handlers.CountryRegion.GetCountries;

public class Country : IMapFrom<Entities.CountryRegion>
{
    public string? CountryRegionCode { get; private init; }
    public Guid ObjectId { get; set; }
    public string? Name { get; private init; }
}
