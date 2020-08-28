using AutoMapper;
using AW.Application.AutoMapper;
using AW.Domain.Person;
using System.Linq;

namespace AW.Application.Customers
{
    public class ContactDto : IMapFrom<BusinessEntityContact>
    {
        public string ContactTypeName { get; set; }
        public string ContactName { get; set; }
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BusinessEntityContact, ContactDto>()
                .ForMember(m => m.ContactName, opt => opt.MapFrom(src => src.Person.FullName))
                .ForMember(m => m.EmailAddress, opt => opt.MapFrom(src => src.Person.EmailAddresses.Any() ?
                    src.Person.EmailAddresses.ToList()[0].EmailAddress1 : null));
        }
    }
}