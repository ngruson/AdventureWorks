using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.UpdateCustomerAddress
{
    public class CustomerAddress : IMapFrom<Entities.CustomerAddress>
    {
        public Guid ObjectId { get; set; }
        public string? AddressType { get; set; }
        public Address? Address { get; set; }
    }
}
