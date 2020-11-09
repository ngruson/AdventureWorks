using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer.AddCustomerContact;

namespace AW.Services.API.CustomerAPI.Models
{
    public class EmailAddress : IMapFrom<EmailAddressDto>
    {
        public string EmailAddress1 { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmailAddressDto, EmailAddress>()
                .ForMember(m => m.EmailAddress1, opt => opt.MapFrom(src => src.EmailAddress));
        }
    }
}