using AutoMapper;
using AW.Application.Common.Mappings;
using AW.Domain.Person;

namespace AW.Application.GetCustomers
{
    public class PersonDto : IMapFrom<Person>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Suffix { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Person, PersonDto>();
        }
    }
}