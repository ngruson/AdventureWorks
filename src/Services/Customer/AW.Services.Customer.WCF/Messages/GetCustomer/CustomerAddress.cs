using AutoMapper;
using AW.Services.Customer.Application.Common;
using AW.Services.Customer.Application.GetCustomer;

namespace AW.Services.Customer.WCF.Messages.GetCustomer
{
    public class CustomerAddress : IMapFrom<CustomerAddressDto>
    {
        public string AddressType { get; set; }
        public Address Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerAddressDto, CustomerAddress>();
        }
    }
}