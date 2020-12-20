using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Domain.Person;

namespace AW.Core.Application.Customer.DeleteCustomerContact
{
    public class ContactDto : IMapFrom<Person>
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ContactDto, Person>();
        }
    }
}