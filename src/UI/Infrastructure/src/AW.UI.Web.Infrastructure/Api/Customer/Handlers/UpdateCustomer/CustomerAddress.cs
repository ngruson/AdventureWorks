using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.UpdateCustomer
{
    public class CustomerAddress : IMapFrom<GetCustomer.CustomerAddress>
    {
        public string? AddressType { get; set; }
        public Address? Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetCustomer.CustomerAddress, CustomerAddress>();
            profile.CreateMap<GetIndividualCustomer.CustomerAddress, CustomerAddress>();
            profile.CreateMap<GetStoreCustomer.CustomerAddress, CustomerAddress>();
        }
    }
}