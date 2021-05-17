using AutoMapper;
using AW.Common.AutoMapper;

namespace AW.Services.Customer.Application.GetCustomer
{
    public class CustomerAddressDto : IMapFrom<Domain.CustomerAddress>
    {
        public string AddressType { get; set; }
        public AddressDto Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.CustomerAddress, CustomerAddressDto>();
        }
    }
}