using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer.GetCustomer;

namespace AW.CustomerService.Messages.UpdateCustomer
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