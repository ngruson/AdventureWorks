using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customers;

namespace AW.Services.API.CustomerAPI.Models
{
    public class ContactInfo : IMapFrom<ContactInfoDto>
    {
        public ContactInfoChannelType ContactInfoChannelType { get; set; }
        public string ContactInfoType { get; set; }
        public string Value { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ContactInfoDto, ContactInfo>();
        }
    }
}