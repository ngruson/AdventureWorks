using AW.Services.Customer.Core.Handlers.DeleteCustomerAddress;
using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Models.DeleteCustomerAddress
{

    public class CustomerAddress : IMapFrom<CustomerAddressDto>
    {
        public string? AddressType { get; set; }
        public Address? Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerAddress, CustomerAddressDto>();

        }
    }
}