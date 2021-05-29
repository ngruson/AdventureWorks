using AutoMapper;
using AW.Common.AutoMapper;
using System.Collections.Generic;

namespace AW.Services.Customer.Application.AddCustomer
{
    public class PersonDto : IMapFrom<Domain.Person>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public List<PersonEmailAddressDto> EmailAddresses { get; set; }
        public List<PersonPhoneDto> PhoneNumbers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonDto, Domain.Person>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}