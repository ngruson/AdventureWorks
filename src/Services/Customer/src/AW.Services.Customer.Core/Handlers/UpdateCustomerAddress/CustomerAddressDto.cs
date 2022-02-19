using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.UpdateCustomerAddress
{
    public class CustomerAddressDto : IMapFrom<Entities.CustomerAddress>
    {
        public string AddressType { get; set; }
        public AddressDto Address { get; set; }
    }
}