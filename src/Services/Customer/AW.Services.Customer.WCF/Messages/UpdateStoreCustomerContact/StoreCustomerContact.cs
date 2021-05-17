using AutoMapper;
using AW.Services.Customer.Application.UpdateStoreCustomerContact;
using AW.Common.AutoMapper;

namespace AW.Services.Customer.WCF.Messages.UpdateStoreCustomerContact
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