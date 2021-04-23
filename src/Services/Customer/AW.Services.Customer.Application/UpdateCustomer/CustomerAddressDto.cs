using AutoMapper;
using AutoMapper.EquivalencyExpression;
using AW.Services.Customer.Application.Common;


namespace AW.Services.Customer.Application.UpdateCustomer
{
    public class CustomerAddressDto : IMapFrom<Domain.CustomerAddress>
    {
        public string AddressType { get; set; }
        public AddressDto Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.CustomerAddress, CustomerAddressDto>()
                .ReverseMap()
                .EqualityComparison((src, dest) => src.AddressType == dest.AddressType);
        }
    }
}