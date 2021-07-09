using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.AddStoreCustomerContact
{
    public class StoreCustomerContactDto : IMapFrom<Entities.StoreCustomerContact>
    {
        public string ContactType { get; set; }
        public PersonDto ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreCustomerContactDto, Entities.StoreCustomerContact>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.StoreCustomer, opt => opt.Ignore())
                .ForMember(m => m.StoreCustomerId, opt => opt.Ignore())
                .ForMember(m => m.ContactPersonId, opt => opt.Ignore());
        }
    }
}