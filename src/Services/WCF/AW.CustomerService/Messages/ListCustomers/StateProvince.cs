using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Application.Customer.GetCustomers;

namespace AW.CustomerService.Messages.ListCustomers
{
    public class StateProvince : IMapFrom<StateProvinceDto>
    {
        public string StateProvinceCode { get; set; }
        public string Name { get; set; }
        public CountryRegion CountryRegion { get; set; }        

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StateProvinceDto, StateProvince>();
        }
    }
}