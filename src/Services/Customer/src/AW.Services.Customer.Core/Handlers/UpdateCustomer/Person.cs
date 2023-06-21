using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.Services.Customer.Core.Handlers.UpdateCustomer
{
    public class Person : IMapFrom<Entities.Person>
    {
        public string? Title { get; set; }
        public NameFactory? Name { get; set; }
        public string? Suffix { get; set; }
        public List<PersonEmailAddress> EmailAddresses { get; set; } = new();
        public List<PersonPhone> PhoneNumbers { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Person, Person>()
                .ReverseMap();
        }
    }
}