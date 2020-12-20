using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Domain.Person;
using System.Linq;

namespace AW.Core.Application.Customer.GetCustomers
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