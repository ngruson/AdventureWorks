using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.CreateCustomerAddress
{
    public class CustomerAddress : IMapFrom<Entities.CustomerAddress>
    {
        public CustomerAddress() { }
        public CustomerAddress(Address address, string addressType)
        {
            Address = address;
            AddressType = addressType;
        }

        public Address? Address { get; set; }
        public string? AddressType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerAddress, Entities.CustomerAddress>()
                .ForMember(_ => _.Id, opt => opt.Ignore())
                .ForMember(_ => _.ObjectId, opt => opt.Ignore())
                .ForMember(_ => _.CustomerId, opt => opt.Ignore());
        }
    }
}
