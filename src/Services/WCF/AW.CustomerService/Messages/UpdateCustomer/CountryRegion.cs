using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Application.Customer.GetCustomer;

namespace AW.CustomerService.Messages.UpdateCustomer
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