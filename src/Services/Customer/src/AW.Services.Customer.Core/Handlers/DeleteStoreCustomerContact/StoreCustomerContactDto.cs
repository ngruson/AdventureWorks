using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.DeleteStoreCustomerContact
{
    public class StoreCustomerContactDto : IMapFrom<Entities.StoreCustomerContact>
    {
        public string? ContactType { get; set; }
        public PersonDto? ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.StoreCustomerContact, StoreCustomerContactDto>();
        }
    }
}