using AutoMapper;
using AW.Common.AutoMapper;
using AW.Services.Customer.Application.AddStoreCustomerContact;

namespace AW.Services.Customer.WCF.Messages.AddStoreCustomerContact
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