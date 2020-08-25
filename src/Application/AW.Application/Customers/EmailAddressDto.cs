using AutoMapper;
using AW.Application.AutoMapper;
using AW.Domain.Person;

namespace AW.Application.Customers
{
    public class EmailAddressDto : IMapFrom<EmailAddress>
    {
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmailAddress, EmailAddressDto>()
                .ForMember(m => m.EmailAddress, opt => opt.MapFrom(src => src.EmailAddress1));
        }
    }
}