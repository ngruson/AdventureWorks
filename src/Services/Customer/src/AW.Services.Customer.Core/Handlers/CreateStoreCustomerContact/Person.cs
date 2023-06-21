using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.Services.Customer.Core.Handlers.CreateStoreCustomerContact
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

        public Guid ObjectId { get; set; }
        public string? Title { get; set; }
        public NameFactory? Name { get; set; }
        public string? Suffix { get; set; }
        public List<PersonEmailAddress> EmailAddresses { get; set; } = new List<PersonEmailAddress>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Person, Entities.Person>()
                .ForMember(_ => _.Id, opt => opt.Ignore())
                .ForMember(m => m.PhoneNumbers, opt => opt.Ignore());
        }
    }
}
