using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Domain.Person;

namespace AW.Core.Application.Customer.AddCustomerContact
{
    public class EmailAddressDto : IMapFrom<EmailAddress>
    {
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmailAddressDto, EmailAddress>()
                .ForMember(m => m.EmailAddress1, opt => opt.MapFrom(src => src.EmailAddress))
                .ReverseMap();
        }
    }
}