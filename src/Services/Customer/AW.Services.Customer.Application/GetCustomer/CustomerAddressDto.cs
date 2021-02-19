using AW.Services.Customer.Application.Common;

namespace AW.Services.Customer.Application.GetCustomer
{
    public class CustomerAddressDto : IMapFrom<Domain.CustomerAddress>
    {
        public string AddressType { get; set; }
        public AddressDto Address { get; set; }
    }
}