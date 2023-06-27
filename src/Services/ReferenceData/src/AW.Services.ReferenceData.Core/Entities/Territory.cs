using AW.Services.SharedKernel.Interfaces;

namespace AW.Services.ReferenceData.Core.Entities;

public class Territory : IAggregateRoot
{
    public Territory(string name, string countryRegionCode, string group)
    {            
        Name = name;
        CountryRegionCode = countryRegionCode;
        Group = group;
    }

    public int Id { get; private set; }
    public Guid ObjectId { get; private set; }
    public string Name { get; private set; }
    public string CountryRegionCode { get; private set; }
    public string Group { get; private set; }
}
