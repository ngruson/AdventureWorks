using AW.Services.Customer.Core.Handlers.GetCustomer;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.REST.API.Models.GetCustomer
{
    public class CustomerAddress : IMapFrom<CustomerAddressDto>
    {
        public string AddressType { get; set; }
        public Address Address { get; set; }
    }
}