using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer.GetCustomers;

namespace AW.CustomerService.Messages.ListCustomers
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