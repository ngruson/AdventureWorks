using AutoMapper;
using AW.Common.AutoMapper;
using AW.Services.Customer.Application.GetCustomers;

namespace AW.Services.Customer.WCF.Messages.ListCustomers
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