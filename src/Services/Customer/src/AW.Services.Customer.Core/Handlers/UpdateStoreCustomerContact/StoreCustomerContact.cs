using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.UpdateStoreCustomerContact
{
    public class StoreCustomerContact : IMapFrom<Entities.StoreCustomerContact>
    {
        public StoreCustomerContact() { }
        public StoreCustomerContact(Guid objectId, string contactType, Person contactPerson)
        {
            ObjectId = objectId;
            ContactType = contactType;
            ContactPerson = contactPerson;
        }

        public Guid ObjectId { get; set; }
        public string? ContactType { get; set; }
        public Person? ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.StoreCustomerContact, StoreCustomerContact>()
                .ReverseMap();
        }
    }
}
