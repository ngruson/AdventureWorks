using AutoMapper;
using AW.Services.Customer.Application.Common;
using AW.Services.Customer.Application.UpdateCustomer;

namespace AW.Services.Customer.WCF.Messages.UpdateCustomer
{
    public class StoreCustomerContact : IMapFrom<StoreCustomerContactDto>
    {
        public string ContactType { get; set; }
        public PersonDto ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreCustomerContact, StoreCustomerContactDto>();
        }
    }
}