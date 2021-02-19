using AW.Services.CountryRegion.Application.Common;

namespace AW.Services.CountryRegion.Application.GetCountries
{
    public class CountryDto : IMapFrom<Domain.CountryRegion>
    {
        public string CountryRegionCode { get; set; }
        public string Name { get; set; }
    }
}