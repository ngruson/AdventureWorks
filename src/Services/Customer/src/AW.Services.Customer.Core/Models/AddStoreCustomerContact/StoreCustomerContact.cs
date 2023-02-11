using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.AddStoreCustomerContact;

namespace AW.Services.Customer.Core.Models.AddStoreCustomerContact
{
    public class StoreCustomerContact : IMapFrom<StoreCustomerContactDto>
    {
        public string? ContactType { get; set; }
        public Person? ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreCustomerContact, StoreCustomerContactDto>();
        }
    }
}