using AutoMapper;
using AW.Application.AutoMapper;
using AW.Domain.Person;

namespace AW.Application.Customer.UpdateCustomerContact
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