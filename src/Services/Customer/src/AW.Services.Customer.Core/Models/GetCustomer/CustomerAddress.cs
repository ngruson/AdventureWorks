using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.GetCustomer;

namespace AW.Services.Customer.Core.Models.GetCustomer
{
    public class CustomerAddress : IMapFrom<CustomerAddressDto>
    {
        public string? AddressType { get; set; }
        public Address? Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerAddressDto, CustomerAddress>();
        }
    }
}