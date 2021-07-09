using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System.Collections.Generic;

namespace AW.Services.Customer.Core.Handlers.AddStoreCustomerContact
{
    public class PersonDto : IMapFrom<Entities.Person>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public List<EmailAddressDto> EmailAddresses { get; set; } = new List<EmailAddressDto>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonDto, Entities.Person>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.PhoneNumbers, opt => opt.Ignore());
        }
    }
}