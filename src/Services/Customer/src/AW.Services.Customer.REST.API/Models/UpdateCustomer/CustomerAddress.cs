using AutoMapper;
using AW.Services.Customer.Core.Handlers.UpdateCustomer;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.REST.API.Models.UpdateCustomer
{
    public class CustomerAddress : IMapFrom<CustomerAddressDto>
    {
        public string AddressType { get; set; }
        public Address Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerAddress, CustomerAddressDto>()
                .ReverseMap();
        }
    }
}