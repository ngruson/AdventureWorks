using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.GetCustomer
{
    public class CustomerAddressDto : IMapFrom<Entities.CustomerAddress>
    {
        public string AddressType { get; set; }
        public AddressDto Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.CustomerAddress, CustomerAddressDto>();
        }
    }
}