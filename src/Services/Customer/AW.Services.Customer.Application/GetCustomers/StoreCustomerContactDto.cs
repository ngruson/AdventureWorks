using AutoMapper;
using AW.Services.Customer.Application.Common;

namespace AW.Services.Customer.Application.GetCustomers
{
    public class StoreCustomerContactDto : IMapFrom<Domain.StoreCustomerContact>
    {
        public string ContactType { get; set; }
        public PersonDto ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.StoreCustomerContact, StoreCustomerContactDto>();
        }
    }
}