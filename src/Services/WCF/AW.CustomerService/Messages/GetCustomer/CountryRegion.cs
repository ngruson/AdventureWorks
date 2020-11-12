using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer.GetCustomer;

namespace AW.CustomerService.Messages.GetCustomer
{
    public class CountryRegion : IMapFrom<CountryRegionDto>
    {
        public string CountryRegionCode { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CountryRegionDto, CountryRegion>();
        }
    }
}