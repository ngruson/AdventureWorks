using AW.Common.AutoMapper;
using AW.Services.Customer.Application.GetCustomer;

namespace AW.Services.Customer.REST.API.Models.GetCustomer
{
    public class CustomerAddress : IMapFrom<CustomerAddressDto>
    {
        public string AddressType { get; set; }
        public Address Address { get; set; }
    }
}