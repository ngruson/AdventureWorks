using AutoMapper;
using AutoMapper.EquivalencyExpression;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.UpdateCustomer
{
    public class CustomerAddressDto : IMapFrom<Entities.CustomerAddress>
    {
        public string AddressType { get; set; }
        public AddressDto Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.CustomerAddress, CustomerAddressDto>()
                .ReverseMap()
                .EqualityComparison((src, dest) => src.AddressType == dest.AddressType);
        }
    }
}