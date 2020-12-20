using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Domain.Person;

namespace AW.Core.Application.Customer.AddCustomerContact
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