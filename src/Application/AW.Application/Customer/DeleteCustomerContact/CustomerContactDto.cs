using AutoMapper;
using AW.Application.AutoMapper;
using AW.Domain.Person;
using System.Linq;

namespace AW.Application.Customer.DeleteCustomerContact
{
    public class CustomerContactDto : IMapFrom<BusinessEntityContact>
    {
        public string ContactTypeName { get; set; }
        public ContactDto Contact { get; set; }
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerContactDto, BusinessEntityContact>();
        }
    }
}