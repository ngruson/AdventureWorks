using AutoMapper;
using AW.Services.Customer.Core.Handlers.UpdateStoreCustomerContact;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Models.UpdateStoreCustomerContact
{
    public class StoreCustomerContact : IMapFrom<StoreCustomerContactDto>
    {
        public string ContactType { get; set; }
        public Person ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreCustomerContact, StoreCustomerContactDto>();
        }
    }
}