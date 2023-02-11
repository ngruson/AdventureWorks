using AutoMapper;
using AW.Services.Customer.Core.Handlers.DeleteStoreCustomerContact;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.Services.Customer.Core.Models.DeleteStoreCustomerContact
{
    public class Person : IMapFrom<PersonDto>
    {
        public string? Title { get; set; }
        public NameFactory? Name { get; set; }
        public string? Suffix { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Person, PersonDto>();
        }
    }
}