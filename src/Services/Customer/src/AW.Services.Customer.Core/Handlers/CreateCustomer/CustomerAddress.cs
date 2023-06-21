using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.CreateCustomer
{
    public class CustomerAddress : IMapFrom<Entities.CustomerAddress>
    {
        public string? AddressType { get; set; }
        public Address? Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerAddress, Entities.CustomerAddress>()
                .ForMember(_ => _.Id, opt => opt.Ignore())
                .ForMember(_ => _.ObjectId, opt => opt.Ignore())
                .ForMember(_ => _.CustomerId, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
