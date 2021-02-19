using AW.Services.CountryRegion.Application.Common;
using AW.Services.CountryRegion.Application.GetCountries;

namespace AW.Services.CountryRegion.REST.API.Models
{
    public class CountryRegion : IMapFrom<CountryDto>
    {
        public string CountryRegionCode { get; set; }
        public string Name { get; set; }
    }
}