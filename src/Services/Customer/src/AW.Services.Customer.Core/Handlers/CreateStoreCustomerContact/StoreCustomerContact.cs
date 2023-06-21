using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.CreateStoreCustomerContact
{
    public class StoreCustomerContact : IMapFrom<Entities.StoreCustomerContact>
    {
        public StoreCustomerContact() { }
        public StoreCustomerContact(string contactType, Person contactPerson)
        {
            ContactType = contactType;
            ContactPerson = contactPerson;
        }

        public string? ContactType { get; set; }
        public Person? ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreCustomerContact, Entities.StoreCustomerContact>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(_ => _.ObjectId, opt => opt.Ignore())
                .ForMember(m => m.StoreCustomerId, opt => opt.Ignore())
                .ForMember(m => m.ContactPersonId, opt => opt.Ignore());
        }
    }
}
