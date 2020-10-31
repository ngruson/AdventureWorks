using AutoMapper;
using AW.Application.AutoMapper;
using AW.Domain.Person;
using System.Linq;

namespace AW.Application.Customer
{
    public class CustomerContactDto : IMapFrom<BusinessEntityContact>
    {
        public string ContactTypeName { get; set; }
        public ContactDto Contact { get; set; }
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BusinessEntityContact, CustomerContactDto>()
                .ForMember(m => m.Contact, opt => opt.MapFrom(src => src.Person))
                .ForMember(m => m.EmailAddress, opt => opt.MapFrom(src => src.Person.EmailAddresses.Any() ?
                    src.Person.EmailAddresses.ToList()[0].EmailAddress1 : null));
        }
    }
}