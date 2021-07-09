using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Entities;

namespace AW.Services.Customer.Core.Handlers.GetCustomers
{
    public class CustomerAddressDto : IMapFrom<CustomerAddress>
    {
        public string AddressType { get; set; }
        public AddressDto Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerAddress, CustomerAddressDto>();
        }
    }
}