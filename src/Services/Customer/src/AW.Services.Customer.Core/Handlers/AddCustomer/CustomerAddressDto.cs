using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.AddCustomer
{
    public class CustomerAddressDto : IMapFrom<Entities.CustomerAddress>
    {
        public string AddressType { get; set; }
        public AddressDto Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerAddressDto, Entities.CustomerAddress>()
                .ForMember(_ => _.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}