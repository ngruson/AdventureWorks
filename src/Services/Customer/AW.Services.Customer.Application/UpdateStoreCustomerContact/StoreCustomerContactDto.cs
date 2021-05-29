using AutoMapper;
using AW.Common.AutoMapper;

namespace AW.Services.Customer.Application.UpdateStoreCustomerContact
{
    public class StoreCustomerContactDto : IMapFrom<Domain.StoreCustomerContact>
    {
        public string ContactType { get; set; }
        public PersonDto ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.StoreCustomerContact, StoreCustomerContactDto>()
                .ReverseMap();
        }
    }
}