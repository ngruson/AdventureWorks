using AutoMapper;
using AW.Services.Customer.Application.Common;

namespace AW.Services.Customer.Application.DeleteStoreCustomerContact
{
    public class StoreCustomerContactDto : IMapFrom<Domain.StoreCustomerContact>
    {
        public string ContactTypeName { get; set; }
        public ContactDto Contact { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.StoreCustomerContact, StoreCustomerContactDto>();
        }
    }
}