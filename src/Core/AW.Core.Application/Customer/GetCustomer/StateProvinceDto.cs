using AutoMapper;
using AW.Core.Application.AutoMapper;

namespace AW.Core.Application.Customer.GetCustomer
{
    public class StateProvinceDto : IMapFrom<Domain.Person.StateProvince>
    {
        public string StateProvinceCode { get; set; }
        public string CountryRegionCode { get; set; }
        public string Name { get; set; }
        public CountryRegionDto CountryRegion { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Person.StateProvince, StateProvinceDto>();
        }
    }
}