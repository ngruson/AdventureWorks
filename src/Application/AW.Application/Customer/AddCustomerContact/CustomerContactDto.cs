using AutoMapper;
using AW.Application.AutoMapper;
using AW.Domain.Person;

namespace AW.Application.Customer.AddCustomerContact
{
    public class CustomerContactDto : IMapFrom<BusinessEntityContact>
    {
        public string ContactTypeName { get; set; }
        public ContactDto Contact { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BusinessEntityContact, CustomerContactDto>()
                .ForMember(m => m.Contact, opt => opt.MapFrom(src => src.Person));
        }
    }
}