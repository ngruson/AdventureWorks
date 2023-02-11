using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.Services.Customer.Core.Handlers.GetCustomer
{
    public class PersonDto : IMapFrom<Entities.Person>
    {
        public string? Title { get; set; }
        public NameFactory? Name { get; set; }
        public string? Suffix { get; set; }
        public List<PersonEmailAddressDto>? EmailAddresses { get; set; }
        public List<PersonPhoneDto>? PhoneNumbers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Person, PersonDto>();
        }
    }
}