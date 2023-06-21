using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.Services.Customer.Core.Handlers.UpdateStoreCustomerContact
{
    public class Person : IMapFrom<Entities.Person>
    {
        public Person() { }
        public Person(string title, NameFactory name, string suffix)
        {
            Title = title;
            Name = name;
            Suffix = suffix;
        }

        public string? Title { get; set; }
        public NameFactory? Name { get; set; }
        public string? Suffix { get; set; }
        public List<PersonEmailAddress> EmailAddresses { get; set; } = new List<PersonEmailAddress>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Person, Person>()
                .ReverseMap();
        }
    }
}
